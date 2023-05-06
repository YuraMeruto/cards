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

    public Dictionary<int, PlayableCharacterIconBase> PlayableIconList { get { return playable_character_list; } }
    public List<PlayableCharacterIconBase> ActionPlayableList { get { return action_playable_list; } }


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
        goal_pos.z = 10;
        instance.transform.position = Camera.main.ScreenToWorldPoint(goal_pos);
        goal_object = instance;


        // プレイアブル生成
        bulletPrefabHundle = Addressables.LoadAssetAsync<GameObject>(AddressablesNames.PLAYABLE_CHARACTER);
        // 非同期での処理について終了を待つ
        bulletPrefab = bulletPrefabHundle.WaitForCompletion();
        instance = MonoBehaviour.Instantiate(bulletPrefab);
        var pos = goal_pos;
        pos.x = goal_pos.x / 2;
        pos.z = 10;
        instance.transform.position = Camera.main.ScreenToWorldPoint(pos);

        PlayableCharacterIcon playable_player = new PlayableCharacterIcon();
        float r_location = 2;
        playable_player.Ini(instance,r_location);
        playable_player.IsEnemy = false;
        Add(instance,playable_player);

        instance = MonoBehaviour.Instantiate(bulletPrefab);
        pos = goal_pos;
        pos.x = goal_pos.x / 1.1f;
        pos.z = 10;
        instance.transform.position = Camera.main.ScreenToWorldPoint(pos);
        PlayableCharacterIcon a = new PlayableCharacterIcon();
        r_location = 6;
        a.Ini(instance,6);
        a.IsEnemy = false;
        Add(instance, a);

        // 敵
        instance = MonoBehaviour.Instantiate(bulletPrefab);
        instance.tag = TagManager.ENEMY;
        pos = goal_pos;
        pos.x = goal_pos.x / 1.3f;
        pos.z = 10;
        instance.transform.position = Camera.main.ScreenToWorldPoint(pos);
        Vector3 convert = instance.transform.position;
        convert.x = -convert.x;
        instance.transform.position = convert;

        PlayableCharacterIcon e = new PlayableCharacterIcon();
        r_location = 1.5f;
        e.Ini(instance,r_location);
        e.IsEnemy = true;
        Add(instance, e);


        instance = MonoBehaviour.Instantiate(bulletPrefab);
        instance.tag = TagManager.ENEMY;
        pos = goal_pos;
        pos.x = goal_pos.x / 3.3f;
        pos.z = 10;
        var screen_pos = Camera.main.ScreenToWorldPoint(pos);
        screen_pos.y += -1;
        instance.transform.position = screen_pos;
        
        convert = instance.transform.position;
        convert.x = -convert.x;
        instance.transform.position = convert;

        PlayableCharacterIcon f = new PlayableCharacterIcon();
        r_location = 1.5f;
        f.Ini(instance, r_location);
        f.IsEnemy = true;
        Add(instance, f);



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
            DrawAction();
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


    void DrawAction()
    {

    }

    // Relocation　再配置
    public void Relocation()
    {
        foreach (var val in player_goal_list)
        {
            var pos = goal_pos;
            pos.x = goal_pos.x / val.ReLocation;
            pos.z = 10;
            val.PlayableObject.transform.position = Camera.main.ScreenToWorldPoint(pos);

        }
        foreach (var val in enemy_goal_list)
        {
           var pos = goal_pos;
            pos.x = goal_pos.x / val.ReLocation;
            pos.z = 10;
            val.PlayableObject.transform.position = Camera.main.ScreenToWorldPoint(pos);
            var convert = val.PlayableObject.transform.position;
            convert.x = -convert.x;
            val.PlayableObject.transform.position = convert;
        }
        enemy_goal_list.Clear();
        player_goal_list.Clear();
        action_playable_list.Clear();
        is_update = true;
    }

}
