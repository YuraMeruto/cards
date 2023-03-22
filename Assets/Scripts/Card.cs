using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class Card
{
    CardManager.Number number;
    CardManager.ShowStatus show_status;
    SpriteRenderer sprite_render;

    public CardManager.Number CardType { get { return number; } set { number = value; } }
    private GameObject game_obj;

    public GameObject GameObject { get { return game_obj; } }

    public void Ini(CardManager.Number num, GameObject obj, CardManager.ShowStatus showStatus)
    {
        number = num;
        sprite_render = obj.GetComponent<SpriteRenderer>();
        game_obj = obj;
        show_status = showStatus;
    }

    public void ChangeShowStatus()
    {
        var load_sprite_name = "";
        if (show_status == CardManager.ShowStatus.Front)
        {
            show_status = CardManager.ShowStatus.Back;
            load_sprite_name = AddressablesNames.BACK_SPRITE;
        }
        else
        {
            show_status = CardManager.ShowStatus.Front;
            load_sprite_name = CardManager.GetCardSpriteFront(number);
        }

        // �J�[�h�̃I�u�W�F�N�g����
        var load_sprite = Addressables.LoadAssetAsync<Sprite>(load_sprite_name);

        // �񓯊��ł̏����ɂ��ďI����҂�
        var sprite = load_sprite.WaitForCompletion();
        sprite_render.sprite = sprite;
    }
}
