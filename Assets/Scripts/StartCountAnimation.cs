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
    string copy_count;

    public void Ini(Text obj,float wait,int count)
    {
        game_object = obj;
        wait_time = wait;
        count_time = count;
        copy_wait_time = wait;
        game_object.text = count.ToString();
        copy_count = count.ToString();
        var pos = Vector3.zero;
        pos.x = Screen.width / 2;
        pos.y = Screen.height / 2;
        pos.z = ConstValues.PLAYABKE_POS_Z;
        game_object.rectTransform.anchoredPosition = pos;
    }

    public void Reset()
    {
        game_object.text = copy_count;
        count_time = int.Parse(copy_count);
        wait_time = copy_wait_time;
    }
    public override void Finish()
    {
//        MonoBehaviour.Destroy(game_object);
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
            game_object.gameObject.GetComponent<Text>().text="";
            Debug.Log("aaaa"+ TurnManager.TurnStatus);
            if (TurnManager.TurnStatus == TurnManager.Turn.None)
            {
                PlayableCharacterManager.Instance.IsUpdate = true;
            }
            StartCountAnimationManager.IsAnimation = false;
            if(TurnManager.TurnStatus == TurnManager.Turn.Enemy)
            {
                CardManager.OnEffectCard();
                EnemyManager.InstanceEnemyManager.EnemyAction.IsUpdate = true;
            }
        }
    }
}
