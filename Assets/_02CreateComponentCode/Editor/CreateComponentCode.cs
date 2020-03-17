using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public static class CreateComponentCode
{
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

    [MenuItem("EditorMenu/3.EditorExtension-Create Code", false)]
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

        string className = ScriptName(gameobject.name);
        List<string> cachePath = new List<string>();
        SearchUIBind(gameobject.transform, cachePath);
        CodeTemplate.LogicWrite(scriptsFolder, ScriptName(gameobject.name));
        CodeTemplate.DesignerWrite(scriptsFolder, ScriptName(gameobject.name), cachePath);


        //确保难以出现两个
        //GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameobject);

        //因为脚本是动态创建的 所以需要刷新才会出现
        //但是刷新会重新编译 清空缓存   
        EditorPrefs.SetString("GENERATE_CLASS_NAME", className);
        AssetDatabase.Refresh();

        Debug.Log("Create Code");
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
            var cls = assembly.GetType(generateClassName);
            if (cls != null)
            {
                //因为会清空缓存 所以没有办法缓存GameObject
                var obj = GameObject.Find(ObjectName(generateClassName));
                var instance = obj.GetComponent(cls);
                if (instance == null)
                    instance = obj.AddComponent(cls);
                List<string> cachePath = new List<string>();
                SearchUIBind(obj.transform, cachePath);
//                foreach (var item in cachePath)
//                {
//                    Debug.Log(item);
//                }
                var serObj = new SerializedObject(instance);

                foreach (var item in cachePath)
                {
                    bool isSelf = string.IsNullOrWhiteSpace(item);
                    var name = isSelf
                        ? ObjectName(generateClassName)
                        : item.Replace('/', '_');

                    serObj.FindProperty(name).objectReferenceValue = isSelf
                        ? obj.transform
                        : obj.transform.Find(item);
                }

                serObj.ApplyModifiedPropertiesWithoutUndo();
            }
        }
    }


    public static void SearchUIBind(Transform nowObj, List<string> cachePath, string basePath = null)
    {
        var bind = nowObj.GetComponent<UIBind>();
        if (bind)
        {
            if (basePath == null)
            {
                cachePath.Add("");
            }
            else
            {
                cachePath.Add(basePath);
            }
        }


        foreach (Transform item in nowObj)
        {
            //如果 find(/ + path) 则为 绝对路径查找  从最外层可以查找
            //应该杜绝
            SearchUIBind(item, cachePath, (basePath == null ? "" : basePath + "/") + item.name);
        }
    }
}