using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInstanceMove : AnimationUpdateBase
{
    public GameObject gameObject;
    public float move_time = 10.0f;  // 移動時間
    float elapsedTime = 0f;         // 経過時間
    float rate;                     // 割合
    // 位置
    Vector3 preposition;            // 移動前位置
    Vector3 postposition;           // 移動後位置
    // 回転
    Quaternion prerotation;         // 回転前角度
    Quaternion postrotation;        // 回転後角度
    bool isMoving = true;

    public void Ini(GameObject obj, Vector3 pos, float m_time)
    {
        gameObject = obj;
        Transform transform = obj.GetComponent<Transform>();
        preposition = transform.position;   // 移動前位置
        postposition = pos;   // 移動後位置(xを6移動)
        prerotation = transform.localRotation;  // 回転前位置
        postrotation = Quaternion.Euler(prerotation.eulerAngles.x, prerotation.eulerAngles.y + 720f, prerotation.eulerAngles.z); // 回転後位置(y軸を180度回転)
        move_time = m_time;
        CardInstanceMoveList.cardInstanceMoves.Add(obj.GetInstanceID(), this);
    }

    public override void Update()
    {
        if (isMoving == false)
        {
            return;
        }
        // 経過時間を過ぎたときの処理
        if (elapsedTime >= move_time)
        {
            if (gameObject.transform.position == postposition)
            {
                isMoving = false;
                is_finish = true;
                GameMaster.GameMasterClass.animationUpdateList.AddRemove(gameObject);

                return;
            }
        }
        elapsedTime += Time.deltaTime;  // 経過時間の加算
        rate = Mathf.Clamp01(elapsedTime / move_time);   // 割合計算
        // 移動・回転
        gameObject.transform.position = Vector3.Lerp(preposition, postposition, rate);
        gameObject.transform.localRotation = Quaternion.Slerp(prerotation, postrotation, rate);
    }

    public override void Finish()
    {
        CardChangeWait wait = new CardChangeWait();
        wait.Ini(3, gameObject);
        CardInstanceMoveList.Remove(gameObject);
        GameMaster.GameMasterClass.animationUpdateList.Add(gameObject, wait);
    }

}
