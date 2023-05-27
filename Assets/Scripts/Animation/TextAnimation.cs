using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour
{
    Text player_text;
    Vector3 player_init_pos;
    Vector3 turn_init_pos;
    Vector3 player_target_pos;
    Vector3 turn_target_pos;
    float player_distance;
    float turn_disntance;
    Text turn_text;
    float const_speed = 2000;
    float wait_finish_time = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        player_text = GameObject.Find("PlyaerTurnMessage").GetComponent<Text>();
        turn_text = GameObject.Find("TurnMessage").GetComponent<Text>();
        Vector3 pos = Vector3.zero;
        pos.z = 10;
        pos.y = Screen.height / 2;
        player_text.rectTransform.anchoredPosition = pos;
        player_init_pos = pos;
        player_target_pos = player_init_pos;
        player_target_pos.x = Screen.width / 2;
        player_distance = Vector3.Distance(player_init_pos, player_target_pos);
        SetUpTurnMessage();
        SetUpDrawMessage();
    }

    void Update()
    {
        float h = (Time.time * const_speed) / player_distance;
        player_text.rectTransform.anchoredPosition = Vector3.Lerp(player_init_pos, player_target_pos, h);

        float t = (Time.time * const_speed) / turn_disntance;
        turn_text.rectTransform.anchoredPosition = Vector3.Lerp(turn_init_pos, turn_target_pos, t);
        if(player_text.rectTransform.anchoredPosition.x >= player_target_pos.x && turn_text.rectTransform.anchoredPosition.x <= turn_target_pos.x)
        {
            Debug.Log("Hogeee");
            wait_finish_time -= Time.deltaTime;
        }
        if (wait_finish_time <= 0.0f)
        {
            player_text.text = "";
            turn_text.text = "";
        }
    }

    void SetUpTurnMessage()
    {
        Vector3 pos = Vector3.zero;
        pos.z = 10;
        pos.x = Screen.width;
        pos.y = Screen.height / 3.5f;
        turn_text.rectTransform.anchoredPosition = pos;
        turn_init_pos = pos;
        turn_target_pos = turn_init_pos;
        turn_target_pos.x = Screen.width / 2;
        turn_disntance = Vector3.Distance(turn_init_pos, turn_target_pos);
    }

    void SetUpEnemyMessage()
    {
        player_text.text = "Enemy";
        player_text.color = Color.red;
        turn_text.color = Color.red;
        turn_text.text = "Turn";
    }

    void SetUpPlayerMessage()
    {
        player_text.text = "Player";
        player_text.color = Color.blue;
        turn_text.color = Color.blue;
        turn_text.text = "Turn";
    }

    void SetUpDrawMessage()
    {
        player_text.text = "Draw";
        player_text.color = Color.yellow;
        turn_text.color = Color.yellow;
        turn_text.text = "Action";
    }
}
