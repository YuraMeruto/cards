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
        // �S�[���I�u�W�F�N�g
        // �J�[�h�̃I�u�W�F�N�g����
        var bulletPrefabHundle = Addressables.LoadAssetAsync<GameObject>(AddressablesNames.GOAL_LINE);
        // �񓯊��ł̏����ɂ��ďI����҂�
        var bulletPrefab = bulletPrefabHundle.WaitForCompletion();
        var instance = MonoBehaviour.Instantiate(bulletPrefab);
        var goal_pos = new Vector3();
        goal_pos.x = Screen.width / 2;
        goal_pos.y = Screen.height / 1.2f;
        goal_pos.z = 10;
        instance.transform.position = Camera.main.ScreenToWorldPoint(goal_pos);
        goal_object = instance;


        // �v���C�A�u������
        bulletPrefabHundle = Addressables.LoadAssetAsync<GameObject>(AddressablesNames.PLAYABLE_CHARACTER);
        // �񓯊��ł̏����ɂ��ďI����҂�
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

        // �G
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
        // �S�[���ɂ��ǂ蒅������
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
        // �ǂ�����S�[���ł͂Ȃ�
        if (player_goal_list.Count == 0 && enemy_goal_list.Count == 0)
        {
            return;
        }
        // �����Ƃ��S�[������
        else if (player_goal_list.Count != 0 && enemy_goal_list.Count != 0)
        {
            Debug.LogWarning("�h���[");
            is_update = false;
            DrawAction();
        }
        // �s���J�n
        // �v���C���[�̍s��
        else if (player_goal_list.Count != 0)
        {
            Debug.LogWarning(player_goal_list.Count + ":::" + enemy_goal_list.Count);
            Debug.LogWarning("�v���C���[�A�N�V����");
            is_update = false;
            PlayerManager.InstancePlayerManger.updateSet(true);
        }
        else if (enemy_goal_list.Count != 0)
        {
            Debug.LogWarning("�G�l�~�[�A�N�V����");
            is_update = false;
            EnemyManager.InstanceEnemyManager.updateSet(true);
        }

    }

    void DrawAction()
    {

    }
}
