using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;


public class Test : MonoBehaviour
{
    float message_speed = 0.01f;
    float cp_speed;
    int message_index;
    [SerializeField]
    string message;
    Text text;
    [SerializeField]
    int size;
    [SerializeField]
    List<string> st_list = new List<string>();
    string show_message;
    int list_index = 0;
    void Start()
    {
        message_index = 0;
        cp_speed = message_speed;
        text = GetComponent<Text>();

        for (var i =0; i < message.Length; i+= size)
        {
            if (i+size >= message.Length)
            {
                Debug.Log(i.ToString()+":::"+ (int)message.Length);
                size = Mathf.Abs((int)message.Length - i);
               
            }
            Debug.Log(size);
            Debug.Log(message.Substring(i, size));
            st_list.Add(message.Substring(i, size));
            show_message = st_list[0];
        }

    }
    void Update()
    {
        message_speed -= Time.deltaTime;
        if (message_speed <= 0.0f)
        {
            message_speed = cp_speed;
            if (message_index >= show_message.Length)
            {
                if (list_index + 1 != st_list.Count)
                {
                    Next();
                }
                return;
            }
            message_index++;
            text.text = show_message.Substring(0, message_index);
        }

    }

    void Next()
    {
        list_index++;
        show_message += "\n";
        show_message += st_list[list_index];
    }
}
