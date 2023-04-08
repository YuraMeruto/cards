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
    EnemyTargetAction enemey_target_action = new EnemyTargetAction();

    static EnemyManager enemyManager;
    public static EnemyManager InstanceEnemyManager { get { return enemyManager; } }
    public EnemyAction EnemyAction { get { return enemy_action; } set { enemy_action = value; } }
    public EnemyTargetAction EnemyTargetAction { get { return enemey_target_action; } set { enemey_target_action = value; } }

    public void Ini()
    {
        enemyManager = this;
    }

    // Update is called once per frame
    override public void Update()
    {
        enemy_action.Update();
        enemey_target_action.Update();
    }

    public void updateSet(bool value)
    {
        InstanceEnemyManager.enemy_action.IsUpdate = value;
    }

    public void updateSetEnemyTargetAction(bool value)
    {
        InstanceEnemyManager.enemey_target_action.IsUpdate = value;
    }
}
