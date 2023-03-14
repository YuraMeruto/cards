using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCalucation : MonoBehaviour
{

    public static int ResultCalucation(CardManager.Number number, int remaining_hp)
    {
        remaining_hp = remaining_hp - ((int)number + 1);
        return remaining_hp;
    }

    public static bool isFinish()
    {
        if (UIManager.Instance.PlayerStatus.HP <= 0 || UIManager.Instance.EnemyStatus.HP <= 0)
        {
            return true;
        }
        return false;
    }
}
