using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : UpdateBase
{
    public enum ActionType
    {
        None,
        Search,
        Pear,
        Random
    }
    EnemyAction enemy_action = new EnemyAction();
    static EnemyManager enemyManager;
    public static EnemyManager InstanceEnemyManager { get { return enemyManager; } }
    EnemyAction EnemyAction { get { return enemy_action; } set { enemy_action = value; } }
    public void Ini()
    {
        enemyManager = this;
    }

    // Update is called once per frame
    override public void Update()
    {
        enemy_action.Update();
    }

    public void updateSet(bool value)
    {
        InstanceEnemyManager.enemy_action.IsUpdate = value;
    }
}
