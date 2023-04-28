using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class StartCountAnimationManager
{
    static bool is_animation = true;
    public static bool IsAnimation { get { return is_animation; } set { is_animation = value; } }

    public static void startAnimationInstance()
    {
        Debug.Log("startAnimationInstance");
        if (!is_animation)
        {
            return;
        }
        is_animation = false;



        var count_text = GameObject.Find("StartCountText");
        count_text.SetActive(true);
        Debug.Log("startAnimationInstanc222e");
        StartCountAnimation s = new StartCountAnimation();
        s.Ini(count_text.GetComponent<Text>(), 1.5f, 3);
        GameMaster.GameMasterClass.animationUpdateList.Add(count_text, s);

        return;
        /*
        // カードのオブジェクト生成
        var bulletPrefabHundle = Addressables.LoadAssetAsync<GameObject>(AddressablesNames.UI_START_COUNT_TEXT);

        // 非同期での処理について終了を待つ
        var bulletPrefab = bulletPrefabHundle.WaitForCompletion();
        var instance = MonoBehaviour.Instantiate(bulletPrefab);
        Debug.Log("startAnimationInstanc222e");
        StartCountAnimation s = new StartCountAnimation();
        s.Ini(instance.GetComponent<Text>(),1.5f,3);
        GameMaster.GameMasterClass.animationUpdateList.Add(instance,s);
        Debug.Log(instance.gameObject.name);
        */
    }
}
