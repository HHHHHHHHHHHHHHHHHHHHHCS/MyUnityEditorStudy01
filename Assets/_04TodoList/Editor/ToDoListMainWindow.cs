using System;
using System.Collections;
using System.Collections.Generic;
using _04TodoList.FrameWork.Drawer;
using _04TodoList.FrameWork.Drawer.Interface;
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

        private ButtonView unfinishedBtn, finishedBtn;
        private VerticalLayout todoItemList;

        private void OnEnable()
        {
            todoListCls = TodoListCls.Load();
            foreach (var item in todoListCls.todoList)
            {
                item.finished.SetValueChanged(todoListCls.Save);
            }

            Init();
        }

        private void OnDisable()
        {
            todoListCls.Save();
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
                //var texture = Resources.Load<Texture2D>("main");
                //window.titleContent = new GUIContent("ToDoLists", texture);

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
            views.Add(new SpaceView(5));

            unfinishedBtn = new ButtonView("显示未完成", () =>
            {
                showFinished = false;
                unfinishedBtn.Hide();
                finishedBtn.Show();
                DrawToDoItem();
            });
            unfinishedBtn.Hide();
            views.Add(unfinishedBtn);

            finishedBtn = new ButtonView("显示已完成", () =>
            {
                showFinished = true;
                unfinishedBtn.Show();
                finishedBtn.Hide();
                DrawToDoItem();
            });
            finishedBtn.Show();
            views.Add(finishedBtn);

            todoItemList = new VerticalLayout("box");
            DrawToDoItem();
        }


        private void OnGUI()
        {
            views.ForEach(view => view.DrawGUI());
        }

        private void DrawToDoItem()
        {
            todoItemList.Clear();
            var dataList = todoListCls.todoList;
            for (int i = dataList.Count - 1; i >= 0; i--)
            {
                var item = todoListCls.todoList[i];

                if (item.finished != showFinished)
                {
                    continue;
                }

                HorizontalLayout horizontalLayout = new HorizontalLayout();
                var toggle = new ToggleView(item.content, item.finished);
                toggle.IsToggle.SetValueChanged(() => { item.finished.Val = !item.finished.Val; });
                horizontalLayout.Add(toggle);
                var tempIndex = i; //这个是匿名函数嵌套 用的
                var deleteBtn = new ButtonView("删除", () =>
                {
                    item.finished.ClearValueChanged();
                    todoListCls.todoList.RemoveAt(tempIndex);
                    todoListCls.Save();
                });
                horizontalLayout.Add(deleteBtn);
                todoItemList.Add(horizontalLayout);
            }

            views.Add(todoItemList);
        }
    }
}