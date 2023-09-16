using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class CharacterTextAnimation : MonoBehaviour
{
    [SerializeField]
    Text t;
    [SerializeField]
    Text right_serif_text;
    [SerializeField]
    Text left_serif_text;
    [SerializeField]
    Text center_serif_text;
    [SerializeField]
    float message_speed;
    float cp_speed;
    [SerializeField]
    string message;
    [SerializeField]
    string left_serif_message;
    [SerializeField]
    string right_serif_message;
    [SerializeField]
    string center_serif_message;
    List<SerifTextParam> serif_param_list = new List<SerifTextParam>();
    SerifTextParam window_serif_text_param = new SerifTextParam();
    SerifTextParam left_serif_text_param = new SerifTextParam();
    SerifTextParam right_serif_text_param = new SerifTextParam();
    SerifTextParam center_serif_text_param = new SerifTextParam();
    // Start is called before the first frame update
    void Start()
    {
        var story_manager = new StoryManager();
        story_manager.Ini();
        Ini();
    }
    void Ini()
    {
        var story = StoryManager.Instance.LoadStory(AddressablesNames.TUTORIAL_STORY);
        StoryManager.Instance.LoadAsset();
        StoryManager.Instance.IniSetting();
        serif_param_list.Clear();
        Text w = GameObject.Find("WindowSerifText").GetComponent<Text>();
        Image window_image = GameObject.Find("WindowSerifTextImage").GetComponent<Image>();
        window_serif_text_param.Ini(w,message_speed, story.list[0].serif_message,ConstValues.SERIF_SPLIT_SIZE_FOR_WINDOW,true, window_image);

        Text left = GameObject.Find("LeftSerifText").GetComponent<Text>();
        Image left_image = GameObject.Find("LeftSerifTextImage").GetComponent<Image>();
        left_serif_text_param.Ini(left, message_speed, story.list[0].left_serif_message, ConstValues.SERIF_SPLIT_SIZE,false,left_image);
        Text right = GameObject.Find("RightSerifText").GetComponent<Text>();
        Image right_image = GameObject.Find("RightSerifTextImage").GetComponent<Image>();
        right_serif_text_param.Ini(right, message_speed, story.list[0].right_serif_message, ConstValues.SERIF_SPLIT_SIZE,false,right_image);
        Text center = GameObject.Find("CenterSerifText").GetComponent<Text>();
        Image center_image = GameObject.Find("CenterSerifTextImage").GetComponent<Image>();
        center_serif_text_param.Ini(center, message_speed, story.list[0].center_serif_message, ConstValues.SERIF_SPLIT_SIZE,false,center_image);
    }

    void Update()
    {
        PlayerAction();
        window_serif_text_param.Update();
        left_serif_text_param.Update();
        right_serif_text_param.Update();
        center_serif_text_param.Update();
    }

    void PlayerAction()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextMessage();
        }
    }

    void NextMessage()
    {
        StoryManager.Instance.Next();
        if (StoryManager.Instance.Index >= StoryManager.Instance.Story.list.Count)
        {
            BattleStart();
           return;
        }
        window_serif_text_param.NextMessageSetUp(StoryManager.Instance.Story.list[StoryManager.Instance.Index].serif_message);
        left_serif_text_param.NextMessageSetUp(StoryManager.Instance.Story.list[StoryManager.Instance.Index].left_serif_message);
        right_serif_text_param.NextMessageSetUp(StoryManager.Instance.Story.list[StoryManager.Instance.Index].right_serif_message);
        center_serif_text_param.NextMessageSetUp(StoryManager.Instance.Story.list[StoryManager.Instance.Index].center_serif_message);

        return;
    }

    void BattleStart()
    {
        Debug.Log("バトルスタート");
        SceneManager.LoadScene(ConstValues.BATTLE_SCENE);
    }
}
