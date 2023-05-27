using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class PlayableSeManager
{
    static Dictionary<int, AudioClip> voice_list = new Dictionary<int, AudioClip>();
    public static Dictionary<int, AudioClip> VoiceList { get { return voice_list; } }
    public static void LoadVoice(PlayableCharacterNumber.Number character_number)
    {
        switch (character_number)
        {
            case PlayableCharacterNumber.Number.KendoClub:
                LoadGoHomeVoice();
                break;
            case PlayableCharacterNumber.Number.GoHomeClub:
                LoadKendoVoice();
                break;
        }
    }


    static void LoadGoHomeVoice()
    {
        LoadClip(ConstValues.GO_HOME_CLUB_ATTACK_01_VALUE,ConstValues.GO_HOME_CLUB_ATTACK_01);
        LoadClip(ConstValues.GO_HOME_CLUB_ATTACK_02_VALUE, ConstValues.GO_HOME_CLUB_ATTACK_02);
        LoadClip(ConstValues.GO_HOME_CLUB_ATTACK_03_VALUE, ConstValues.GO_HOME_CLUB_ATTACK_03);
        LoadClip(ConstValues.GO_HOME_CLUB_ATTACK_04_VALUE, ConstValues.GO_HOME_CLUB_ATTACK_04);
        LoadClip(ConstValues.GO_HOME_CLUB_ATTACK_05_VALUE, ConstValues.GO_HOME_CLUB_ATTACK_05);
        LoadClip(ConstValues.GO_HOME_CLUB_DRAW_BATTLE_VALUE, ConstValues.GO_HOME_CLUB_DRAW_BATTLE);
    }
    static void LoadKendoVoice()
    {
        LoadClip(ConstValues.KENDO_CLUB_ATTACK_01_VALUE, ConstValues.KENDO_CLUB_ATTACK_01);
        LoadClip(ConstValues.KENDO_CLUB_ATTACK_02_VALUE, ConstValues.KENDO_CLUB_ATTACK_02);
        LoadClip(ConstValues.KENDO_CLUB_ATTACK_03_VALUE, ConstValues.KENDO_CLUB_ATTACK_03);
        LoadClip(ConstValues.KENDO_CLUB_ATTACK_04_VALUE, ConstValues.KENDO_CLUB_ATTACK_04);
        LoadClip(ConstValues.KENDO_CLUB_ATTACK_05_VALUE, ConstValues.KENDO_CLUB_ATTACK_05);
        LoadClip(ConstValues.KENDO_CLUB_DRAW_BATTLE_VALUE, ConstValues.KENDO_CLUB_DRAW_BATTLE);

    }

    static void LoadClip(int number,string address_name)
    {
        if (voice_list.ContainsKey(number))
        {
            return;
        }
        var snd = Addressables.LoadAssetAsync<AudioClip>(address_name);
        var a = snd.WaitForCompletion();
        voice_list.Add(number,a);
    }
}
