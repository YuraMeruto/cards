using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetAction
{

    bool is_update;

    public bool IsUpdate { get { return is_update; } set { is_update = value; } }

    public enum Status
    {
        None,
        Target
    }

    public void Update()
    {
        if (!is_update)
        {
            return;
        }
        DecideTargetPlayable();
        TurnManager.Instance.EnemyBattle(CardManager.AttachCards[0].CardType);
        is_update = false;

        EnemyManager.InstanceEnemyManager.updateSet(true);
        TurnManager.TurnStatus = TurnManager.Turn.Enemy;
    }

    public void DecideTargetPlayable()
    {
        var target = findNearPosFromGoal();
        BattleManager.Instance.SetTargetPlayableCharacter(target);

    }

    private PlayableCharacterIconBase findNearPosFromGoal()
    {
        var playable_list = PlayableCharacterManager.Instance.getPlayerIconList();
        var near_pos = -999f;
        PlayableCharacterIconBase target = null;
        foreach (var val in playable_list)
        {
            if (val.PlayableObject.gameObject.transform.position.x >= near_pos)
            {
                target = val;
                near_pos = val.PlayableObject.gameObject.transform.position.x;
            }
        }
        return target;
    }
}
