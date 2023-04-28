using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus
{
    int hp = 10;
    Slider slider;
    Text text;

    public int HP { get { return hp; } set { hp = value; } }
    public Text Text { get { return text; } set { text = value; } }
    public Slider Slider { get { return slider; } set { slider = value; } }

    public void setHp(int value)
    {
        hp = value;
        text.text = hp.ToString();
        slider.value = (float)hp;
    }
}
