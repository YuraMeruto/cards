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
    protected AudioSource audio_source;
    protected  int attack_se_01 = 0;
    protected int attack_se_02 = 0;
    protected int attack_se_03 = 0;
    protected int attack_se_04 = 0;
    protected int attack_se_05 = 0;
    protected int draw_battle_succesess_se = 0;

    public AudioSource AudioSource { get { return audio_source; } set { audio_source = value; } }
    public float BackAttack { get { return back_attack; } set { back_attack = value; } }
    public GameObject PlayableObject { get { return game_object; } }
    public float ReLocation { get { return relocation; } set { relocation = value; } }
    public bool IsEnemy { get { return is_enemy; } set { is_enemy = value; } }
    public bool IsSwoon { get { return is_swoon; } set { is_swoon = value; } }
    public float SwoonTime { get { return swoon_time; } set { swoon_time = value; } }
    public SpriteRenderer SpriteRen { get { return sprite; }set { sprite = value; } }
    public bool IsAlphaUpdate { get { return is_alpha_update; } set { is_alpha_update = value; } }
     public virtual int AttackSe01 { get { return attack_se_01; } set { attack_se_01 = value; } }
    public virtual int AttackSe02 { get { return attack_se_02; } set { attack_se_02 = value; } }
    public virtual int AttackSe03 { get { return attack_se_03; } set { attack_se_03 = value; } }
    public virtual int AttackSe04 { get { return attack_se_04; } set { attack_se_04 = value; } }
    public virtual int AttackSe05 { get { return attack_se_05; } set { attack_se_05 = value; } }

    public virtual int DrawBattleSuccesessSe { get { return draw_battle_succesess_se; } set { draw_battle_succesess_se = value; } }

    abstract public void Update();

    abstract public void AttackSe();
    abstract public void DrawBattleSe();
    public virtual int SettingComboSe()
    {
        var combo = BattleCalucation.Instance.Combo;
        if (combo >= 5)
        {
            combo = 5;
        }
        switch (combo)
        {
            case 1:
                return AttackSe01;
            case 2:
                return AttackSe02;
            case 3:
                return AttackSe03;
            case 4:
                return AttackSe04;
            case 5:
                return AttackSe05;
        }
        return 0;
    }
}
