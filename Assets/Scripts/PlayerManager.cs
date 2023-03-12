using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : UpdateBase
{
    static PlayerManager player_manager;
    public static PlayerManager InstancePlayerManger { get { return player_manager; } }
    PlayerAction player_action;
    public PlayerAction PlayerAction { get { return player_action; } }
    public void Ini()
    {
        player_action = new PlayerAction();
        player_manager = this;
    }

    public override void Update()
    {
        player_action.Update();
    }

    public void updateSet(bool value)
    {
        InstancePlayerManger.player_action.IsUpdate = value;
    }
}
