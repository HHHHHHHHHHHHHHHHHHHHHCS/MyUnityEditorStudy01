using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CodeGenerateInfo), editorForChildClasses: true /*, isFallback = false*/)]
public class CodeGenerateInfoInspector : Editor
{
    public override void OnInspectorGUI()
    {
        var codeGenerateInfo = target as CodeGenerateInfo;

        base.OnInspectorGUI();


        GUILayout.BeginVertical("box");
        GUILayout.Label("代码生成部分", new GUIStyle()
        {
            fontStyle = FontStyle.Bold,
            fontSize = 20
        });
        GUILayout.BeginHorizontal();
        GUILayout.Label("Scripts Generate Folder:");
        codeGenerateInfo.scriptSavePath = GUILayout.TextField(codeGenerateInfo.scriptSavePath);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        codeGenerateInfo.createPrefab = GUILayout.Toggle(codeGenerateInfo.createPrefab, "Create Prefab");
        GUILayout.EndHorizontal();

        if (codeGenerateInfo.createPrefab)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Prefab Generate Folder:");
            codeGenerateInfo.prefabPath = GUILayout.TextField(codeGenerateInfo.prefabPath);
            GUILayout.EndHorizontal();
        }

        GUILayout.EndVertical();
    }
}