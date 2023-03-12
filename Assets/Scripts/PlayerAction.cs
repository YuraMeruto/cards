using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class PlayerAction
{
    bool is_update = false;

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
                if (clickedGameObject.tag == TagManager.CARD)
                {
                    hitCard(clickedGameObject);
                }
            }

        }
    }
    private void hitCard(GameObject obj)
    {
        CardManager.attachCard(obj);
        if(CardManager.AttachCard.Count != 2)
        {
            return;
        }
        is_update = false;
        ActionTimeManger.Instance.StartWaitCardTime();
    }
}
