using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PlayableCharacterIconBase
{
    protected bool is_enemy;
    protected GameObject game_object;
    protected float relocation; // 再配置の値
    protected float back_attack = 1.0f; // 配置させる値


    public float BackAttack { get { return back_attack; } set { back_attack = value; } }
    public GameObject PlayableObject { get { return game_object; } }
    public float ReLocation { get { return relocation; } set { relocation = value; } }
    public bool IsEnemy { get { return is_enemy; } set { is_enemy = value; } }
    abstract public void Update();
}
