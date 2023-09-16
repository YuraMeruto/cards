using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableMovePostionAnimation : UpdateBase
{
    static PlayableMovePostionAnimation instance;
    public static PlayableMovePostionAnimation Instance { get { return instance; } }
    public enum ActionType
    {
        None,
        Player,
        Enemy,
    }
    ActionType action_type = ActionType.None;

    bool is_update = false;
    List<PlayableMovePostionAnimationParam> param_list = new List<PlayableMovePostionAnimationParam>();
    float enable_time = 0.0f;
    float const_speed = 0.5f;
    public void Ini()
    {
        instance = this;
    }

    public void Set(GameObject target_obj,Vector3 start_pos,Vector3 target_pos)
    {
        PlayableMovePostionAnimationParam param = new PlayableMovePostionAnimationParam();
        param.Ini(target_obj,start_pos, target_pos);
        param_list.Add(param);
    }

    public void Start(ActionType type)
    {
        Debug.Log("hogehoge"+type);
        enable_time = 0.0f;
        is_update = true;
        action_type = type;
    }

    public override void Update()
    {
        if (!is_update)
        {
            return;
        }
        enable_time += Time.deltaTime;
        var rate = Mathf.Clamp01(enable_time / const_speed);
        foreach (var val in param_list)
        {
            val.Obj.transform.position = Vector3.Lerp(val.StartPos,val.EndPos, rate);
        }
        var is_finish = true;
        foreach (var val in param_list)
        {
            if (!val.IsFinish())
            {
                is_finish = false;
                break;
            }
        }
        if (is_finish)
        {
            param_list.Clear();
            is_update = false;
            NextAction();
        }
    }

    void NextAction()
    {
        Debug.Log("aiueo"+action_type);

        if (TurnManager.TurnStatus == TurnManager.Turn.Draw)
        {
            PlayableCharacterManager.Instance.UpdateRestart();
        }
        else
        {
            switch (action_type)
            {
                case ActionType.Player:
                    ChainTextAnimation.Instance.Start(TurnManager.Turn.Enemy);
                    break;
                case ActionType.Enemy:
                    ChainTextAnimation.Instance.Start(TurnManager.Turn.Player);
                    Debug.Log("Ç¶ÇÀÇ›Å[true1");
                    EnemyManager.InstanceEnemyManager.updateSet(true);
                    break;
            }
        }
    }
}
