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
            var window = GetWindow<ToDoListMainWindow>(true, "ToDoLists", true);

            if (window.isShow)
            {
                window.Close();
                window.isShow = false;
            }
            else
            {
                window.todoListCls = TodoListCls.Load();
//                var texture = Resources.Load<Texture2D>("main");
//                window.titleContent = new GUIContent("ToDoLists", texture);
                window.ShowUtility();
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
                item.finishedValue = GUILayout.Toggle(item.finishedValue, item.content);

                if (item.finishedChanged)
                {
                    item.finishedChanged = false;
                    todoListCls.Save();
                }

                if (GUILayout.Button("删除"))
                {
                    todoListCls.todoList.RemoveAt(i);
                    todoListCls.Save();
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