using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    static GameObject game_master_obj;
    static GameMaster game_master;
    UpdateList update_list = new UpdateList();
    AnimationUpdateList animation_update_list = new AnimationUpdateList();
    public AnimationUpdateList animationUpdateList { get { return animation_update_list; } set { animation_update_list = value; ; } }

    public static GameObject GameMasterObj { get { return game_master_obj; } }
    public static GameMaster GameMasterClass { get { return game_master; } }

    // Start is called before the first frame update
    void Start()
    {
        Ini();
        Debug.Log("GameStart");
        game_master_obj = this.gameObject;
        game_master = this;
        CardManager.InstanceCards();
        Utill.Hoge();

    }

    // Update is called once per frame
    void Update()
    {
        update_list.Update();
        animation_update_list.Update();
//        Debug.Log("GameUpdate");
    }


    private void Ini()
    {
        DontDestroyOnLoad(gameObject);
        PlayerManager player_manger = new PlayerManager();
        player_manger.Ini();
        update_list.Add(player_manger);

        EnemyManager enemy_manger = new EnemyManager();
        enemy_manger.Ini();
        update_list.Add(enemy_manger);

        UIManager ui = new UIManager();
        ui.Ini();

        ActionTimeManger time_action_maneger = new ActionTimeManger();
        time_action_maneger.Ini();
        update_list.Add(time_action_maneger);

        TurnManager turn_manager = new TurnManager();
        update_list.Add(turn_manager);
        TurnManager.TurnStatus = TurnManager.Turn.Player;
        turn_manager.Ini();

        PlayableCharacterManager playablemanager = new PlayableCharacterManager();
        playablemanager.Ini();
        update_list.Add(playablemanager);

        TargetIconManager target_manager = new TargetIconManager();
        target_manager.Ini();

        BattleManager battle_manager = new BattleManager();
        battle_manager.Ini();
    }
}
