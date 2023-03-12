using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
    [SerializeField]
    GameObject obj;
    [SerializeField]
    GameObject obj2;
    [SerializeField]
    GameObject obj3;
    [SerializeField]
    GameObject obj4;
    [SerializeField]
    GameObject obj5;
    [SerializeField]
    GameObject obj6;


    [SerializeField]
    GameObject target;
    [SerializeField]
    GameObject target2;
    void Start()
    {
        /*
Debug.Log("画面の左下の座標は " + Camera.main.ScreenToWorldPoint(new Vector2(0, 0)));
Debug.Log("画面の左上の座標は " + Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)));
Debug.Log("画面の右上の座標は " + Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)));
Debug.Log("画面の右下の座標は " + Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)));
*/

        obj.transform.position = Camera.main.ScreenToWorldPoint(Vector3.zero);
        obj2.transform.position = Camera.main.ScreenToWorldPoint(new Vector2(0,Screen.height));
        obj3.transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        obj4.transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,0));

        /*
        obj.transform.position = Camera.main.ViewportToScreenPoint(Vector3.zero);
        obj2.transform.position = Camera.main.WorldToViewportPoint(Vector3.zero);
        obj3.transform.position = Camera.main.WorldToScreenPoint(Vector3.zero);
        obj4.transform.position = Camera.main.ViewportToWorldPoint(Vector3.zero);
        obj5.transform.position = Camera.main.ScreenToViewportPoint(Vector3.zero);
        obj6.transform.position = Camera.main.ScreenToWorldPoint(Vector3.zero);
        */

    }
    void Update()
    {
        Debug.Log(Mathf.Abs(target.transform.position.x) -  Mathf.Abs(target2.transform.position.x));

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
    }
}
