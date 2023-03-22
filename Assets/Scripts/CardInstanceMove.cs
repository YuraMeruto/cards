using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInstanceMove : AnimationUpdateBase
{
    public GameObject gameObject;
    public float move_time = 10.0f;  // �ړ�����
    float elapsedTime = 0f;         // �o�ߎ���
    float rate;                     // ����
    // �ʒu
    Vector3 preposition;            // �ړ��O�ʒu
    Vector3 postposition;           // �ړ���ʒu
    // ��]
    Quaternion prerotation;         // ��]�O�p�x
    Quaternion postrotation;        // ��]��p�x
    bool isMoving = true;

    public void Ini(GameObject obj, Vector3 pos, float m_time)
    {
        gameObject = obj;
        Transform transform = obj.GetComponent<Transform>();
        preposition = transform.position;   // �ړ��O�ʒu
        postposition = pos;   // �ړ���ʒu(x��6�ړ�)
        prerotation = transform.localRotation;  // ��]�O�ʒu
        postrotation = Quaternion.Euler(prerotation.eulerAngles.x, prerotation.eulerAngles.y + 720f, prerotation.eulerAngles.z); // ��]��ʒu(y����180�x��])
        move_time = m_time;
        CardInstanceMoveList.cardInstanceMoves.Add(obj.GetInstanceID(), this);
    }

    public override void Update()
    {
        if (isMoving == false)
        {
            return;
        }
        // �o�ߎ��Ԃ��߂����Ƃ��̏���
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
        elapsedTime += Time.deltaTime;  // �o�ߎ��Ԃ̉��Z
        rate = Mathf.Clamp01(elapsedTime / move_time);   // �����v�Z
        // �ړ��E��]
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
