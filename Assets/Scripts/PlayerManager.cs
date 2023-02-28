using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : UpdateBase
{
    PlayerAction player_action;
    public void Ini()
    {
        player_action = new PlayerAction();
    }

    public override void Update()
    {
        player_action.Update();
    }
}
