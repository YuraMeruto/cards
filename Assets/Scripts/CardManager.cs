using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using System.Linq;

public class CardManager
{
    const int INSTANT_COUNT = 4;
    const int INSTANCE_TYPE = 2;
    public enum Number {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
    }
    public enum ShowStatus
    {
        None,
        Front,
        Back
    }
    static Dictionary<int, Card> attached_cards = new Dictionary<int, Card>();
    static List<Card> attach_card = new List<Card>();
    static Dictionary<int, Card> game_object_list = new Dictionary<int, Card>();
    public static Dictionary<int, Card> GameObjectList { get { return game_object_list; } set { game_object_list = value; } }
    public static List<Card> AttachCards { get { return attach_card; } }
    public static Dictionary<int,Card> AttachedCards { get { return attached_cards; } set { attached_cards = value; } }
    public static void InstanceCards()
    {
        attached_cards.Clear();
        game_object_list.Clear();
        var instance_list = new List<Number>();
        // 出すカードの種類を決める
        for (var instance_type = 0; instance_type <= INSTANCE_TYPE; instance_type++)
        {
            while (true)
            {
                var no = UnityEngine.Random.Range(0, (int)Number.King);
                if (instance_list.IndexOf((Number)no) < 0) {
                    instance_list.Add((Number)no);
                    break;
                }
            }
        }

        var add_card_instance_list = new List<Number>(instance_list);

        foreach (var val in instance_list)
        {
            add_card_instance_list.Add(val);
        }
        // ランダムシャッフル
        add_card_instance_list = add_card_instance_list.OrderBy(a => Guid.NewGuid()).ToList();

        // カードのオブジェクト生成
        var bulletPrefabHundle = Addressables.LoadAssetAsync<GameObject>(AddressablesNames.BACK_PREFAB);

        // 非同期での処理について終了を待つ
        var bulletPrefab = bulletPrefabHundle.WaitForCompletion();

        // Prefabからゲームオブジェクトの作成
        float left_pos = 0;
        float top_pos = 0.0f;
        var const_x = Screen.width / 10;
        var const_y = Screen.height / 4;
        var instance_line = INSTANT_COUNT / 2;
        var instance_column = 2;
        // カード生成
        var instance_pos = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10));
        for (var index_column = 0; index_column < instance_column; index_column++) {
            top_pos += const_y;
            left_pos = 0;
            for (var index = 0; index <= instance_line; index++)
            {
                var instance = MonoBehaviour.Instantiate(bulletPrefab);

                instance.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10));
                instance.gameObject.tag = TagManager.CARD;
                var card = new Card();
                card.Ini(add_card_instance_list[0], instance, ShowStatus.Back);
                add_card_instance_list.RemoveAt(0);
                game_object_list.Add(instance.GetInstanceID(), card);
                // 配置移動のスクリプト
                CardInstanceMove move = new CardInstanceMove();
                left_pos += const_x;
                var ran = UnityEngine.Random.RandomRange(0.5f,1.0f);
                move.Ini(instance, Camera.main.ScreenToWorldPoint(new Vector3(left_pos, top_pos, 10)), ran);
                GameMaster.GameMasterClass.animationUpdateList.Add(instance,move);
                // プレハブが裏なので画像を変える
                card.ChangeShowStatus();
            }
        }
    }

    public static void ChangeCard(GameObject obj){
        var card = GetCard(obj);
        card.ChangeShowStatus();
    }

    public static void AttachCard(GameObject obj)
    {
        if (isAttached(obj))
        {
            return;
        }

        if (attach_card.Count == 2)
        {
            Debug.Log("タッチしている数が２なので返す");
            return;
        }
        var card = GetCard(obj);
        attach_card.Add(card);
        ChangeCard(obj);
    }

    public static bool resultCards()
    {

        if (!isMatching())
        {
            Debug.Log("違うので裏にします");
            return false;
        }
        return true;
    }

    public static void failureMatching()
    {
        setAttachedCards();
        resetAttachMaching();
    }

    public static void sucssesMatching()
    {
        if (TurnManager.TurnStatus == TurnManager.Turn.Player)
        {
            var t = PlayableCharacterManager.Instance.getEnemyIconList()[0];
            TargetIconManager.Instance.InstanceIcon(t.PlayableObject.gameObject.transform.position);
            PlayerManager.InstancePlayerManger.PlayerAction.Status = PlayerAction.ActionStatus.TargetEnemy;
            PlayerManager.InstancePlayerManger.PlayerAction.IsUpdate = true;
            BattleManager.Instance.SetTargetPlayableCharacter(t.PlayableObject.gameObject);
        }
        else if (TurnManager.TurnStatus == TurnManager.Turn.Enemy)
        {
            var t = PlayableCharacterManager.Instance.getPlayerIconList()[0];
            TargetIconManager.Instance.InstanceIcon(t.PlayableObject.gameObject.transform.position);
            EnemyManager.InstanceEnemyManager.updateSetEnemyTargetAction(true);
        }
        return;
        /*
        removeAttachedCards();
        destoryAttachCards();
        if (game_object_list.Count == 0)
        {
            Debug.Log("再生産");
            InstanceCards();
        }
        */
    }

    public static void resetDestoryAttachCards()
    {
        removeAttachedCards();
        destoryAttachCards();
        if (game_object_list.Count == 0)
        {
            if (TurnManager.TurnStatus == TurnManager.Turn.Enemy)
            {
                EnemyManager.InstanceEnemyManager.updateSet(false);
            }
            InstanceCards();
        }
    }

    public static void setAttachedCards()
    {
        foreach(var val in attach_card)
        {
            if(attached_cards.ContainsKey(val.GameObject.GetInstanceID())) {
                continue;
            }
            attached_cards.Add(val.GameObject.GetInstanceID(),val);
        }
    }

    public static void removeAttachedCards()
    {
        foreach (var val in attach_card)
        {
            if (!attached_cards.ContainsKey(val.GameObject.GetInstanceID()))
            {
                continue;
            }
            attached_cards.Remove(val.GameObject.GetInstanceID());
        }
    }

        public static bool isAttached(GameObject obj)
    {
        foreach(var val in attach_card)
        {
            if(val.GameObject.GetInstanceID() == obj.GetInstanceID())
            {
                return true;
            }
        }
        return false;

    }

    public static void destoryAttachCards()
    {
        for (var index = 0; index < attach_card.Count; index++)
        {
            game_object_list.Remove(attach_card[index].GameObject.GetInstanceID());
            MonoBehaviour.Destroy(attach_card[index].GameObject);
        }
        attach_card.Clear();
    }

    public static void resetAttachMaching()
    {
        foreach (var val in attach_card)
        {
            val.ChangeShowStatus();
        }
        attach_card.Clear();
        OffEffectCard();
    }

    public static bool isMatching()
    {
        if (attach_card[0].CardType == attach_card[1].CardType)
        {
            return true;
        }

        return false;
    }


    public static Card GetCard(GameObject gameObj)
    {
        return game_object_list[gameObj.GetInstanceID()];
    }

    public static string GetCardSpriteFront(Number num)
    {
        switch (num)
        {
            case Number.Ace:
                return AddressablesNames.FRONT_ACE;
            case Number.Two:
                return AddressablesNames.FRONT_TWO;
            case Number.Three:
                return AddressablesNames.FRONT_THREE;
            case Number.Four:
                return AddressablesNames.FRONT_FOUR;
            case Number.Five:
                return AddressablesNames.FRONT_FIVE;
            case Number.Six:
                return AddressablesNames.FRONT_SIX;
            case Number.Seven:
                return AddressablesNames.FRONT_SEVEN;
            case Number.Eight:
                return AddressablesNames.FRONT_EIGHT;
            case Number.Nine:
                return AddressablesNames.FRONT_NINE;
            case Number.Ten:
                return AddressablesNames.FRONT_TEN;
            case Number.Jack:
                return AddressablesNames.FRONT_JACK;
            case Number.Queen:
                return AddressablesNames.FRONT_QUEEN;
            case Number.King:
                return AddressablesNames.FRONT_KING;
        }
        return "";
    }

    public static void OnEffectCard()
    {
        foreach(var card in game_object_list)
        {
            card.Value.SetActiveRightEffect(true);
        }
    }
    public static void OffEffectCard()
    {
        foreach (var card in game_object_list)
        {
            card.Value.SetActiveRightEffect(false);
        }
    }

}
