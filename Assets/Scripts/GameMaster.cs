using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    static GameObject game_master_obj;
    static GameMaster game_master;
    UpdateList update_list = new UpdateList();

    public static GameObject GameMasterObj { get { return game_master_obj; } }
    public static GameMaster GameMasterClass { get { return game_master; } }

    // Start is called before the first frame update
    void Start()
    {
        Ini();
        Debug.Log("GameStart");
        game_master_obj = this.gameObject;
        game_master = this;
        CardManager.instanceCards();
        Utill.Hoge();

    }

    // Update is called once per frame
    void Update()
    {
        update_list.Update();
//        Debug.Log("GameUpdate");
    }


    private void Ini()
    {
        DontDestroyOnLoad(gameObject);
        PlayerManager player_manger = new PlayerManager();
        player_manger.Ini();
        update_list.Add(player_manger);
    }
}
