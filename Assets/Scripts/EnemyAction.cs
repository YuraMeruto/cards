using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAction
{
    bool is_update = false;
    EnemyManager.ActionType action_type = EnemyManager.ActionType.None;
    Card pear_card = new Card();
    public bool IsUpdate { get { return is_update; } set { is_update = value; } }
    float update_time = 2.0f;
    public void Update()
    {
        if(TurnManager.TurnStatus != TurnManager.Turn.Enemy)
        {
            return;
        }
        if (!is_update)
        {
            return;
        }
        update_time -= Time.deltaTime;
        if(update_time >= 0)
        {
            return;
        }
        Debug.Log("更新します"+CardManager.AttachedCards.Count);
        Debug.Log(action_type);
        update_time = 2.0f;
        if (action_type == EnemyManager.ActionType.None)
        {
            Debug.Log("dicideアクション");
            dicideAction();
        }
        else if (action_type == EnemyManager.ActionType.Pear)
        {
            Debug.Log("Pearアクション");
            CardManager.attachCard(pear_card.GameObject);
            pear_card = null;
        }
        else if (action_type == EnemyManager.ActionType.Random)
        {
            Debug.Log("ランダムアクション");
            var rnd = randomCard();
            CardManager.attachCard(rnd);

        }
        if (CardManager.AttachCard.Count != 2)
        {
            Debug.Log("帰ります！－");
            return;
        }
        Debug.Log("敵更新リセット");
        action_type = EnemyManager.ActionType.None;
        if (CardManager.isMatching())
        {
            // バトル計算
            var hp = BattleCalucation.ResultCalucation(CardManager.AttachCard[0].CardType, UIManager.Instance.PlayerStatus.HP);
            UIManager.Instance.PlayerStatus.HP = hp;
            UIManager.Instance.PlayerStatus.Text.text = hp.ToString();
            // 連続でドロー
            CardManager.sucssesMatching();
            Debug.Log("isFinish"+ BattleCalucation.isFinish());
            if (BattleCalucation.isFinish())
            {
                UIManager.Instance.Finish(false);
            }

            return;
        }
        is_update = false;
        ActionTimeManger.Instance.StartWaitCardTime();
    }

    private void searchCard()
    {

    }

    private GameObject randomCard()
    {
        GameObject target = null;
        while (true)
        {
            var number = CardManager.GameObjectList.Count;
            var rnd = new System.Random();
            var rnd_target = CardManager.GameObjectList.ElementAt(rnd.Next(0, CardManager.GameObjectList.Count));
            Debug.Log("randomCard_START");
            Debug.Log(CardManager.AttachedCards.Count);
            Debug.Log(CardManager.GameObjectList[rnd_target.Key].GameObject.GetInstanceID());
            Debug.Log("randomCard_END");

            if (CardManager.AttachedCards.Count == 0 || CardManager.AttachCard.Count == 0 || CardManager.AttachCard[0].GameObject.GetInstanceID() != CardManager.GameObjectList[rnd_target.Key].GameObject.GetInstanceID()) {
                target = CardManager.GameObjectList[rnd_target.Key].GameObject;
                break;
            }
        }
        Debug.Log("選びました");
        return target;
    }

    private void dicideAction()
    {
        if (action_type == EnemyManager.ActionType.None)
        {
            Debug.Log("索敵アクション");
            var target = searchPearCards();
            if(target != null)
            {
                CardManager.attachCard(target.GameObject);
                action_type = EnemyManager.ActionType.Pear;
                return;
            }
        }

        searchCard();
        var rnd = randomCard();
        action_type = EnemyManager.ActionType.Random;
        CardManager.attachCard(rnd);

    }

    private Card searchPearCards()
    {
        Debug.Log("searchPearCards");
        Dictionary<CardManager.Number, Card> c = new Dictionary<CardManager.Number, Card>();
        Debug.Log(CardManager.AttachedCards.Count);
        foreach (var val in CardManager.AttachedCards)
        {
            var i = 0;
            foreach (var v in CardManager.AttachedCards) {
                Debug.Log(v.Value.CardType +":" + val.Value.CardType);
                if (v.Value.CardType == val.Value.CardType)
                {
                    i++;
                }
                if(i == 2)
                {
                    Debug.Log("i+2");
                    pear_card = val.Value;
                    Debug.Log("debugaaaa");
                    return v.Value;
                }
            }
        }
        return null;
    }
}
