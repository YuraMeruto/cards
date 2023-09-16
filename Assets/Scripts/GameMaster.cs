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
//        Screen.SetResolution(1280, 960, FullScreenMode.Windowed, 60);
        Debug.LogWarning(Screen.width);
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
        DebugMethod();
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
        TurnManager.TurnStatus = TurnManager.Turn.None;
        turn_manager.Ini();

        PlayableCharacterManager playablemanager = new PlayableCharacterManager();
        playablemanager.Ini();
        update_list.Add(playablemanager);

        TargetIconManager target_manager = new TargetIconManager();
        target_manager.Ini();

        BattleManager battle_manager = new BattleManager();
        battle_manager.Ini();

        CardChangeManager card_change_manager = new CardChangeManager();
        card_change_manager.Ini();

        BattleCalucation battle_calucation = new BattleCalucation();
        battle_calucation.Ini();

        ComboManager combo_manager = new ComboManager();
        combo_manager.Ini();
        update_list.Add(combo_manager);

        DrawBattleManager draw_battle_manager = new DrawBattleManager();
        draw_battle_manager.Ini();

        DrawBattleUI draw_battle_ui = new DrawBattleUI();
        draw_battle_ui.Ini();
        update_list.Add(draw_battle_ui);

        CommonSeManager.Ini();

        var turn = new TurnTextAnimation();
        turn.Ini();
        update_list.Add(turn);

        var playable_move_postion_animation = new PlayableMovePostionAnimation();
        playable_move_postion_animation.Ini();
        update_list.Add(playable_move_postion_animation);

        var chain_text = new ChainTextAnimation();
        chain_text.Ini();
        update_list.Add(chain_text);

    }

    void DebugMethod()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.LogWarning(TurnManager.TurnStatus);
            Debug.LogWarning(PlayerManager.InstancePlayerManger.PlayerAction.Status);
            Debug.LogWarning(PlayerManager.InstancePlayerManger.PlayerAction.IsUpdate);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            foreach (var val in PlayableSeManager.VoiceList)
            {
                Debug.LogWarning(val.Key);
                Debug.LogWarning(val.Value);

            }
        }
    }
}
