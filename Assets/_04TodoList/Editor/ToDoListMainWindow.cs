using System;
using System.Collections;
using System.Collections.Generic;
using _04TodoList.FrameWork.Drawer;
using _04TodoList.FrameWork.Layout;
using UnityEditor;
using UnityEngine;

namespace ToDoList
{
    public class ToDoListMainWindow : EditorWindow
    {
        private bool isShow;

        private TodoListCls todoListCls;
        private string todoName = string.Empty;
        private bool showFinished = false;

        private List<IView> views = new List<IView>();

        private void Awake()
        {
            Init();
        }

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
                foreach (var item in window.todoListCls.todoList)
                {
                    item.finished.SetValueChanged(window.todoListCls.Save);
                }

                window.ShowUtility();
                window.isShow = true;
            }
        }

        private void Init()
        {
            views.Clear();
            var layout = new VerticalLayout("box");
            views.Add(layout);
            var inputTextArea = new TextAreaView(todoName);
            inputTextArea.Content.Bind(x => todoName = x);
            layout.Add(inputTextArea);
            layout.Add(new ButtonView("添加", () =>
            {
                if (!string.IsNullOrEmpty(todoName))
                {
                    todoListCls.Add(todoName, false);
                    todoName = string.Empty;
                }
            }));
            views.Add(new SpaceView(10));
        }


        private void OnGUI()
        {
            views.ForEach(view => view.OnGUI());

            if (showFinished)
            {
                if (GUILayout.Button("显示已未完成"))
                {
                    showFinished = false;
                }
            }
            else
            {
                if (GUILayout.Button("显示已完成"))
                {
                    showFinished = true;
                }
            }

            if (todoListCls.todoList.Count > 0)
            {
                GUILayout.BeginVertical("box");
                {
                    for (int i = todoListCls.todoList.Count - 1; i >= 0; --i)
                    {
                        var item = todoListCls.todoList[i];

                        if (item.finished != showFinished)
                        {
                            continue;
                        }

                        EditorGUILayout.BeginHorizontal();
                        item.finished.Val =
                            GUILayout.Toggle(item.finished.Val, item.content);

                        if (GUILayout.Button("删除"))
                        {
                            todoListCls.todoList.RemoveAt(i);
                            todoListCls.Save();
                        }

                        EditorGUILayout.EndHorizontal();
                    }
                }
                GUILayout.EndVertical();
            }
        }

        private void OnDisable()
        {
            todoListCls.Save();
        }
    }
}