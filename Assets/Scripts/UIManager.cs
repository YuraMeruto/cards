using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class UIManager
{
    EnemyStatus enemy_status;
    PlayerStatus player_status;
    Text finish_text;
    Text combo_text;

    static UIManager ui_manger;
    public PlayerStatus PlayerStatus { get { return player_status; } }
    public Text FinishText { get { return finish_text; } }
    public Text ComboText { get { return combo_text; } }
    public EnemyStatus EnemyStatus { get { return enemy_status; } }
    public static UIManager Instance { get { return ui_manger; } }


    public void Ini()
    {
        ui_manger = this;
        // 敵のHP
        var enemy_hp = 99;
        enemy_status = new EnemyStatus();
        enemy_status.Text = GameObject.Find("EnmeyHp").GetComponent<Text>();
        var p = Vector3.zero;
        p.x = 770;
        p.y = 40f;
        p.z = 100;

        enemy_status.Slider = GameObject.Find("EnemyHpGage").GetComponent<Slider>();
        enemy_status.Slider.maxValue = enemy_hp;
        enemy_status.setHp(enemy_hp);
        p = Vector3.zero;
        p.x = 593.3f;
        p.y = 36.5f;
        p.z = 100;
        enemy_status.Slider.GetComponent<RectTransform>().anchoredPosition = p;

        // プレイヤーのHP
        var player_hp = 999;
        player_status = new PlayerStatus();
        player_status.Text = GameObject.Find("PlayerHp").GetComponent<Text>();
        p = Vector3.zero;
        p.x = 130f;
        p.y = 40f;
        p.z = 100;
        player_status.Text.GetComponent<RectTransform>().anchoredPosition = p;

        player_status.Slider = GameObject.Find("PlayerHpGage").GetComponent<Slider>();
        player_status.Slider.maxValue = player_hp;
        player_status.setHp(player_hp);
        p = Vector3.zero;
        p.x = 200;
        p.y = 30;
        p.z = 100;
        player_status.Slider.GetComponent<RectTransform>().anchoredPosition = p;
 
        // 終わりのテキスト
        finish_text = GameObject.Find("FinishText").GetComponent<Text>();
        finish_text.text = "";

    }

    public void Finish(bool is_win_player)
    {
        if (is_win_player)
        {
            finish_text.text = "Win";
            finish_text.color = Color.red;
        }
        else
        {
            finish_text.text = "Lose";
            finish_text.color = Color.blue;
        }
        EnemyManager.InstanceEnemyManager.EnemyAction.IsUpdate = false;
        PlayerManager.InstancePlayerManger.PlayerAction.IsUpdate = false;
    }

    static void Instances()
    {
        // カードのオブジェクト生成
        var bulletPrefabHundle = Addressables.LoadAssetAsync<GameObject>(AddressablesNames.UI_HP_TEXT);

        // 非同期での処理について終了を待つ
        var bulletPrefab = bulletPrefabHundle.WaitForCompletion();
        /*
Debug.Log("画面の左下の座標は " + Camera.main.ScreenToWorldPoint(new Vector2(0, 0)));
Debug.Log("画面の左上の座標は " + Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)));
Debug.Log("画面の右上の座標は " + Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)));
Debug.Log("画面の右下の座標は " + Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)));
*/
        var canvas = GameObject.Find("Canvas");
        Debug.Log(RectTransformUtility.WorldToScreenPoint(Camera.main, canvas.transform.position));
        var instance = MonoBehaviour.Instantiate(bulletPrefab);
        instance.transform.position = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));
        instance.transform.parent = canvas.transform;

    }
}
