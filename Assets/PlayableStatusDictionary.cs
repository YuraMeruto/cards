using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableStatusDictionary
{
    public static PlayableCharacterIcon LoadPlayable(PlayableCharacterNumber.Number number)
    {
        switch (number)
        {
            case PlayableCharacterNumber.Number.GoHomeClub:
                return new GoHomeClub();
        }
        return null;
    }
}
