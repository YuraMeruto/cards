using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus 
{
    int hp = 10;
    public int HP { get { return hp; } set { hp = value; } }
    Text text;
    public Text Text { get { return text; } set { text = value; } }

    public void setHp(int value)
    {
        hp = value;
        text.text = hp.ToString();
    }

}
