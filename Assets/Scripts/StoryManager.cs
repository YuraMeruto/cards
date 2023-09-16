using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StoryManager
{
    static StoryManager instance;
    public static StoryManager Instance { get { return instance; } }

    int index;

    public int Index { get { return index; } }
    Image left_image;
    Image right_image;
    Image center_image;
    Image back_groud_image;

    Text serif_charater_name_text;

    Story story;
    public Story Story { get { return story; } }

    public void Ini()
    {
        instance = this;
        left_image = GameObject.Find("LeftCharacterImage").GetComponent<Image>();
        right_image = GameObject.Find("RightCharacterImage").GetComponent<Image>();
        center_image = GameObject.Find("CenterCharacterImage").GetComponent<Image>();

        back_groud_image = GameObject.Find("BackGroundImage").GetComponent<Image>();
        serif_charater_name_text = GameObject.Find("SerifCharacterNameText").GetComponent<Text>();
    }

    public Story LoadStory(string load_story)
    {
        var asset = Addressables.LoadAssetAsync<Story>(load_story);
        story = asset.WaitForCompletion();
        return story;
    }

    public void LoadAsset()
    {
        ScenarioIconManager.SpriteList.Clear();
        foreach (var param in story.list)
        {
            ScenarioIconManager.LoadCharacterSprite(param.left_character_sprite_number, param.left_character_type);
            ScenarioIconManager.LoadCharacterSprite(param.right_character_sprite_number, param.right_character_type);
            ScenarioIconManager.LoadCharacterSprite(param.center_character_sprite_number, param.center_character_type);
            ScenarioIconManager.LoadBackGroundSprite(param.back_ground_sprite_number);
        }
    }

    public void Next()
    {
        if (index >= StoryManager.Instance.Story.list.Count)
        {
            BattleStart();
            return;
        }
        index++;
        if (index >= StoryManager.Instance.Story.list.Count)
        {
            BattleStart();
            return;
        }
        SetSprites();
        SetSerifCharacterNameText();
    }

    void SetSprites()
    {
        // 画像非表示の処理
        if (story.list[index].left_character_sprite_number == ConstValues.NONE_SPRITE_VALUE)
        {
            left_image.sprite = null;
            left_image.color = Color.clear;
        }
        else if (story.list[index].left_character_sprite_number != 0)
        {
            left_image.color = Color.white;
            left_image.sprite = ScenarioIconManager.SpriteList[story.list[index].left_character_sprite_number];

        }
        if (story.list[index].right_character_sprite_number == ConstValues.NONE_SPRITE_VALUE)
        {
            right_image.sprite = null;
            right_image.color = Color.clear;
        }
        else if(story.list[index].right_character_sprite_number != 0)
        {
            right_image.color = Color.white;
            right_image.sprite = ScenarioIconManager.SpriteList[story.list[index].right_character_sprite_number];
        }

        if (story.list[index].center_character_sprite_number == ConstValues.NONE_SPRITE_VALUE)
        {
            center_image.sprite = null;
            center_image.color = Color.clear;
        }
        else if (story.list[index].center_character_sprite_number != 0)
        {
            center_image.color = Color.white;
            center_image.sprite = ScenarioIconManager.SpriteList[story.list[index].center_character_sprite_number];
        }


        // 背景
        if (story.list[index].back_ground_sprite_number != 0)
        {
            back_groud_image.sprite = ScenarioIconManager.SpriteList[story.list[index].back_ground_sprite_number];
        }
        SetSpritesForSerifImage();
    }

    void SetSpritesForSerifImage()
    {

    }

    void SetSerifCharacterNameText()
    {
        serif_charater_name_text.text = CharacterNames.GetName(story.list[index].serif_character_type);
    }

    void BattleStart()
    {
        Debug.Log("バトルスタート");
        return;
        SceneManager.LoadScene(ConstValues.BATTLE_SCENE);
    }

    public void IniSetting()
    {
        SetSprites();
        SetSerifCharacterNameText();
    }
}
