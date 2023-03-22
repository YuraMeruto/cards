using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class PlayerAction
{
    bool is_update = false;
    public enum ActionStatus
    {
        None,
        CardSelect,
        TargetEnemy,
    }
    ActionStatus action_satus = ActionStatus.None;
    public ActionStatus Status { get { return action_satus; } set { action_satus = value; } }

    public bool IsUpdate { get { return is_update; } set { is_update = value; } }

    public void Update()
    {
        if (TurnManager.TurnStatus != TurnManager.Turn.Player)
        {
            return;
        }
        if (!is_update)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            GameObject clickedGameObject = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2d)
            {
                clickedGameObject = hit2d.transform.gameObject;
                switch (action_satus)
                {
                    case ActionStatus.CardSelect:
                        CardSelect(clickedGameObject);
                        break;
                    case ActionStatus.TargetEnemy:
                        TargetEnemy(clickedGameObject);
                        break;
                }
            }

        }
    }

    private void CardSelect(GameObject target)
    {
        if (target.tag == TagManager.CARD)
        {
            hitCard(target);
        }
    }

    private void TargetEnemy(GameObject target)
    {
        Debug.Log(target.tag);
        if (target.tag == TagManager.ENEMY)
        {
            if (IsBattleStart(target))
            {
                TurnManager.Instance.PlayerBattle();
            }
            BattleManager.Instance.SetTargetPlayableCharacter(target);
            TargetIconManager.Instance.MoveTarget(target);
        }
    }

    private void hitCard(GameObject obj)
    {
        CardManager.AttachCard(obj);
        if (CardManager.AttachCards.Count != 2)
        {
            return;
        }
        is_update = false;
        ActionTimeManger.Instance.StartWaitCardTime();
    }

    private bool IsBattleStart(GameObject target)
    {

        if (BattleManager.Instance.Target.PlayableObject.GetInstanceID() != target.GetInstanceID())
        {
            return false;
        }
        return true;
    }
}
