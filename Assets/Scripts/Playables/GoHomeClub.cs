using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// 帰宅部
/// </summary>
public class GoHomeClub : PlayableCharacterIcon
{
    public override void Ini(GameObject o, float r_location)
    {
        base.Ini(o,r_location);
        AttackSe01 = ConstValues.GO_HOME_CLUB_ATTACK_01_VALUE;
        AttackSe02 = ConstValues.GO_HOME_CLUB_ATTACK_02_VALUE;
        AttackSe03 = ConstValues.GO_HOME_CLUB_ATTACK_03_VALUE;
        AttackSe04 = ConstValues.GO_HOME_CLUB_ATTACK_04_VALUE;
        AttackSe05 = ConstValues.GO_HOME_CLUB_ATTACK_05_VALUE;

        AttackEffect01 = ConstValues.GO_HOME_CLUB_ATTACK_EFFECT_01_VALUE;
        AttackEffect02 = ConstValues.GO_HOME_CLUB_ATTACK_EFFECT_02_VALUE;
        AttackEffect03 = ConstValues.GO_HOME_CLUB_ATTACK_EFFECT_03_VALUE;
        AttackEffect04 = ConstValues.GO_HOME_CLUB_ATTACK_EFFECT_04_VALUE;
        AttackEffect05 = ConstValues.GO_HOME_CLUB_ATTACK_EFFECT_05_VALUE;

        DrawBattleSuccesessSe = ConstValues.GO_HOME_CLUB_DRAW_BATTLE_VALUE; 
        var load_sprite = Addressables.LoadAssetAsync<Sprite>(AddressablesNames.GOHOME_ICON);
        // 非同期での処理について終了を待つ
        var s = load_sprite.WaitForCompletion();
        sprite.sprite = s;

        LoadAsset();
    }

    void LoadAsset()
    {
        PlayableSeManager.LoadVoice(PlayableCharacterNumber.Number.GoHomeClub);
        PlayableEffectManager.LoadEffect(PlayableCharacterNumber.Number.GoHomeClub);
    }

    public override void AttackSe()
    {
        var se_value = base.SettingComboSe();
        AudioSource.clip = PlayableSeManager.VoiceList[se_value];
        AudioSource.Play();
    }

    public override void DrawBattleSe()
    {
        AudioSource.clip = PlayableSeManager.VoiceList[draw_battle_succesess_se];
        AudioSource.Play();
    }

    public override void AttackEffect()
    {
        Debug.Log("帰宅部のエフェクト");
        var instance = PlayableEffectManager.Insatnce(base.SettingComboEffect());
    }
}
