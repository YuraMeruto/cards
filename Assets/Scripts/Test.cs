using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
    [SerializeField]
    GameObject obj;
    [SerializeField]
    float start;
    [SerializeField]
    float end;
    GameObject can;
    Text t;
    [SerializeField]
    GameObject start_obj;
    void Start()
    {
        // Cavasè„Ç≈ÇÃÉ|ÉWÉVÉáÉìê›íË
       var text_obji =  obj.GetComponent<RectTransform>();
        var newPos = Vector2.zero;
        var camera = Camera.main;
        can = GameObject.Find("Canvas");
        var canvasRectTrans = can.GetComponent<RectTransform>();
        var r = start_obj.GetComponent<RectTransform>().position;
        var target = r;
        var screenPos = RectTransformUtility.WorldToScreenPoint(camera, target);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTrans, screenPos, camera, out newPos);
        newPos.y /= 2;
        text_obji.localPosition = newPos;

    }
    void Update()
    {
        /*
        start =Mathf.Lerp(start, end,Time.deltaTime * 2);
        int i = (int)start;
        t.text = i.ToString();
        */
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var pos = Camera.main.ViewportToWorldPoint(gameObject.transform.position);
            Debug.Log("view_to_world" + pos.x + ":" + pos.y);

            var pos2 = Camera.main.ScreenToViewportPoint(gameObject.transform.position);
            Debug.Log("screen_to_view" + pos2.x + ":" + pos2.y);

            var pos3 = Camera.main.ViewportToScreenPoint(gameObject.transform.position);
            Debug.Log("view_to_screen" + pos3.x + ":" + pos3.y);
        }
        */
        //   Debug.Log(Mathf.Abs(target.transform.position.x) -  Mathf.Abs(target2.transform.position.x));

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
    }
}
