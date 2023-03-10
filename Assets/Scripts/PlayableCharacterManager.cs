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
        var goal_pos = new Vector3();
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
        playable_player.Ini(instance);
        playable_player.IsEnemy = false;
        Add(instance,playable_player);

        instance = MonoBehaviour.Instantiate(bulletPrefab);
        pos = goal_pos;
        pos.x = goal_pos.x / 6;
        pos.z = 10;
        instance.transform.position = Camera.main.ScreenToWorldPoint(pos);
        PlayableCharacterIcon a = new PlayableCharacterIcon();
        a.Ini(instance);
        a.IsEnemy = false;
        Add(instance, a);

        // 敵
        instance = MonoBehaviour.Instantiate(bulletPrefab);
        pos = goal_pos;
        pos.x = goal_pos.x / 1.3f;
        pos.z = 10;
        instance.transform.position = Camera.main.ScreenToWorldPoint(pos);
        Vector3 convert = instance.transform.position;
        Debug.Log(convert);
        convert.x = -convert.x;
        instance.transform.position = convert;
        Debug.Log(convert);

        PlayableCharacterIcon e = new PlayableCharacterIcon();
        e.Ini(instance);
        e.IsEnemy = true;
        Add(instance, e);



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
            Debug.LogWarning(player_goal_list.Count + ":::" + enemy_goal_list.Count);
            Debug.LogWarning("プレイヤーアクション");
            is_update = false;
            PlayerManager.InstancePlayerManger.updateSet(true);
        }
        else if (enemy_goal_list.Count != 0)
        {
            Debug.LogWarning("エネミーアクション");
            is_update = false;
            EnemyManager.InstanceEnemyManager.updateSet(true);
        }

    }

    void DrawAction()
    {

    }
}
