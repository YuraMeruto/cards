using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : UpdateBase
{
    Text combo_text;
    public Text ComboText { get { return combo_text; } }
    static ComboManager instance;
    public static ComboManager Instance { get { return instance; } }
    bool is_update = false;
    float show_time = 2.0f;

    public void SetCombo()
    {
        combo_text.text = BattleCalucation.Instance.Combo.ToString() + "コンボ";
        is_update = true;
        show_time = 2.0f;
    }
    public void Ini()
    {
        instance = this;
        is_update = false;
        // コンボテキスト
        combo_text = GameObject.Find(ConstValues.COMBO_TEXT_OBJECT_NAME).GetComponent<Text>();
        combo_text.text = "";
        var pos = Vector3.zero;
        pos.x = 500;
        pos.y = 72;
        pos.z = 100;
        combo_text.GetComponent<RectTransform>().anchoredPosition = pos;

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
            combo_text.text = "";
        }
    }
}
