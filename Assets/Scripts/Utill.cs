using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utill
{
    public static void Hoge()
    {
        Debug.Log("Hoge");
    }

    public static Vector3 FowardObj(Vector3 pos, bool is_enemy)
    {
        var p = pos;
        var add = Time.deltaTime * 4.0f* ConstValues.DEFAULT_MOVE_SPEED;
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

    public static bool IsGoalPos(GameObject obj, GameObject goal_obj, bool is_enemy)
    {
        if (is_enemy)
        {
            return goal_obj.transform.position.x >= obj.transform.position.x;
        }
        return obj.transform.position.x >= goal_obj.transform.position.x;
    }

    public static float BackPostionCalculation(PlayableCharacterIconBase attacker, PlayableCharacterIconBase attacked_side)
    {
        var back_value = attacker.BackAttack;
        back_value *= 10;
        return back_value;
    }

    public static float getDistanceFromGoalPos(GameObject goal_obj, GameObject obj)
    {
        return goal_obj.transform.position.x - obj.transform.position.x;
    }

    public static Vector3 convertScreenPos(Vector3 pos)
    {
        return Vector3.zero;
    }

    public static bool IsOutField(Vector3 pos, bool is_enemy)
    {

        var pos_c = Camera.main.WorldToScreenPoint(pos);
        if (is_enemy && pos_c.x >= Screen.width)
        {
            return true;
        }
        if (pos_c.x <= 0.0f)
        {
            return true;
        }
        return false;
    }

    public static float GetOutField(bool is_enemy)
    {
        var vec = Vector3.zero;
        if (is_enemy)
        {
            vec.x = Screen.width;
            return Camera.main.ScreenToWorldPoint(vec).x;
        }
        return Camera.main.ScreenToWorldPoint(vec).x;
    }
}
