using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// ‹A‘î•”
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
        DrawBattleSuccesessSe = ConstValues.GO_HOME_CLUB_DRAW_BATTLE_VALUE; 
        var load_sprite = Addressables.LoadAssetAsync<Sprite>(AddressablesNames.GOHOME_ICON);
        // ”ñ“¯Šú‚Å‚Ìˆ—‚É‚Â‚¢‚ÄI—¹‚ğ‘Ò‚Â
        var s = load_sprite.WaitForCompletion();
        sprite.sprite = s;

        PlayableSeManager.LoadVoice(PlayableCharacterNumber.Number.GoHomeClub);
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
}
