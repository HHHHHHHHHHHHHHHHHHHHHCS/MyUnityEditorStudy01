﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class CreateUI
{
    [MenuItem("EditorMenu/1.MenuItem %u", true)]
    private static bool ValidateCreateUIRoot()
    {
        return GameObject.Find("UIRoot") == null;
    }

    //isValidateFunction 如果写了true/false 代表是检测方法  只是 决定是否启用而已
    [MenuItem("EditorMenu/1.MenuItem %u")]
    private static void DoCreateUIRoot()
    {
        var uiRootObj = new GameObject("UIRoot");

        uiRootObj.AddComponent<CodeGenerateInfo>();

        uiRootObj.layer = LayerMask.NameToLayer("UI");

        var canvas = new GameObject("Canvas");

        canvas.transform.SetParent(uiRootObj.transform);

        canvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();

        var eventSystem = new GameObject("EventSystem");

        eventSystem.transform.SetParent(uiRootObj.transform);

        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();

        foreach (Transform trans in uiRootObj.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = LayerMask.NameToLayer("UI");
        }


        Undo.RegisterCreatedObjectUndo(uiRootObj, "UIRoot");

        Debug.Log("CreateUIRoot");
    }
}