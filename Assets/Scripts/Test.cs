using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;


public class Test : MonoBehaviour
{
    [SerializeField]
    Image r;
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            var snd = Addressables.LoadAssetAsync<Sprite>("Images/ReImages/GohomeIcon.png");
            var s = snd.WaitForCompletion();
            r.sprite = s;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
    }
}
