using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawBattleUI : UpdateBase
{
    static DrawBattleUI instnace;
    public static DrawBattleUI Instance { get { return instnace; } }
    Text draw_battle_text;
    public Text DrawBattleText { get { return draw_battle_text; } set { draw_battle_text = value; } }
    bool is_update = false;
    float show_time = 2.0f;
    public void Ini()
    {
        instnace = this;
        draw_battle_text = GameObject.Find(ConstValues.DRAW_BATTLE_TEXT_OBJECT_NAME).GetComponent<Text>();
        var pos = draw_battle_text.GetComponent<RectTransform>().transform.position;
        pos.z = 11;
        draw_battle_text.GetComponent<RectTransform>().transform.localPosition = pos;
        draw_battle_text.text = "";
    }

    public void SetBattleResult(DrawBattleManager.BattleResult result)
    {
        switch (result)
        {
            case DrawBattleManager.BattleResult.Draw:
                draw_battle_text.text = "DRAW";
                draw_battle_text.color = Color.yellow;
                break;
            case DrawBattleManager.BattleResult.Sucsess:
                draw_battle_text.text = "SUCSESS";
                draw_battle_text.color = Color.blue;
                break;
            case DrawBattleManager.BattleResult.Failed:
                draw_battle_text.text = "FAILED";
                draw_battle_text.color = Color.red;
                break;
        }
        is_update = true;
        show_time = 1f;
    }

    public override void Update()
    {
        if (!is_update)
        {
            return;
        }
        show_time -= Time.deltaTime;
        if (show_time <= 0.0f)
        {
            is_update = false;
            draw_battle_text.text = "";
            DrawBattleManager.Instance.Destroy();
            DrawBattleManager.Instance.BattleResultCalucation();
            DrawBattleManager.Instance.Clear();
        }
    }
}
