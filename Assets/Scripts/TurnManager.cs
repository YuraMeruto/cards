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
        Draw,
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

    public void PlayerBattle(CardManager.Number card_number)
    {
        // バトル計算
        var hp = BattleCalucation.ResultCalucation(card_number, UIManager.Instance.EnemyStatus.HP,false,true);
        if (hp <= 0)
        {
            hp = 0;
        }
        UIManager.Instance.EnemyStatus.setHp(hp);
        foreach (var val in PlayableCharacterManager.Instance.ActionPlayableList)
        {
            val.AttackSe();
        }

        var addhp = new AddHpTextAnimation();
        addhp.Ini((int)card_number + 1,false,false);
        BattleManager.Instance.SetMovePlayableIcon();
        if (BattleCalucation.IsFinish())
        {
            UIManager.Instance.Finish(true);
        }
        CardManager.resetDestoryAttachCards();
        ComboManager.Instance.SetCombo();
        TargetIconManager.Instance.IconDestory();
        BattleManager.Instance.Destorytaret();
    }

    public void EnemyBattle(CardManager.Number card_number)
    {
        // バトル計算
        var hp = BattleCalucation.ResultCalucation(card_number, UIManager.Instance.PlayerStatus.HP,true, true);
        if (hp <= 0)
        {
            hp = 0;
        }
        UIManager.Instance.PlayerStatus.setHp(hp);
        foreach (var val in PlayableCharacterManager.Instance.ActionPlayableList)
        {
            val.AttackSe();
        }

        var addhp = new AddHpTextAnimation();
        addhp.Ini((int)card_number + 1, true, false);
        BattleManager.Instance.SetMovePlayableIcon();
        if (BattleCalucation.IsFinish())
        {
            UIManager.Instance.Finish(false);
        }
        ComboManager.Instance.SetCombo();
        CardManager.resetDestoryAttachCards();
        TargetIconManager.Instance.IconDestory();
        BattleManager.Instance.Destorytaret();

    }
}
