using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCountAnimation : AnimationUpdateBase
{
    Text game_object;
    float wait_time = 0.0f;
    int count_time;
    float copy_wait_time;

    public void Ini(Text obj,float wait,int count)
    {
        game_object = obj;
        wait_time = wait;
        count_time = count;
        copy_wait_time = wait;
        game_object.text = count.ToString();
    }
    public override void Finish()
    {
        MonoBehaviour.Destroy(game_object);
    }

    public override void Update()
    {
        wait_time -= Time.deltaTime;
        if (wait_time <= 0.0f)
        {
            count_time--;
            game_object.text = count_time.ToString();
            wait_time = copy_wait_time;
        }
        if (game_object.text == "0")
        {
            GameMaster.GameMasterClass.animationUpdateList.AddRemove(game_object.gameObject);
            game_object.gameObject.SetActive(false);
            PlayableCharacterManager.Instance.IsUpdate = true;
        }
    }
}
