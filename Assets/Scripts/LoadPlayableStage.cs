using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LoadPlayableStage
{

    static int load_stage_number = 1;

    public static int LoadStageNumber { get { return load_stage_number; } set { load_stage_number = value; } }

    public static void LoadPlayable()
    {
        switch (load_stage_number)
        {
            case ConstValues.FIRST_STAGE:
                LoadFirstStage();
                break;
        }
    }


    static void LoadFirstStage()
    {
        // プレイアブル生成
        var bulletPrefabHundle = Addressables.LoadAssetAsync<GameObject>(AddressablesNames.PLAYABLE_CHARACTER);
        // 非同期での処理について終了を待つ
        var bulletPrefab = bulletPrefabHundle.WaitForCompletion();

        // プレイヤー
        LoadGoHomePlayable(bulletPrefab, 0,false,6,1.1f);
        LoadGoHomePlayable(bulletPrefab, -1, false, 6, 2f);
        // エネミー
        LoadKendoClubPlaybale(bulletPrefab, 0, true, 1.5f, 1.2f);
        LoadKendoClubPlaybale(bulletPrefab, -1, true, 1.5f, 2.1f);
    }

    static void LoadGoHomePlayable(GameObject bulletPrefab,float pos_y,bool is_enemy,float r_location,float init_pos_x)
    {
        var instance = MonoBehaviour.Instantiate(bulletPrefab);

        var pos = PlayableCharacterManager.Instance.GoalPos;
        pos.x = PlayableCharacterManager.Instance.GoalPos.x / init_pos_x;
        pos.z = ConstValues.PLAYABKE_POS_Z;
        var screen_pos = Camera.main.ScreenToWorldPoint(pos);
        screen_pos.z = ConstValues.PLAYABKE_POS_Z;
        if (pos_y != 0)
        {
            screen_pos.y += pos_y;
        }
        instance.transform.position = screen_pos;
        GoHomeClub a = new GoHomeClub();
        if (is_enemy)
        {
            instance.tag = TagManager.ENEMY;
            instance.transform.position = ConvertPos(instance.transform.position);
        }
        a.Ini(instance, r_location);
        a.IsEnemy = is_enemy;
        PlayableCharacterManager.Instance.Add(instance, a);
    }

    static void LoadKendoClubPlaybale(GameObject bulletPrefab, float pos_y,bool is_enemy,float r_location,float init_pos_x)
    {
        var instance = MonoBehaviour.Instantiate(bulletPrefab);
        var pos = PlayableCharacterManager.Instance.GoalPos;
        pos.x = PlayableCharacterManager.Instance.GoalPos.x / init_pos_x;
        pos.z = ConstValues.PLAYABKE_POS_Z;
        var screen_pos = Camera.main.ScreenToWorldPoint(pos);
        screen_pos.z = ConstValues.PLAYABKE_POS_Z;
        if (pos_y != 0)
        {
            screen_pos.y += pos_y;
        }
        instance.transform.position = screen_pos;
        if (is_enemy)
        {
            instance.tag = TagManager.ENEMY;
            instance.transform.position = ConvertPos(instance.transform.position);
        }
        KendoClub e = new KendoClub();
        e.Ini(instance, r_location);
        e.IsEnemy = is_enemy;
        PlayableCharacterManager.Instance.Add(instance, e);
    }

    static Vector3 ConvertPos(Vector3 pos)
    {
        Vector3 convert = pos;
        convert.x = -convert.x;
        return convert;
    }
}
