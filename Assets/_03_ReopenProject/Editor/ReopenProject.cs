using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ReopenProject : MonoBehaviour
{
    [MenuItem("EditorMenu/6.Reopen")]
    public static void Reopen()
    {
        if (EditorUtility.DisplayDialog("Reopen Project?"
            , "Reopen Project , are you have save project !?", "Reopen", "Cancel"))
        {
            EditorApplication.OpenProject(Application.dataPath.Substring(0, Application.dataPath.Length - 6));
        }
    }
}