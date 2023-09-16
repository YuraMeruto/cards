using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChainTextAnimation : UpdateBase
{
    Text text;
    float show_time;
    float copy_show_time = 1;
    bool is_update = false;
    static ChainTextAnimation instance;
    public static ChainTextAnimation Instance { get { return instance; } }

    public override void Update()
    {
        if (!is_update)
        {
            return;
        }
        show_time -= Time.deltaTime;
        if (show_time <= 0)
        {
            is_update = false;
            text.text = "";
        }
    }

    public void Ini()
    {
        instance = this;
        text = GameObject.Find("ChainText").GetComponent<Text>();
        var pos = text.transform.position;
        pos.x = Screen.width / 2;
        pos.y = Screen.height / 2;
        text.rectTransform.anchoredPosition = pos;
        text.text = "";
    }
    public void Start(TurnManager.Turn turn)
    {
        text.text = "Chain";
        switch (turn)
        {
            case TurnManager.Turn.Enemy:
                text.color = Color.red;
                break;
            case TurnManager.Turn.Player:
                text.color = Color.blue;
                break;
        }
        show_time = copy_show_time;
        is_update = true;
    }
}
