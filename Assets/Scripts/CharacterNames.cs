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
    }

    public static string GetName(Character c)
    {
        switch (c)
        {
            case Character.GoHome:
                return "�A�";
            case Character.Kendo:
                return "������";
                break;
        }
        return "";
    }
}
