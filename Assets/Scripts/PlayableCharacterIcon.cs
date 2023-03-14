using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterIcon :PlayableCharacterIconBase
{
    bool is_update = true;
    public bool IsUpdate { get { return is_update; } set { is_update = value; } }

    public void Ini(GameObject o,float r_location)
    {
        game_object = o;
        ReLocation = r_location;

    }

    // Update is called once per frame
    public override void Update()
    {
        if (!is_update)
        {
            return;
        }
        MoveUpdate();
    }

    void MoveUpdate()
    {
        game_object.transform.position = Utill.FowardObj(game_object.transform.position,is_enemy);
        return;
    }

}
