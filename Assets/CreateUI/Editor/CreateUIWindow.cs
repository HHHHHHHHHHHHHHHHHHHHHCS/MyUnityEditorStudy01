using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        var widthStr = GUILayout.TextField("720");
        GUILayout.Label("x", GUILayout.Width(10));
        GUILayout.Label("Height", GUILayout.Width(50));
        var heightStr = GUILayout.TextField("1280");
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Setup"))
        {
            if (float.TryParse(widthStr, out var width)
                && float.TryParse(heightStr, out var height))
            {
                Setup(width, height);
            }
            else
            {
                Debug.LogError("Please Input number");
            }
        }
    }

    private static void Setup(float width, float height)
    {
        if (GameObject.Find("UIRoot"))
        {
            Debug.Log("Have UI Root in Scene , Can't Create");
            return;
        }

        //UIRoot
        var uiRootObj = new GameObject("UIRoot");

        //Canvas
        var canvas = new GameObject("Canvas");
        canvas.transform.SetParent(uiRootObj.transform);
        canvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        //CanvasScaler
        var canvasScaler = canvas.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(width, height);

        canvas.AddComponent<GraphicRaycaster>();

        var eventSystem = new GameObject("EventSystem");

        eventSystem.transform.SetParent(uiRootObj.transform);

        //EventSystem
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();

        //UIRoot
        var uiRoot = uiRootObj.AddComponent<UIRoot>();

        var bg = new GameObject("BG");
        uiRoot.bg = bg.AddComponent<RectTransform>();
        bg.transform.SetParent(canvas.transform);
        ((RectTransform) bg.transform).localPosition = Vector3.zero;


        var common = new GameObject("Common");
        uiRoot.common = common.AddComponent<RectTransform>();
        common.transform.SetParent(canvas.transform);
        ((RectTransform) common.transform).localPosition = Vector3.zero;


        var popUI = new GameObject("PopUI");
        uiRoot.popUI = popUI.AddComponent<RectTransform>();
        popUI.transform.SetParent(canvas.transform);
        ((RectTransform) popUI.transform).localPosition = Vector3.zero;


        var forwardObj = new GameObject("Forward");
        uiRoot.forward = forwardObj.AddComponent<RectTransform>();
        forwardObj.transform.SetParent(canvas.transform);
        ((RectTransform) forwardObj.transform).localPosition = Vector3.zero;

        var uiRootSerObj = new SerializedObject(uiRoot);
        uiRootSerObj.FindProperty("mRootCanvas").objectReferenceValue = canvas.GetComponent<Canvas>();
        uiRootSerObj.ApplyModifiedPropertiesWithoutUndo();

        //layer
        uiRootObj.layer = LayerMask.NameToLayer("UI");
        foreach (Transform trans in uiRootObj.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = LayerMask.NameToLayer("UI");
        }

        var savedFolder = Application.dataPath + "/Resources";
        if (!Directory.Exists(savedFolder))
        {
            Directory.CreateDirectory(savedFolder);
        }

        var saveFilePath = savedFolder + "/UIRoot.prefab";

        PrefabUtility.SaveAsPrefabAssetAndConnect(uiRootObj, saveFilePath, InteractionMode.AutomatedAction);


        Undo.RegisterCreatedObjectUndo(uiRootObj, "UIRoot");

        Debug.Log("CreateUIRoot");
    }
}