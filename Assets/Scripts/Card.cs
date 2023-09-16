using UnityEngine;
using UnityEngine.AddressableAssets;
public class Card
{
    CardManager.Number number;
    CardManager.ShowStatus show_status;
    SpriteRenderer sprite_render;
    GameObject particle_object;

    public CardManager.Number CardType { get { return number; } set { number = value; } }
    private GameObject game_obj;

    public GameObject GameObject { get { return game_obj; } }

    public void Ini(CardManager.Number num, GameObject obj, CardManager.ShowStatus showStatus,bool is_partical_active = false)
    {
        number = num;
        sprite_render = obj.GetComponent<SpriteRenderer>();
        game_obj = obj;
        show_status = showStatus;
        foreach(ParticleSystem child in obj.GetComponentsInChildren<ParticleSystem>())
        {
            if (child.gameObject.name == ConstValues.RIGHT_EFFECT_OBJECT_NAME)
            {
                particle_object = child.gameObject;
            }
        }
        SetActiveRightEffect(is_partical_active);
    }

    public void ChangeShowStatus()
    {
        var load_sprite_name = "";
        if (show_status == CardManager.ShowStatus.Front)
        {
            show_status = CardManager.ShowStatus.Back;
            load_sprite_name = AddressablesNames.BACK_SPRITE;
//            particle_object.SetActive(true);
        }
        else
        {
            show_status = CardManager.ShowStatus.Front;
            load_sprite_name = CardManager.GetCardSpriteFront(number);
//            particle_object.SetActive(false);
        }

        // カードのオブジェクト生成
        var load_sprite = Addressables.LoadAssetAsync<Sprite>(load_sprite_name);

        // 非同期での処理について終了を待つ
        var sprite = load_sprite.WaitForCompletion();
        sprite_render.sprite = sprite;
    }

    public void SetActiveRightEffect(bool active)
    {
        particle_object.SetActive(active);
    }

    public int ToIntNumber()
    {
        return ((int)number + 1);
    }
}
