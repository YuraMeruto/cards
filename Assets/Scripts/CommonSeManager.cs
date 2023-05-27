using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CommonSeManager 
{
    static Dictionary<int, AudioClip> se_list = new Dictionary<int, AudioClip>();
    public static Dictionary<int, AudioClip> VoiceList { get { return se_list; } }

    static AudioSource source;

    public static void Ini()
    {
        source = GameObject.Find("CommonSeObject").GetComponent<AudioSource>();
        Load();
    }

    static void Load()
    {
        DrawBattleSe();
    }

    static void DrawBattleSe()
    {
        LoadClip(ConstValues.DRAW_BATTLE_RESULT_WIN_VALUE,ConstValues.DRAW_BATTLE_RESULT_WIN);
    }

    public static void PlaySe(int number)
    {
        source.clip = se_list[number];
        source.Play();
    }

    static void LoadClip(int number, string address_name)
    {
        if (se_list.ContainsKey(number))
        {
            return;
        }
        var snd = Addressables.LoadAssetAsync<AudioClip>(address_name);
        var a = snd.WaitForCompletion();
        se_list.Add(number, a);
    }

}
