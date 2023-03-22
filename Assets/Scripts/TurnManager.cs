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
            ActionTimeManger.Instance.ResetWaitCardTime();
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
            CardManager.sucssesMatching();
            return;
            // バトル計算
            var hp = BattleCalucation.ResultCalucation(CardManager.AttachCards[0].CardType, UIManager.Instance.PlayerStatus.HP);
            if (hp <= 0)
            {
                hp = 0;
            }
            UIManager.Instance.PlayerStatus.HP = hp;
            UIManager.Instance.PlayerStatus.Text.text = hp.ToString();
            // 連続でドロー
            if (BattleCalucation.IsFinish())
            {
                UIManager.Instance.Finish(false);
            }
//            return;
        }
        CardManager.failureMatching();
        PlayableCharacterManager.Instance.Relocation();
        EnemyManager.InstanceEnemyManager.updateSet(false);
    }

    private void PlayerResult()
    {
        if (CardManager.isMatching())
        {
            CardManager.sucssesMatching();
            return;
        }
        CardManager.failureMatching();
        PlayableCharacterManager.Instance.Relocation();
        PlayerManager.InstancePlayerManger.updateSet(false);

    }

    public void PlayerBattle()
    {
        // バトル計算
        var hp = BattleCalucation.ResultCalucation(CardManager.AttachCards[0].CardType, UIManager.Instance.EnemyStatus.HP);
        if (hp <= 0)
        {
            hp = 0;
        }
        UIManager.Instance.EnemyStatus.HP = hp;
        UIManager.Instance.EnemyStatus.Text.text = hp.ToString();
        // 連続でドロー
        if (BattleCalucation.IsFinish())
        {
            UIManager.Instance.Finish(true);
        }
        CardManager.failureMatching();
        PlayableCharacterManager.Instance.Relocation();
        PlayerManager.InstancePlayerManger.updateSet(false);
        TargetIconManager.Instance.IconDestory();
    }
}
