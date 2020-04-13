using System;
using System.Collections.Generic;
using _04TodoList.Editor.FrameWork.DataBinding;
using _04TodoList.Editor.FrameWork.Drawer;
using _04TodoList.Editor.FrameWork.Drawer.Interface;
using _04TodoList.Editor.FrameWork.Layout;
using _04TodoList.Editor.FrameWork.ViewGUI;

namespace _04TodoList.Editor.FrameWork.ViewController
{
    public class ToDoListController : AbsViewController
    {
        private bool isShow;

        private readonly TodoListCls todoListCls;
        private bool showFinished = false;

        private readonly ButtonView unfinishedBtn;
        private readonly ButtonView finishedBtn;
        private readonly VerticalLayout todoItemList;


        public ToDoListController()
        {
            todoListCls = TodoListCls.ModelData;

            views.Add(new ToDoListInputView(AddAction));

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

        public void OnDisable()
        {
            todoListCls.Save();
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