using System.Collections.Generic;
using _04TodoList.Editor.FrameWork.DataBinding;
using _04TodoList.Editor.FrameWork.Drawer;
using _04TodoList.Editor.FrameWork.Drawer.Interface;
using _04TodoList.Editor.FrameWork.Layout;
using _04TodoList.Editor.FrameWork.ViewController;
using UnityEditor;
using UnityEngine;

namespace _04TodoList.Editor.FrameWork.Window
{
    public class ToDoListMainWindow : Window
    {
        private bool isShow;

        private TodoListCls todoListCls;
        private bool showFinished = false;

        private List<IView> views = new List<IView>();

        private ButtonView unfinishedBtn, finishedBtn;
        private VerticalLayout todoItemList;


        private TodoInputController todoInputController;

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
            todoInputController = new TodoInputController(AddAction);



            /*
            views.Clear();

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
            */
        }

        protected override void OnGUI()
        {
            todoInputController.Draw();
            views.ForEach(view => view.DrawGUI());
        }

        private void AddAction(string _todoName)
        {
            todoListCls.Add(_todoName, false);
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