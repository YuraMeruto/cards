using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnTextAnimation : UpdateBase
{
    static TurnTextAnimation instnace;

    Text player_text;
    Vector3 player_init_pos;
    Vector3 turn_init_pos;
    Vector3 player_target_pos;
    Vector3 turn_target_pos;
    Text turn_text;
    float const_speed = 0.1f;
    float enable_time = 0;
    float wait_finish_time = 0.4f;
    float cp_wait_finish_time;
    bool is_update = false;
    public static TurnTextAnimation Instance { get { return instnace; } }

    // Start is called before the first frame update
    public void Ini()
    {
        SetUpPlayerInit();
        SetUpTurnMessage();
        cp_wait_finish_time = wait_finish_time;
        instnace = this;
    }

    public void Start(TurnManager.Turn action_type)
    {
        Debug.Log(action_type);
        is_update = true;
        switch (action_type)
        {
            case TurnManager.Turn.Draw:
                SetUpDrawMessage();
                break;
            case TurnManager.Turn.Player:
                SetUpPlayerMessage();
                break;
            case TurnManager.Turn.Enemy:
                SetUpEnemyMessage();
                break;
        }
        ResetPosition();
        wait_finish_time = cp_wait_finish_time;
    }


    public override void Update()
    {
        if (!is_update)
        {
            return;
        }
        enable_time += Time.deltaTime;  // Œo‰ßŽžŠÔ‚Ì‰ÁŽZ
        var rate = Mathf.Clamp01(enable_time / const_speed);   // Š„‡ŒvŽZ

        player_text.rectTransform.anchoredPosition = Vector3.Lerp(player_init_pos, player_target_pos,rate);

        turn_text.rectTransform.anchoredPosition = Vector3.Lerp(turn_init_pos, turn_target_pos, rate);
        if (player_text.rectTransform.anchoredPosition.x >= player_target_pos.x && turn_text.rectTransform.anchoredPosition.x <= turn_target_pos.x)
        {
            wait_finish_time -= Time.deltaTime;
        }
        if (wait_finish_time <= 0.0f)
        {
            player_text.text = "";
            turn_text.text = "";
            is_update = false;
            NextAction();
        }
    }

    void NextAction()
    {
        switch (TurnManager.TurnStatus)
        {
            case TurnManager.Turn.Draw:
                DrawBattleManager.Instance.InstanceDrawCards();
                break;
            case TurnManager.Turn.Player:
                Debug.Log("Player");
                CardManager.OnEffectCard();
                break;
            case TurnManager.Turn.Enemy:
                CardManager.OnEffectCard();
                break;
        }
    }

    void ResetPosition()
    {
        enable_time = 0;
        player_text.rectTransform.anchoredPosition = player_init_pos;
        turn_text.rectTransform.anchoredPosition = turn_init_pos;
    }


    void SetUpPlayerInit()
    {
        player_text = GameObject.Find("PlayerTurnMessage").GetComponent<Text>();
        Vector3 pos = Vector3.zero;
        pos.z = 10;
        pos.y = Screen.height / 2;
        player_init_pos = pos;
        player_target_pos = player_init_pos;
        player_target_pos.x = Screen.width / 2;
        player_text.rectTransform.anchoredPosition = pos;
    }


    void SetUpTurnMessage()
    {
        turn_text = GameObject.Find("TurnMessage").GetComponent<Text>();
        Vector3 pos = Vector3.zero;
        pos.z = 10;
        pos.x = Screen.width;
        pos.y = Screen.height / 3.5f;
        turn_text.rectTransform.anchoredPosition = pos;
        turn_init_pos = pos;
        turn_target_pos = turn_init_pos;
        turn_target_pos.x = Screen.width / 2;
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
