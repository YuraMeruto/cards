using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager
{
    static BattleManager instance;
    public static BattleManager Instance { get { return instance; } }

    PlayableCharacterIconBase target;
    GameObject target_back_space;
    public PlayableCharacterIconBase Target { get { return target; } }

    public void Ini()
    {
        instance = this;
    }

    public void SetTargetPlayableCharacter(GameObject obj)
    {

        target = PlayableCharacterManager.Instance.GetPlayable(obj);
        // çƒîzíuå„ÇÃèÍèäÇê∂ê¨
        if (target_back_space != null)
        {
            MonoBehaviour.Destroy(target_back_space);
        }
        target_back_space = MonoBehaviour.Instantiate(target.PlayableObject);
        var c = target_back_space.GetComponent<SpriteRenderer>().color;
        c.a = c.a / 2;
        target_back_space.GetComponent<SpriteRenderer>().color = c;
        var pos = target.PlayableObject.transform.position;
        var back_value = 0.0f;
        MonoBehaviour.Destroy(target_back_space.GetComponent<BoxCollider2D>());
        foreach (var val in PlayableCharacterManager.Instance.ActionPlayableList)
        {
            back_value += Utill.BackPostionCalculation(val, target);
        }
        if (!target.IsEnemy)
        {
            back_value = -back_value;
        }
        pos.x += back_value;
        target_back_space.transform.position = pos;
    }

    public void SetMovePlayableIcon()
    {
        var back_value = 0.0f;
        var pos = target.PlayableObject.gameObject.transform.position;
        foreach (var val in PlayableCharacterManager.Instance.ActionPlayableList)
        {
            back_value += Utill.BackPostionCalculation(val, target);
        }
        if (!target.IsEnemy)
        {
            back_value = -back_value;
        }
        pos.x += back_value;
        target.PlayableObject.gameObject.transform.position = pos;
    }

    public void Destorytaret()
    {
        target = null;
        MonoBehaviour.Destroy(target_back_space);
    }
}
