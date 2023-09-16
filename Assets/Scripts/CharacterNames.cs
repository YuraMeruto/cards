using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNames 
{
    public enum Character
    {
        None,
        GoHome,
        Kendo,
        BaseBall,
        Unknow,
    }

    public static string GetName(Character c)
    {
        switch (c)
        {
            case Character.GoHome:
                return "�A�";
            case Character.Kendo:
                return "������";
            case Character.BaseBall:
                return "�싅��";
            case Character.Unknow:
                return "???";
        }
        return "";
    }
}
