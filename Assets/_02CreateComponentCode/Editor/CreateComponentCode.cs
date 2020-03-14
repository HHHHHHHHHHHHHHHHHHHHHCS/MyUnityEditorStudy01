using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class CreateComponentCode
{
    [MenuItem("GameObject/@EditorExtension-Create Code", false, 0)]
    private static void Create()
    {
        var gameobject = Selection.gameObjects.First();

        if (!gameobject && !EditorUtility.IsPersistent(gameobject))
        {
            Debug.LogWarning("selection is not gameObject or not in scene!");
            return;
        }


        var scriptsFolder = Application.dataPath + "/_02CreateComponentCode/";

        if (!Directory.Exists(scriptsFolder))
        {
            Directory.CreateDirectory(scriptsFolder);
        }

        string className = gameobject.name + "Code";
        var scriptFile = scriptsFolder + className + ".cs";
        using (var sw = File.CreateText(scriptFile))
        {
            CodeTemplate.Write(sw, className);

            sw.Close();
        }

        AssetDatabase.Refresh();

        Debug.Log("Create Code");
    }
}