using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerifTextParam
{
    Image serif_text_image;
    Text text;
    float message_speed;
    float cp_speed;
    string message;
    int message_index = 0;
    List<string> st_list = new List<string>();
    string show_message;
    int list_index = 0;
    int split_size;
    bool is_window_text;
    public void Ini(Text text_obj,float message_speed_value,string serif_message,int split_size_value,bool is_window,Image text_image)
    {
        text = text_obj;
        message_speed = message_speed_value;
        message_index = 0;
        message = serif_message;
        cp_speed = message_speed;
        text.text = "";
        show_message = serif_message;
        split_size = split_size_value;
        is_window_text = is_window;
        serif_text_image = text_image;
        SetSplitMessage();
        SetImageColor();
    }

    public void Update()
    {
        message_speed -= Time.deltaTime;
        if (message_speed <= 0.0f)
        {
            message_speed = cp_speed;
            if (message_index >= show_message.Length)
            {
                if (list_index + 1 != st_list.Count && st_list.Count != 0)
                {
                    NextLineMessage();
                }
                return;
            }
            message_index++;
            text.text = show_message.Substring(0, message_index);
        }
    }

    public void NextMessageSetUp(string serif_message)
    {
        text.text = "";
        message_index = 0;
        message = serif_message;
        show_message = serif_message;
        SetSplitMessage();
        SetImageColor();
    }

    void SetImageColor()
    {
        if (!is_window_text && message == "")
        {
            Debug.Log("fuga");
            serif_text_image.color = Color.clear;
            Debug.Log(serif_text_image.gameObject.name);
        }
        else if (!is_window_text)
        {
            Debug.Log("hoge");
            serif_text_image.color = Color.white;
        }
    }

    void SetSplitMessage()
    {
        if (message == "")
        {
            Debug.Log("null‚Û");
            return;
        }
        st_list.Clear();
        int size = 0;
        for (var i = 0; i < message.Length; i += split_size)
        {
            size = split_size;
            if (i + split_size >= message.Length)
            {
                Debug.Log(i.ToString() + ":::" + (int)message.Length);
                size = Mathf.Abs((int)message.Length - i);

            }
            st_list.Add(message.Substring(i, size));
        }
        show_message = st_list[0];
    }

    void NextLineMessage()
    {
        Debug.Log(list_index+"::::"+st_list.Count);
        list_index++;
        show_message += "\n";
        show_message += st_list[list_index];
    }
}
