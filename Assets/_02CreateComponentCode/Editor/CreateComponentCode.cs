using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class CreateComponentCode : EditorWindow
{
    public static readonly string namespaceKey = Application.productName + "@CreateCodeNamespace";
    public static readonly string defaultNamespace = "DefaultNamespace";


    public static string CodeNamespace
    {
        get
        {
            var str = EditorPrefs.GetString(namespaceKey);
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultNamespace;
            }

            return str;
        }

        set
        {
            var str = value;
            if (string.IsNullOrWhiteSpace(str))
            {
                str = defaultNamespace;
            }

            EditorPrefs.SetString(namespaceKey, str);
        }
    }

    public static bool IsDefaultNamespace => CodeNamespace == defaultNamespace;


    public static string ScriptName(string file)
    {
        return file + "Code";
    }

    public static string ObjectName(string scriptName)
    {
        if (scriptName.Length > 4)
        {
            return scriptName.Substring(0, scriptName.Length - 4);
        }

        return scriptName;
    }

    [MenuItem("EditorMenu/3.EditorExtension-Create Code", true)]
    [MenuItem("GameObject/@EditorExtension-Create Code", true, 0)]
    private static bool ValidateCreateCode()
    {
        var gameobject = Selection.gameObjects.First();

        if (!gameobject && !EditorUtility.IsPersistent(gameobject))
        {
            return false;
        }

        var viewBind = gameobject.GetComponent<CodeGenerateInfo>();

        return viewBind != null;
    }

    [MenuItem("EditorMenu/3.EditorExtension-Create Code", false)]
    [MenuItem("GameObject/@EditorExtension-Create Code", false, 0)]
    private static void CreateCode()
    {
        var gameobject = Selection.gameObjects.First();

        if (!gameobject && !EditorUtility.IsPersistent(gameobject))
        {
            Debug.LogWarning("selection is not gameObject or not in scene!");
            return;
        }

        var scriptSavePath = gameobject.GetComponent<CodeGenerateInfo>().scriptSavePath;

        var scriptsFolder = Application.dataPath + "/" + scriptSavePath;

        if (!Directory.Exists(scriptsFolder))
        {
            Directory.CreateDirectory(scriptsFolder);
        }

        string className = ScriptName(gameobject.name);
        List<KeyValuePair<string, string>> cachePath = new List<KeyValuePair<string, string>>();
        SearchUIBind(gameobject.transform, cachePath);
        //利用注释的GUID 强制编译
        CodeTemplate.LogicWrite(scriptsFolder, ScriptName(gameobject.name));
        CodeTemplate.DesignerWrite(scriptsFolder, ScriptName(gameobject.name), cachePath);


        //确保难以出现两个
        //GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameobject);

        //因为脚本是动态创建的 所以需要刷新才会出现
        //但是刷新会重新编译 清空缓存   
        EditorPrefs.SetString("GENERATE_CLASS_NAME", className);
        AssetDatabase.Refresh();
    }

    //DidReloadScripts 编译完成之后callback 事件
    [DidReloadScripts]
    private static void AddComponentToGameObject()
    {
        //Debug.Log("DidReloadScripts");
        var generateClassName = EditorPrefs.GetString("GENERATE_CLASS_NAME");
        //避免改其他的时候调用到
        EditorPrefs.SetString("GENERATE_CLASS_NAME", "");
        //Debug.Log(generateClassName);

        if (string.IsNullOrWhiteSpace(generateClassName))
        {
            //Debug.Log("class name is null , breaking!");
            return;
        }

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var assembly = assemblies.First(temp => temp.GetName().Name == "Assembly-CSharp");
        if (assembly != null)
        {
            var clsName = CodeNamespace + "." + generateClassName;
            var cls = assembly.GetType(clsName);
            if (cls == null)
            {
                Debug.LogError("编译失败没有获取到class:" + cls);
                return;
            }

            //因为会清空缓存 所以没有办法缓存GameObject
            var obj = GameObject.Find(ObjectName(generateClassName));
            var instance = obj.GetComponent(cls);
            if (instance == null)
                instance = obj.AddComponent(cls);
            List<KeyValuePair<string, string>> cachePath = new List<KeyValuePair<string, string>>();
            SearchUIBind(obj.transform, cachePath);
//                foreach (var item in cachePath)
//                {
//                    Debug.Log(item);
//                }
            var serObj = new SerializedObject(instance);

            foreach (var item in cachePath)
            {
                bool isSelf = string.IsNullOrWhiteSpace(item.Key);
                var name = isSelf
                    ? ObjectName(generateClassName)
                    : item.Key.Replace('/', '_');

                var ts = isSelf
                    ? obj.transform
                    : obj.transform.Find(item.Key);

                serObj.FindProperty(name).objectReferenceValue = ts.GetComponent(item.Value);
            }

            serObj.ApplyModifiedPropertiesWithoutUndo();

            var info = obj.GetComponent<CodeGenerateInfo>();

            if (info.createPrefab)
            {
                if (!Directory.Exists(info.prefabPath))
                {
                    Directory.CreateDirectory(info.prefabPath);
                }

                PrefabUtility.SaveAsPrefabAssetAndConnect(obj,
                    Application.dataPath + "/" + info.prefabPath + "/" + obj.name + ".prefab",
                    InteractionMode.AutomatedAction);
            }


            Debug.Log("Create Code");
        }
    }


    public static void SearchUIBind(Transform nowObj, List<KeyValuePair<string, string>> cachePath,
        string basePath = null)
    {
        var bind = nowObj.GetComponent<UIBind>();
        if (bind)
        {
            if (basePath == null)
            {
                cachePath.Add(new KeyValuePair<string, string>("", bind.Component));
            }
            else
            {
                cachePath.Add(new KeyValuePair<string, string>(basePath, bind.Component));
            }
        }


        foreach (Transform item in nowObj)
        {
            //如果 find(/ + path) 则为 绝对路径查找  从最外层可以查找
            //应该杜绝
            SearchUIBind(item, cachePath, (basePath == null ? "" : basePath + "/") + item.name);
        }
    }

    [MenuItem("EditorMenu/4.Create Code Window", false)]
    public static void OpenWindow()
    {
        GetWindow<CreateComponentCode>().Show();
    }


    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Namespace:");
        CodeNamespace = GUILayout.TextField(CodeNamespace);
        GUILayout.EndHorizontal();
    }

    [MenuItem("EditorMenu/5.Add Add CodeGenerateInfo", false)]
    public static void AddCodeGenerateInfo()
    {
        var gameobject = Selection.gameObjects.First();

        if (!gameobject && !EditorUtility.IsPersistent(gameobject))
        {
            return;
        }

        var viewBind = gameobject.GetComponent<CodeGenerateInfo>();
        if (!viewBind)
        {
            gameobject.AddComponent<CodeGenerateInfo>();
        }
    }
}