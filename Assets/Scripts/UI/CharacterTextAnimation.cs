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
    float message_speed;
    float cp_speed;
    [SerializeField]
    string message;
    int index = 0;
    int message_index = 0;
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
        index = 0;
        message_index = 0;
        message = story.list[message_index].serif_message;
        t.text = "";
        cp_speed = message_speed;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAction();
        message_speed -= Time.deltaTime;
        if (message_speed <= 0.0f)
        {
            message_speed = cp_speed;
            if (message_index >= message.Length)
            {
                return;
            }
            message_index++;
            t.text = message.Substring(0, message_index);
        }
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
            return;
        }
        t.text = "";
        message_index = 0;
        message = StoryManager.Instance.Story.list[StoryManager.Instance.Index].serif_message;
    }

    void BattleStart()
    {
        Debug.Log("バトルスタート");
        SceneManager.LoadScene(ConstValues.BATTLE_SCENE);
    }
}
