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

    static UIManager ui_manger;
    public PlayerStatus PlayerStatus { get { return player_status; } }
    public Text FinishText { get { return finish_text; } }
    public EnemyStatus EnemyStatus { get { return enemy_status; } }
    public static UIManager Instance { get { return ui_manger; } }


    public void Ini()
    {
        ui_manger = this;
        // 敵のHP
        enemy_status = new EnemyStatus();
        enemy_status.Text = GameObject.Find("EnmeyHp").GetComponent<Text>();
        enemy_status.setHp(10);
        // プレイヤーのHP
        player_status = new PlayerStatus();
        player_status.Text = GameObject.Find("PlayerHp").GetComponent<Text>();
        player_status.setHp(999);
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
