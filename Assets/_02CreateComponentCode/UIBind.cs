using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("ScriptEditor/UIBind")]
public class UIBind : MonoBehaviour
{
    public string Component
    {
        get
        {
            if (GetComponent<MeshRenderer>())
            {
                return "MeshRenderer";
            }
            else if (GetComponent<SpriteRenderer>())
            {
                return "SpriteRenderer";
            }

            return "Transform";
        }
    }
}
