using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class AddHpTextAnimation : AnimationUpdateBase
{
    public GameObject gameObject;
    float value;
    float instance_time = 2.0f;

    public void Ini(int hp_value,bool is_enemy,bool is_add_hp)
    {

        // カードのオブジェクト生成
        var bulletPrefabHundle = Addressables.LoadAssetAsync<GameObject>("Prefabs/HitPointText.prefab");
        // 非同期での処理について終了を待つ
        var bulletPrefab = bulletPrefabHundle.WaitForCompletion();
        var instance = MonoBehaviour.Instantiate(bulletPrefab);
        gameObject = instance;
        var can = GameObject.Find("Canvas");
        gameObject.transform.parent = can.transform;
        gameObject.transform.localScale = Vector3.one;
        SetTextValue(hp_value,is_add_hp);
        SetTextColor(is_add_hp);
        SetTextPos(is_enemy);

        GameMaster.GameMasterClass.animationUpdateList.Add(gameObject,this);
    }
    public override void Finish()
    {
        MonoBehaviour.Destroy(gameObject);
    }

    public override void Update()
    {
        instance_time -= Time.deltaTime;
        if(instance_time <= 0.0f)
        {
            GameMaster.GameMasterClass.animationUpdateList.AddRemove(gameObject);
        }
    }

    private void SetTextColor(bool is_add_hp)
    {
        if (is_add_hp)
        {
            gameObject.GetComponent<Text>().color = Color.blue;
        }
        else
        {
            gameObject.GetComponent<Text>().color = Color.red;
        }
    }

    private void SetTextValue(int hp_value,bool is_add_hp)
    {
        if (is_add_hp)
        {
            gameObject.GetComponent<Text>().text = "+";
        }
        else
        {
            gameObject.GetComponent<Text>().text = "-";
        }
        gameObject.GetComponent<Text>().text += hp_value.ToString();

    }

    private void SetTextPos(bool is_enemy)
    {

        if (is_enemy)
        {
            var pos = UIManager.Instance.PlayerStatus.Text.GetComponent<RectTransform>().position;
            pos.y /= 2;
            gameObject.GetComponent<RectTransform>().position = pos;
        }
        else
        {

            var pos = UIManager.Instance.EnemyStatus.Text.GetComponent<RectTransform>().position;
            pos.y /= 2;
            gameObject.GetComponent<RectTransform>().position = pos;
        }
    }
}
