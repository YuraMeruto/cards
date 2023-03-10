using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : UpdateBase
{
    public enum Turn
    {
        None,
        Player,
        Enemy,
    }
    static Turn turn;
    public static Turn TurnStatus { get { return turn; } set { turn = value; } }
    static TurnManager turn_manger;
    public static TurnManager Instance { get { return turn_manger; } }

    public void Ini()
    {
        turn_manger = this;
    }
    public override void Update()
    {
        if (ActionTimeManger.Instance.WaitCardTime < 0.0f)
        {
            Debug.Log("リセット");
            ActionTimeManger.Instance.resetWaitCardTime();
            CardResult();
//            ChangeTurn();
        }
    }

    private void ChangeTurn()
    {
        if (turn == Turn.Player)
        {
            turn = Turn.Enemy;
            return;
        }
        turn = Turn.Player;
    }

    private void CardResult()
    {
        switch (turn)
        {
            case Turn.Player:
                PlayerResult();
                break;
            case Turn.Enemy:
                EnemyResult();
                break;
        }
    }

    private void EnemyResult()
    {
        if (CardManager.isMatching())
        {
            // バトル計算
            var hp = BattleCalucation.ResultCalucation(CardManager.AttachCard[0].CardType, UIManager.Instance.PlayerStatus.HP);
            if (hp <= 0)
            {
                hp = 0;
            }
            UIManager.Instance.PlayerStatus.HP = hp;
            UIManager.Instance.PlayerStatus.Text.text = hp.ToString();
            // 連続でドロー
            CardManager.sucssesMatching();
            if (BattleCalucation.isFinish())
            {
                UIManager.Instance.Finish(false);
            }
            return;
        }
        CardManager.failureMatching();
    }

    private void PlayerResult()
    {
        if (CardManager.isMatching())
        {
            // バトル計算
            var hp = BattleCalucation.ResultCalucation(CardManager.AttachCard[0].CardType, UIManager.Instance.EnemyStatus.HP);
            if (hp <= 0)
            {
                hp = 0;
            }
            UIManager.Instance.EnemyStatus.HP = hp;
            UIManager.Instance.EnemyStatus.Text.text = hp.ToString();
            // 連続でドロー
            CardManager.sucssesMatching();
            if (BattleCalucation.isFinish())
            {
                Debug.Log("kokoko");
                UIManager.Instance.Finish(true);
            }
            return;
        }
        CardManager.failureMatching();
    }
}
