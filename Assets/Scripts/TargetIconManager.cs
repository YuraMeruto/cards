using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TargetIconManager
{
    static TargetIconManager instnace;
    public static TargetIconManager Instance { get { return instnace; } }

    GameObject icon;
    public GameObject Icon { get { return icon; } }

    public void Ini()
    {
        instnace = this;
    }

    public void InstanceIcon(Vector3 pos)
    {
        // �J�[�h�̃I�u�W�F�N�g����
        var prefab = Addressables.LoadAssetAsync<GameObject>(AddressablesNames.TARGET_ICON_LIST);

        // �񓯊��ł̏����ɂ��ďI����҂�
        var p = prefab.WaitForCompletion();
        icon = MonoBehaviour.Instantiate(p);
        pos.y += 1;
        icon.transform.position = pos;
    }

    public void MoveTarget(GameObject target)
    {
        Vector3 pos = target.transform.position;
        pos.y += 1;
        icon.transform.position = pos;
    }

    public void IconDestory()
    {
        MonoBehaviour.Destroy(icon);
    }

}
