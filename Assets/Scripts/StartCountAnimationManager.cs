using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class StartCountAnimationManager
{
    static bool is_animation = true;
    public static bool IsAnimation { get { return is_animation; } set { is_animation = value; } }
    static StartCountAnimation start_count_animation;
    public static void startAnimationInstance()
    {
        is_animation = true;


        var count_text = GameObject.Find(ConstValues.START_COUNT_TECT_OBJECT_NAME);

        count_text.SetActive(true);
        if (start_count_animation == null)
        {
            StartCountAnimation start_count_animation = new StartCountAnimation();
            start_count_animation.Ini(count_text.GetComponent<Text>(), 1.5f, 3);
            GameMaster.GameMasterClass.animationUpdateList.Add(count_text, start_count_animation);
        }
        else
        {
            start_count_animation.Reset();
        }
        return;
    }
}
