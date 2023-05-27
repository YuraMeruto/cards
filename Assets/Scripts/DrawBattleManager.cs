using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DrawBattleManager 
{
    static DrawBattleManager instance;
    public static DrawBattleManager Instance { get { return instance; } set { instance = value; } }
    public void Ini()
    {
        instance = this;
    }
    Dictionary<int, Card> cards = new Dictionary<int, Card>();
    Card player_attach_card;
    Card enemy_attach_card;
    public enum BattleResult
    {
        Sucsess,
        Failed,
        Draw,
    }

    BattleResult result;
    public BattleResult Result { get { return result; } }

    public Dictionary<int, Card> Cards { get { return cards; } set { cards = value; } }
    public Card PlayerAttachCard { get { return player_attach_card; } set { player_attach_card = value; } }

    public Card EnemyAttachCard { get { return enemy_attach_card; } }
    public void InstanceDrawCards()
    {
        cards.Clear();
        player_attach_card = null;
        enemy_attach_card = null;
        var instance_list = new List<CardManager.Number>();
        for (var instance_type = 0; instance_type < 2; instance_type++)
        {
            while (true)
            {
                var no = UnityEngine.Random.Range(0, (int)CardManager.Number.King);
                if (instance_list.IndexOf((CardManager.Number)no) < 0)
                {
                    instance_list.Add((CardManager.Number)no);
                    break;
                }
            }
        }
        // カードのオブジェクト生成
        var bulletPrefabHundle = Addressables.LoadAssetAsync<GameObject>(AddressablesNames.BACK_PREFAB);
        // 非同期での処理について終了を待つ
        var bulletPrefab = bulletPrefabHundle.WaitForCompletion();
        var instance_pos = Vector3.zero;
        instance_pos.x = Screen.width / 1.5f;
        instance_pos.y = Screen.height / 1.5f;

        var scale = Vector3.zero;
        scale.x = 1;
        scale.y = 1;
        scale.z = 1;

        instance_pos.z = ConstValues.PLAYABKE_POS_Z;
        foreach (var val in instance_list)
        {
            var instance = MonoBehaviour.Instantiate(bulletPrefab);
            instance.transform.position = Camera.main.ScreenToWorldPoint(instance_pos);
            instance.gameObject.tag = TagManager.DRAW_CARD;
            var card = new Card();
            card.Ini(val, instance, CardManager.ShowStatus.Back);
            instance_pos.x += 10;
            instance.transform.position =Camera.main.ScreenToWorldPoint(instance_pos);
            instance.transform.localScale = scale;
            instance_pos.x = Screen.width / 3f;
            cards.Add(instance.gameObject.GetInstanceID(),card);
        }
    }

    public Card GetCard(GameObject obj)
    {
        return Cards[obj.GetInstanceID()];
    }

    public void PlayerSelect(GameObject obj)
    {
        var card = GetCard(obj);
        card.ChangeShowStatus();
        player_attach_card = card;
        // いったんプレイヤーが決めたら敵も同時に決める
        EnemySelect();
        BattleStart();
    }

    public void EnemySelect()
    {
        foreach (var val in cards)
        {
            if (val.Key == player_attach_card.GameObject.GetInstanceID())
            {
                continue;
            }
            enemy_attach_card = val.Value;
            val.Value.ChangeShowStatus();
            break;
        }
    }

    public void BattleStart()
    {

        if (player_attach_card.ToIntNumber() < enemy_attach_card.ToIntNumber())
        {
            result = BattleResult.Failed;
            DrawBattleUI.Instance.SetBattleResult(result);
        }else if (player_attach_card.ToIntNumber() > enemy_attach_card.ToIntNumber())
        {
            result = BattleResult.Sucsess;
            DrawBattleUI.Instance.SetBattleResult(result);
            CommonSeManager.PlaySe(ConstValues.DRAW_BATTLE_RESULT_WIN_VALUE);
        }
        else if (player_attach_card.ToIntNumber() == enemy_attach_card.ToIntNumber())
        {
            result = BattleResult.Draw;
            DrawBattleUI.Instance.SetBattleResult(result);
        }
    }

    public void BattleResultCalucation()
    {
        switch (Result)
        {
            case BattleResult.Failed:
                EnemyBattle(Instance.EnemyAttachCard.CardType);
                break;
            case BattleResult.Sucsess:
                PlayerBattle(Instance.PlayerAttachCard.CardType);
                break;
            case BattleResult.Draw:
                break;

        }
    }

    public void Destroy()
    {
        foreach (var val in cards)
        {
            MonoBehaviour.Destroy(val.Value.GameObject);
        }
    }

    public void Clear()
    {
        cards.Clear();
    }

    public void PlayerBattle(CardManager.Number card_number)
    {
        // バトル計算
        var hp = BattleCalucation.ResultCalucation(card_number, UIManager.Instance.EnemyStatus.HP, false, true);
        if (hp <= 0)
        {
            hp = 0;
        }
        UIManager.Instance.EnemyStatus.setHp(hp);

        var addhp = new AddHpTextAnimation();
        addhp.Ini((int)card_number + 1, false, false);
        var playble = PlayableCharacterManager.Instance.PlayerGoalList[0];
        playble.DrawBattleSe();

        if (BattleCalucation.IsFinish())
        {
            UIManager.Instance.Finish(true);
        }
        SetMovePlayableIconForPlayer();

    }

    public void EnemyBattle(CardManager.Number card_number)
    {
        // バトル計算
        var hp = BattleCalucation.ResultCalucation(card_number, UIManager.Instance.PlayerStatus.HP, false, true);
        if (hp <= 0)
        {
            hp = 0;
        }
        UIManager.Instance.PlayerStatus.setHp(hp);

        var addhp = new AddHpTextAnimation();
        addhp.Ini((int)card_number + 1, false, false);
        var playble = PlayableCharacterManager.Instance.EnemyGoalList[0];
        playble.DrawBattleSe();

        if (BattleCalucation.IsFinish())
        {
            UIManager.Instance.Finish(true);
        }
        SetMovePlayableIconForEnemy();

    }


    public void SetMovePlayableIconForPlayer()
    {
        var enemy_list = PlayableCharacterManager.Instance.EnemyGoalList;
        // フィールド外に飛ばしてとりま気絶させる
        var back_value = Utill.GetOutField(true);
        foreach (var enemy in enemy_list)
        {
            var pos = enemy.PlayableObject.gameObject.transform.position;
            pos.x = back_value;
            BattleManager.Instance.SetSwoonStatus(enemy);
            PlayableMovePostionAnimation.Instance.Set(enemy.PlayableObject.gameObject, enemy.PlayableObject.gameObject.transform.position,pos);
//            enemy.PlayableObject.gameObject.transform.position = pos;
        }
        PlayableMovePostionAnimation.Instance.Start(PlayableMovePostionAnimation.ActionType.Player);
        foreach (var player in PlayableCharacterManager.Instance.PlayerGoalList)
        {
            PlayableCharacterManager.Instance.SetRelocation(player);
        }
//        PlayableCharacterManager.Instance.UpdateRestart();
    }

    public void SetMovePlayableIconForEnemy()
    {
        var player_list = PlayableCharacterManager.Instance.PlayerGoalList;
        // フィールド外に飛ばしてとりま気絶させる
        var back_value = Utill.GetOutField(false);
        foreach (var player in player_list)
        {
            var pos = player.PlayableObject.gameObject.transform.position;
            pos.x = back_value;
            BattleManager.Instance.SetSwoonStatus(player);
            player.PlayableObject.gameObject.transform.position = pos;
        }
        foreach (var enemy in PlayableCharacterManager.Instance.EnemyGoalList)
        {
            PlayableCharacterManager.Instance.SetRelocation(enemy);
        }
        PlayableCharacterManager.Instance.UpdateRestart();

    }
}
