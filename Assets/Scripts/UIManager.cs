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
    Text draw_text;

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
        enemy_status.Text = GameObject.Find(ConstValues.ENEMY_HP_OBJECT_NAME).GetComponent<Text>();
        var p = Vector3.zero;
        p.x = 811;
        p.y = 10.1f;
        p.z = 100;
        enemy_status.Text.GetComponent<RectTransform>().anchoredPosition = p;


        enemy_status.Slider = GameObject.Find(ConstValues.ENEMY_HP_GAGE_OBJECT_NAME).GetComponent<Slider>();
        enemy_status.Slider.maxValue = enemy_hp;
        enemy_status.setHp(enemy_hp);
        p = Vector3.zero;
        p.x = 844.3f;
        p.y = 10.1f;
        p.z = 100;
        enemy_status.Slider.GetComponent<RectTransform>().anchoredPosition = p;

        // プレイヤーのHP
        var player_hp = 999;
        player_status = new PlayerStatus();
        player_status.Text = GameObject.Find(ConstValues.PLAYER_HP_OBJECT_NAME).GetComponent<Text>();
        p = Vector3.zero;
        p.x = 98;
        p.y = 10.1f;
        p.z = 100;
        player_status.Text.GetComponent<RectTransform>().anchoredPosition = p;

        player_status.Slider = GameObject.Find(ConstValues.PLYAER_HP_GAGE_OBJECT_NAME).GetComponent<Slider>();
        player_status.Slider.maxValue = player_hp;
        player_status.setHp(player_hp);
        p = Vector3.zero;
        p.x = 277;
        p.y = 1.1f;
        p.z = 100;
        player_status.Slider.GetComponent<RectTransform>().anchoredPosition = p;
 
        // 終わりのテキスト
        finish_text = GameObject.Find(ConstValues.FINISH_TEXT_OBJECT_NAME).GetComponent<Text>();
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
        var canvas = GameObject.Find(ConstValues.CANVAS_OBJECT_NAME);
        Debug.Log(RectTransformUtility.WorldToScreenPoint(Camera.main, canvas.transform.position));
        var instance = MonoBehaviour.Instantiate(bulletPrefab);
        instance.transform.position = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));
        instance.transform.parent = canvas.transform;

    }
}
