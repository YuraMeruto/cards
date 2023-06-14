using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class ScenarioIconManager
{
    public enum CharacterType
    {
        None,
        GoHome,
        Kendo,
    }
    static Dictionary<int, Sprite> sprite_list = new Dictionary<int, Sprite>();
    public static Dictionary<int, Sprite> SpriteList { get { return sprite_list; } }
    public static void LoadCharacterSprite(int icon_number,CharacterType type)
    {
        var directory = "";
        switch (type)
        {
            case CharacterType.GoHome:
                directory = "GoHome/"+"Test" + icon_number.ToString() + ".png";
                Load(icon_number, directory);
                break;
            case CharacterType.Kendo:
                directory = "KendoClub/" + "Test" + icon_number.ToString() + ".png";
                Load(icon_number, directory);
                break;
        }
    }

    public static void LoadBackGroundSprite(int back_ground_number)
    {
        Load(back_ground_number, "BackGround/Test"+back_ground_number.ToString()+".png");
    }

    static void Load(int number, string address_name)
    {
        if (sprite_list.ContainsKey(number))
        {
            return;
        }
        if (number == 0 || number == ConstValues.NONE_SPRITE_VALUE)
        {
            return;
        }
        Debug.Log("ƒAƒhƒŒƒX::"+address_name);
        var snd = Addressables.LoadAssetAsync<Sprite>(address_name);
        var a = snd.WaitForCompletion();
        sprite_list.Add(number, a);
    }
}
