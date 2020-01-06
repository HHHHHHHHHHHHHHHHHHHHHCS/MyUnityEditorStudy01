using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class CreateUI
{
    [MenuItem("EditorMenu/1.MenuItem")]
    private static void MenuItem()
    {
        Debug.Log("CreateUIRoot");


        var uiRootObj = new GameObject("UIRoot");

        var canvas = new GameObject("Canvas");

        canvas.transform.SetParent(uiRootObj.transform);

        canvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();

        var eventSystem = new GameObject("EventSystem");

        eventSystem.transform.SetParent(uiRootObj.transform);

        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();

        Undo.RegisterCreatedObjectUndo(uiRootObj, "UIRoot");
    }
}