using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreateUIWindow : EditorWindow
{
    [MenuItem("EditorMenu/2.MenuItemWindow")]
    private static void DoCreateUIWindow()
    {
        var window = GetWindow<CreateUIWindow>();

        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Width", GUILayout.Width(45));
        var width = GUILayout.TextField("720");
        GUILayout.Label("x", GUILayout.Width(10));
        GUILayout.Label("Height", GUILayout.Width(50));
        var height = GUILayout.TextField("1280");
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Setup"))
        {
            Setup(720, 1280);
        }
    }

    private static void Setup(float width, float height)
    {
        var uiRootObj = new GameObject("UIRoot");

        uiRootObj.layer = LayerMask.NameToLayer("UI");

        var canvas = new GameObject("Canvas");

        canvas.transform.SetParent(uiRootObj.transform);

        canvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        var canvasScaler = canvas.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(width, height);

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