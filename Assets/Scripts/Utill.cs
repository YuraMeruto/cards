using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utill 
{
    public static void Hoge()
    {
        Debug.Log("Hoge");
    }

    public static Vector3 FowardObj(Vector3 pos,bool is_enemy)
    {
        var p = pos;
        var add = Time.deltaTime * 4.0f;
        if (!is_enemy)
        {
            p.x += add;
        }
        else
        {
            p.x -= add;

        }
        return p;

    }

    public static bool IsGoalPos(GameObject obj,GameObject goal_obj,bool is_enemy)
    {
        if (is_enemy) {
            return goal_obj.transform.position.x >= obj.transform.position.x;
        }
        return obj.transform.position.x >= goal_obj.transform.position.x;
    }

    public static float BackPostionCalculation(PlayableCharacterIconBase attacker, PlayableCharacterIconBase attacked_side)
    {
        var back_value = attacker.BackAttack;
        return back_value;
    }
}
