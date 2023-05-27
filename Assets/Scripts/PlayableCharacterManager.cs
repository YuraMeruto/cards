using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayableCharacterManager : UpdateBase
{
    Dictionary<int, PlayableCharacterIconBase> playable_character_list = new Dictionary<int, PlayableCharacterIconBase>();
    GameObject goal_object;
    public GameObject GoalObject { get { return goal_object; }}
    bool is_update = true;
    public bool IsUpdate { get { return is_update; } set { is_update = value; } }
    List<PlayableCharacterIconBase> player_goal_list = new List<PlayableCharacterIconBase>();
    List<PlayableCharacterIconBase> enemy_goal_list = new List<PlayableCharacterIconBase>();
    List<PlayableCharacterIconBase> action_playable_list = new List<PlayableCharacterIconBase>();
    Vector3 goal_pos = new Vector3();

    public Vector3 GoalPos { get { return goal_pos; }}
    public Dictionary<int, PlayableCharacterIconBase> PlayableIconList { get { return playable_character_list; } }
    public List<PlayableCharacterIconBase> ActionPlayableList { get { return action_playable_list; } }

    public List<PlayableCharacterIconBase> PlayerGoalList { get { return player_goal_list; } }
    public List<PlayableCharacterIconBase> EnemyGoalList { get { return enemy_goal_list; } }


    static PlayableCharacterManager playableCharacterManager;
    public static PlayableCharacterManager Instance { get { return playableCharacterManager; } }
    public void Add(GameObject obj, PlayableCharacterIconBase add)
    {
        playable_character_list.Add(obj.GetInstanceID(),add);
    }

    public void Ini()
    {
        playableCharacterManager = this;
        // ゴールオブジェクト
        // カードのオブジェクト生成
        var bulletPrefabHundle = Addressables.LoadAssetAsync<GameObject>(AddressablesNames.GOAL_LINE);
        // 非同期での処理について終了を待つ
        var bulletPrefab = bulletPrefabHundle.WaitForCompletion();
        var instance = MonoBehaviour.Instantiate(bulletPrefab);
        goal_pos = new Vector3();
        goal_pos.x = Screen.width / 2;
        goal_pos.y = Screen.height / 1.2f;
        goal_pos.z = ConstValues.PLAYABKE_POS_Z;
        var screen_pos = Camera.main.ScreenToWorldPoint(goal_pos);
        screen_pos.z = ConstValues.PLAYABKE_POS_Z;
        instance.transform.position = screen_pos;
        goal_object = instance;
        LoadPlayableStage.LoadPlayable();

        is_update = false;
    }

    public override void Update()
    {
        if (!is_update)
        {
            return;
        }
        foreach (var playable in playable_character_list)
        {
            playable.Value.Update();
        }
        // ゴールにたどり着いたか
        foreach (var playable in playable_character_list)
        {
            if (Utill.IsGoalPos(playable.Value.PlayableObject,goal_object,playable.Value.IsEnemy))
            {
                if (playable.Value.IsEnemy)
                {
                    enemy_goal_list.Add(playable.Value);
                }
                else
                {
                    player_goal_list.Add(playable.Value);
                }
            }
        }
        // どちらもゴールではない
        if (player_goal_list.Count == 0 && enemy_goal_list.Count == 0)
        {
            return;
        }
        // 両方ともゴールした
        else if (player_goal_list.Count != 0 && enemy_goal_list.Count != 0)
        {
            Debug.LogWarning("ドロー");
            is_update = false;
            PlayerManager.InstancePlayerManger.updateSet(true);
            DrawAction();
            TurnManager.TurnStatus = TurnManager.Turn.Draw;
            TurnTextAnimation.Instance.Start(TurnManager.TurnStatus);


        }
        // 行動開始
        // プレイヤーの行動
        else if (player_goal_list.Count != 0)
        {
            foreach (var val in player_goal_list)
            {
                action_playable_list.Add(val);
            }
            Debug.LogWarning("プレイヤーアクション");
            PlayerManager.InstancePlayerManger.PlayerAction.Status = PlayerAction.ActionStatus.CardSelect;
            is_update = false;
            PlayerManager.InstancePlayerManger.updateSet(true);
            TurnManager.TurnStatus = TurnManager.Turn.Player;
            TurnTextAnimation.Instance.Start(TurnManager.TurnStatus);
            CardManager.OnEffectCard();
        }
        else if (enemy_goal_list.Count != 0)
        {
            foreach (var val in enemy_goal_list)
            {
                action_playable_list.Add(val);
            }
            Debug.LogWarning("エネミーアクション");
            is_update = false;
            EnemyManager.InstanceEnemyManager.updateSet(true);
            TurnManager.TurnStatus = TurnManager.Turn.Enemy;
            TurnTextAnimation.Instance.Start(TurnManager.TurnStatus);
        }

    }

    public List<PlayableCharacterIconBase> getEnemyIconList()
    {
        List<PlayableCharacterIconBase> l = new List<PlayableCharacterIconBase>();
        foreach (var val in playable_character_list)
        {
            if (val.Value.IsEnemy)
            {
                l.Add(val.Value);
            }
        }
        return l;
    }

    public List<PlayableCharacterIconBase> getPlayerIconList()
    {
        List<PlayableCharacterIconBase> l = new List<PlayableCharacterIconBase>();
        foreach (var val in playable_character_list)
        {
            if (!val.Value.IsEnemy)
            {
                l.Add(val.Value);
            }
        }
        return l;
    }

    public PlayableCharacterIconBase GetPlayable(GameObject obj)
    {
        return PlayableIconList[obj.GetInstanceID()];
    }

    /// <summary>
    /// 同着した時の処理
    /// </summary>
    void DrawAction()
    {
        Debug.Log("DrawAction");
        PlayerManager.InstancePlayerManger.PlayerAction.Status = PlayerAction.ActionStatus.DrawCardSelect;
        DrawBattleManager.Instance.InstanceDrawCards();
    }

    // Relocation　再配置
    public void Relocation()
    {
        foreach (var val in player_goal_list)
        {
            var pos = goal_pos;
            pos.x = goal_pos.x / val.ReLocation;
            pos.z = ConstValues.PLAYABKE_POS_Z;
            var screen_pos = Camera.main.ScreenToWorldPoint(pos);
            screen_pos.z = ConstValues.PLAYABKE_POS_Z;
            screen_pos.y = val.PlayableObject.transform.position.y;
            val.PlayableObject.transform.position = screen_pos;

        }
        foreach (var val in enemy_goal_list)
        {
           var pos = goal_pos;
            pos.x = goal_pos.x / val.ReLocation;
            pos.z = ConstValues.PLAYABKE_POS_Z;
            var screen_pos = Camera.main.ScreenToWorldPoint(pos);
            screen_pos.y = val.PlayableObject.transform.position.y;
            screen_pos.z = ConstValues.PLAYABKE_POS_Z;
            val.PlayableObject.transform.position = screen_pos;

            var convert = val.PlayableObject.transform.position;
            convert.x = -convert.x;
            val.PlayableObject.transform.position = convert;
        }
        enemy_goal_list.Clear();
        player_goal_list.Clear();
        action_playable_list.Clear();
        is_update = true;
    }

    public void SetRelocation(PlayableCharacterIconBase t)
    {
        var pos = goal_pos;
        pos.x = goal_pos.x / t.ReLocation;
        pos.z = ConstValues.PLAYABKE_POS_Z;
        var screen_pos = Camera.main.ScreenToWorldPoint(pos);
        screen_pos.z = ConstValues.PLAYABKE_POS_Z;
        screen_pos.y =  t.PlayableObject.transform.position.y;
        if (t.IsEnemy)
        {
            screen_pos.x = -screen_pos.x;
        }
        t.PlayableObject.transform.position = screen_pos;
    }

    public void UpdateRestart()
    {
        enemy_goal_list.Clear();
        player_goal_list.Clear();
        action_playable_list.Clear();
        is_update = true;
    }
}
