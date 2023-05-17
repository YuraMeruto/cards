using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterIcon :PlayableCharacterIconBase
{
    bool is_update = true;
    public bool IsUpdate { get { return is_update; } set { is_update = value; } }


    public virtual void Ini(GameObject o,float r_location)
    {
        game_object = o;
        ReLocation = r_location;
        SpriteRen = o.GetComponent<SpriteRenderer>();
        AudioSource = o.GetComponent<AudioSource>();
    }

    public virtual void OverRideParameter(int attack_se)
    {
        AttackSe01 = attack_se;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (!is_update)
        {
            return;
        }
        if (IsSwoon)
        {
            SwoonUpdate();
            return;
        }
        MoveUpdate();
    }

    public void SwoonUpdate()
    {
        SwoonTime -= Time.deltaTime;
        var color = sprite.color;
        if (is_alpha_update)
        {
            color.a += Time.deltaTime * ConstValues.SWOON_UPDATE_COLOR_ALPHA;
        }
        else
        {
            color.a -= Time.deltaTime * ConstValues.SWOON_UPDATE_COLOR_ALPHA;
        }
        if (color.a <= 0.0f)
        {
            is_alpha_update = true;
        }
        else if(color.a >= 1.0f){
            is_alpha_update = false;
        }

        sprite.color = color;
        if (SwoonTime <= 0.0f)
        {
            is_swoon = false;
            color.a = 1.0f;
            sprite.color = color;
        }
    }

    void MoveUpdate()
    {
        game_object.transform.position = Utill.FowardObj(game_object.transform.position,is_enemy);
        return;
    }

    public override void AttackSe()
    {
        Debug.Log("PlayeableC"+AttackSe01+":::"+game_object.GetInstanceID());
    }

    public override void DrawBattleSe()
    {
        Debug.Log("PlayeableC" + AttackSe01 + ":::" + game_object.GetInstanceID());
    }
}
