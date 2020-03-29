using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ToDoList
{
    public class ToDoListMainWindow : EditorWindow
    {
        private bool isShow;

        private TodoListCls todoListCls;
        private string todoName = string.Empty;


        [MenuItem("TodoList/MainWindow %#t")]
        public static void Open()
        {
            var window = GetWindow<ToDoListMainWindow>();

            if (window.isShow)
            {
                window.Close();
                window.isShow = false;
            }
            else
            {
                window.todoListCls = TodoListCls.Load();

                window.Show();
                window.isShow = true;
            }
        }


        private void OnGUI()
        {
            //GUILayout 多半不支持中文
            todoName = EditorGUILayout.TextField(todoName);

            if (GUILayout.Button("添加"))
            {
                if (!string.IsNullOrEmpty(todoName))
                {
                    todoListCls.Add(todoName, false);
                }
            }

            for (int i = todoListCls.todoList.Count - 1; i >= 0; i--)
            {
                var item = todoListCls.todoList[i];

                EditorGUILayout.BeginHorizontal();
                GUILayout.Toggle(false, item.content);
                if (GUILayout.Button("删除"))
                {
                    todoListCls.todoList.RemoveAt(i);
                    EditorPrefs.SetString("TodoList_TODOS", JsonUtility.ToJson(todoListCls));
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        private void OnDisable()
        {
            todoListCls.Save();
        }
    }
}