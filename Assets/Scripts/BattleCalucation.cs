using UnityEngine;

public class BattleCalucation : MonoBehaviour
{
    static BattleCalucation instance;
    public static BattleCalucation Instance { get { return instance; } }
    int combo = 0;
    int chain = 0;
    bool is_enemy_turn = true;
    public int Combo { get { return combo; } set { combo = value; } }
    public int Chain { get { return chain; } set { chain = value; } }
    public void Ini()
    {
        instance = this;
    }
    public static int ResultCalucation(CardManager.Number number, int remaining_hp,bool is_enmey,bool is_combo_update = false)
    {
        if(is_enmey == instance.is_enemy_turn)
        {
            instance.is_enemy_turn = is_enmey;
            instance.ResetCombo();
        }
        if (is_combo_update)
        {
            instance.combo++;
        }
        remaining_hp = remaining_hp - ((int)number + 1) * instance.combo;
        return remaining_hp;
    }

    public static bool IsFinish()
    {
        if (UIManager.Instance.PlayerStatus.HP <= 0 || UIManager.Instance.EnemyStatus.HP <= 0)
        {
            return true;
        }
        return false;
    }
    
    void ResetCombo()
    {
        combo = 1;
    }
}
