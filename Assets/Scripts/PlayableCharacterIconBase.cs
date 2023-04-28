using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PlayableCharacterIconBase
{
    protected bool is_enemy;
    protected GameObject game_object;
    protected SpriteRenderer sprite;
    protected float relocation; // 再配置の値
    protected float back_attack = 1.0f; // 配置させる値
    protected bool is_swoon = false;
    protected float swoon_time;
    protected bool is_alpha_update = true;

    public float BackAttack { get { return back_attack; } set { back_attack = value; } }
    public GameObject PlayableObject { get { return game_object; } }
    public float ReLocation { get { return relocation; } set { relocation = value; } }
    public bool IsEnemy { get { return is_enemy; } set { is_enemy = value; } }
    public bool IsSwoon { get { return is_swoon; } set { is_swoon = value; } }
    public float SwoonTime { get { return swoon_time; } set { swoon_time = value; } }
    public SpriteRenderer SpriteRen { get { return sprite; }set { sprite = value; } }
    public bool IsAlphaUpdate { get { return is_alpha_update; } set { is_alpha_update = value; } }
    abstract public void Update();
}
