using UnityEngine;

public class BattleManager
{
    static BattleManager instance;
    public static BattleManager Instance { get { return instance; } }

    PlayableCharacterIconBase target;
    GameObject target_back_space;
    public PlayableCharacterIconBase Target { get { return target; } }

    public void Ini()
    {
        instance = this;
    }
    public void SetTargetPlayableCharacter(PlayableCharacterIconBase obj)
    {
        SetTargetPlayableCharacter(obj.PlayableObject.transform.gameObject);
    }
    public void SetTargetPlayableCharacter(GameObject obj)
    {

        target = PlayableCharacterManager.Instance.GetPlayable(obj);
        // 再配置後の場所を生成
        if (target_back_space != null)
        {
            MonoBehaviour.Destroy(target_back_space);
        }
        target_back_space = MonoBehaviour.Instantiate(target.PlayableObject);
        target_back_space.name = "hogetarou";
        var c = target_back_space.GetComponent<SpriteRenderer>().color;
        c.a = c.a / 2;
        target_back_space.GetComponent<SpriteRenderer>().color = c;
        var pos = target.PlayableObject.transform.position;
        var back_value = 0.0f;
        MonoBehaviour.Destroy(target_back_space.GetComponent<BoxCollider2D>());

        foreach (var val in PlayableCharacterManager.Instance.ActionPlayableList)
        {
            back_value += Utill.BackPostionCalculation(val, target);
        }
        if (!target.IsEnemy)
        {
            back_value = -back_value;
        }
        pos.x += back_value;
        if (Utill.IsOutField(pos, target.IsEnemy))
        {
            Debug.Log("吹き飛んだ");
            pos.x = Utill.GetOutField(target.IsEnemy);
            SetTargetSwoonStatus();
        }
        target_back_space.transform.position = pos;
    }

    public void SetMovePlayableIcon()
    {
        var back_value = 0.0f;
        var pos = target.PlayableObject.gameObject.transform.position;
        foreach (var val in PlayableCharacterManager.Instance.ActionPlayableList)
        {
            back_value += Utill.BackPostionCalculation(val, target);
        }
        if (!target.IsEnemy)
        {
            back_value = -back_value;
        }
        pos.x += back_value;
        // フィールド外に飛ばされたらとりま気絶させる
        if (Utill.IsOutField(pos,target.IsEnemy))
        {
            Debug.Log("吹き飛んだ");
            pos.x = Utill.GetOutField(target.IsEnemy);
            SetTargetSwoonStatus();
        }
        PlayableMovePostionAnimation.Instance.Set(target.PlayableObject, target.PlayableObject.transform.position, pos);
        PlayableMovePostionAnimation.Instance.Start(PlayableMovePostionAnimation.ActionType.Player);
//        target.PlayableObject.gameObject.transform.position = pos;
    }

    public void Destorytaret()
    {
        target = null;
        MonoBehaviour.Destroy(target_back_space);
    }

    /// <summary>
    /// 気絶状態の設定
    /// </summary>
    private void SetTargetSwoonStatus()
    {
        if (target.IsSwoon)
        {
            return;
        }
        target.IsSwoon = true;
        target.SwoonTime = ConstValues.DEFAULT_SWOON_TIME;
        var c = target.PlayableObject.GetComponent<SpriteRenderer>().color;
        c.a /= 2;
        target.PlayableObject.GetComponent<SpriteRenderer>().color = c;
        target.IsAlphaUpdate = false;
    }

    public void SetSwoonStatus(PlayableCharacterIconBase target_playable)
    {
        target_playable.IsSwoon = true;
        target_playable.SwoonTime = ConstValues.DEFAULT_SWOON_TIME;
        var c = target_playable.PlayableObject.GetComponent<SpriteRenderer>().color;
        c.a /= 2;
        target_playable.PlayableObject.GetComponent<SpriteRenderer>().color = c;
        target_playable.IsAlphaUpdate = false;
    }
}
