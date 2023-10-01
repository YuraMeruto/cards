using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class KendoClub : PlayableCharacterIcon
{
    public override void Ini(GameObject o, float r_location)
    {
        base.Ini(o, r_location);
        AttackSe01 = ConstValues.KENDO_CLUB_ATTACK_01_VALUE;
        AttackSe02 = ConstValues.KENDO_CLUB_ATTACK_02_VALUE;
        AttackSe03 = ConstValues.KENDO_CLUB_ATTACK_03_VALUE;
        AttackSe04 = ConstValues.KENDO_CLUB_ATTACK_04_VALUE;
        AttackSe05 = ConstValues.KENDO_CLUB_ATTACK_05_VALUE;
        DrawBattleSuccesessSe = ConstValues.KENDO_CLUB_DRAW_BATTLE_VALUE;

        var load_sprite = Addressables.LoadAssetAsync<Sprite>(AddressablesNames.KENDO_CLUB_ICON);
        // îÒìØä˙Ç≈ÇÃèàóùÇ…Ç¬Ç¢ÇƒèIóπÇë“Ç¬
        var s = load_sprite.WaitForCompletion();
        sprite.sprite = s;

        PlayableSeManager.LoadVoice(PlayableCharacterNumber.Number.KendoClub);
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

    }

}
