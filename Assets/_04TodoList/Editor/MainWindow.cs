using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainWindow : EditorWindow
{
    [System.Serializable]
    public class TodoListCls
    {
        public List<string> todoList = new List<string>();
    }

    private bool isShow;

    private TodoListCls todoListCls = new TodoListCls();
    private string todoName = string.Empty;


    [MenuItem("TodoList/MainWindow %#t")]
    public static void Open()
    {
        var window = GetWindow<MainWindow>();

        if (window.isShow)
        {
            window.Close();
            window.isShow = false;
        }
        else
        {
            var todoContext = EditorPrefs.GetString("TodoList_TODOS", string.Empty);

            if (string.IsNullOrEmpty(todoContext))
            {
            }
            else
            {
                window.todoListCls = JsonUtility.FromJson<TodoListCls>(todoContext);
            }

            window.Show();
            window.isShow = true;
        }
    }


    private void OnGUI()
    {
        todoName = GUILayout.TextField(todoName);

        if (GUILayout.Button("添加"))
        {
            if (!string.IsNullOrEmpty(todoName))
            {
                todoListCls.todoList.Add(todoName);
                EditorPrefs.SetString("TodoList_TODOS", JsonUtility.ToJson(todoListCls));
            }
        }

        foreach (var item in todoListCls.todoList)
        {
            GUILayout.Toggle(false, item);
        }
    }

    private void OnDisable()
    {
        EditorPrefs.SetString("TodoList_TODOS", JsonUtility.ToJson(todoListCls));
    }
}