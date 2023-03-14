using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTimeManger : UpdateBase
{
    static ActionTimeManger action_time_manger;
    bool is_wait_card_time_update = false;
    float wait_card_time = 0.0f;

    public float WaitCardTime { get { return wait_card_time; } set { wait_card_time = value; } }
    public static ActionTimeManger Instance { get { return action_time_manger; } }

    public void Ini()
    {
        action_time_manger = this;
    }
    public override void Update()
    {
        if (is_wait_card_time_update)
        {
            wait_card_time = wait_card_time - Time.deltaTime;
        }
    }

    public void ResetWaitCardTime()
    {
        wait_card_time = 0.0f;
        is_wait_card_time_update = false;
    }

    public void StartWaitCardTime()
    {
        wait_card_time = 1.0f;
        is_wait_card_time_update = true;
    }
}
