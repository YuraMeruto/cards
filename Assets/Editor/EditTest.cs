using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateEnemyParamAsset")]
public class EditTest : ScriptableObject
{
    [System.Serializable]
    public class EnemyParam
    {
        public string EnemyName = "ƒXƒ‰ƒCƒ€";

        [SerializeField]
        int MaxHP = 100;
    }


    public List<EnemyParam> EnemyParamList = new List<EnemyParam>();
}

 