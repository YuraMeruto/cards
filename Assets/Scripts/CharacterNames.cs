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
                return "‹A‘î•”";
            case Character.Kendo:
                return "Œ•“¹•”";
                break;
        }
        return "";
    }
}
