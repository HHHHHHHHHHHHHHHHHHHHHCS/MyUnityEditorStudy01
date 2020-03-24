using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("ScriptEditor/CodeGenerateInfo")]
public class CodeGenerateInfo : MonoBehaviour
{
    [HideInInspector]
    public string scriptSavePath =  "_02CreateComponentCode";
    [HideInInspector]
    public bool createPrefab = true;
    [HideInInspector]
    public string prefabPath = "_02CreateComponentCode";
}
