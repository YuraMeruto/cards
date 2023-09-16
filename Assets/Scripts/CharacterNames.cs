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
                return "‹A‘î•”";
            case Character.Kendo:
                return "Œ•“¹•”";
            case Character.BaseBall:
                return "–ì‹…•”";
            case Character.Unknow:
                return "???";
        }
        return "";
    }
}
