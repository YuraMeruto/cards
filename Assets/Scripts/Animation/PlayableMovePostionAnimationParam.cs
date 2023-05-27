using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableMovePostionAnimationParam
{
    GameObject obj;
    Vector3 start_pos;
    Vector3 end_pos;

    public GameObject Obj { get { return obj; } }
    public Vector3 StartPos { get { return start_pos; } }
    public Vector3 EndPos { get { return end_pos; } }

    public void Ini(GameObject target_obj, Vector3 s_pos, Vector3 e_pos)
    {
        obj = target_obj;
        start_pos = s_pos;
        end_pos = e_pos;
    }

    public bool IsFinish()
    {
        if (obj.transform.position.x >= end_pos.x && obj.transform.position.x <= end_pos.x)
        {
            return true;
        }
        return false;
    }
}
