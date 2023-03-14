using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardChangeWait : AnimationUpdateBase
{
    float wait_time = 0.0f;
    GameObject game_obj;

    public void Ini(float time,GameObject obj)
    {
        wait_time = time;
        game_obj = obj;
    }

    public override void Finish()
    {

    }

    public override void Update()
    {
        if (is_finish == true)
        {
            return;
        }
        wait_time = wait_time - Time.deltaTime;
        if (wait_time <= 0.0f)
        {
            is_finish = true;
            Debug.Log("hogehoge");
            CardManager.ChangeCard(game_obj);
            GameMaster.GameMasterClass.animationUpdateList.AddRemove(game_obj);
        }
    }
}
