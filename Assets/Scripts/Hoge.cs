using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Hoge : MonoBehaviour
{
    private GameObject clickedGameObject;
    private AsyncOperationHandle<GameObject> bulletPrefabHundle;
    private GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        bulletPrefabHundle = Addressables.LoadAssetAsync<GameObject>("Prefabs/back.prefab");

        // �񓯊��ł̏����ɂ��ďI����҂�
        bulletPrefab = bulletPrefabHundle.WaitForCompletion();
        // Prefab����Q�[���I�u�W�F�N�g�̍쐬
        //        Instantiate(bulletPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        return;

        if (Input.GetMouseButtonDown(0))
        {

            clickedGameObject = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2d)
            {
                clickedGameObject = hit2d.transform.gameObject;
                if(clickedGameObject.tag == TagManager.CARD)
                {
                    Card card = CardManager.GetCard(clickedGameObject);
                    Debug.Log(card.CardType);

                    // �J�[�h�̃I�u�W�F�N�g����
                    var bulletPrefabHundle = Addressables.LoadAssetAsync<Sprite>(AddressablesNames.FRONT_TWO);
                    // �񓯊��ł̏����ɂ��ďI����҂�
                    var bulletPrefab = bulletPrefabHundle.WaitForCompletion();
//                    clickedGameObject.GetComponent<SpriteRenderer>().sprite = bulletPrefab;
                    CardManager.ChangeCard(clickedGameObject);
                }
            }

//            Debug.Log(clickedGameObject);
        }
    }
}
