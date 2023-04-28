using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardChangeManager
{
    static CardChangeManager instance;
    public static CardChangeManager Instance { get { return instance; } }
    List<CardChangeWait> change_list = new List<CardChangeWait>();
    public List<CardChangeWait> ChangeWaits { get { return change_list; } set { change_list = value; } }
    int card_list_count = 0;
    public int CardListCount { get { return card_list_count; } set { card_list_count = value; } }
    public void Ini() {
        instance = this;
    }

    public void Decriment()
    {
        instance.card_list_count--;
        if(instance.card_list_count == 0 && TurnManager.TurnStatus == TurnManager.Turn.Enemy)
        {
            EnemyManager.InstanceEnemyManager.updateSet(true);
        }
    }
}
