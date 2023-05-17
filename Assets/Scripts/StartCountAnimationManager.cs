using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class StartCountAnimationManager
{
    static bool is_animation = true;
    public static bool IsAnimation { get { return is_animation; } set { is_animation = value; } }

    public static void startAnimationInstance()
    {
        if (!is_animation)
        {
            return;
        }
        is_animation = false;



        var count_text = GameObject.Find(ConstValues.START_COUNT_TECT_OBJECT_NAME);

        count_text.SetActive(true);
        StartCountAnimation s = new StartCountAnimation();
        s.Ini(count_text.GetComponent<Text>(), 1.5f, 3);
        GameMaster.GameMasterClass.animationUpdateList.Add(count_text, s);

        return;
    }
}
