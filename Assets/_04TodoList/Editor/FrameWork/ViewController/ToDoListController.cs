using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewGUI;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewController
{
    public class ToDoListController : AbsViewController
    {
        private bool isShow;

        private ToDoListCls _toDoListCls;
        public readonly Property<bool> showFinished = new Property<bool>(false);

        private ToolBarView todoListToolBarView;

        private ToDoListInputView todoListInputView;
        private ToDoListView todoListView;

        protected override void SetUpView()
        {
            _toDoListCls = ToDoListCls.ModelData;
            todoListToolBarView = new ToolBarView {style = "box"};
            todoListToolBarView.AddMenu("清单", () =>
            {
                showFinished.Val = false;
                todoListInputView.Show();
            });
            todoListToolBarView.AddMenu("已完成", () =>
            {
                showFinished.Val = true;
                todoListInputView.Hide();
            });

            todoListInputView = new ToDoListInputView(AddAction);
            todoListView = new ToDoListView(showFinished);

            showFinished.Bind(UpdateShowFinished);
            UpdateShowFinished(showFinished.Val);

            views.Add(todoListToolBarView);
            views.Add(todoListInputView);
            views.Add(todoListView);
        }

        public void TurnShowFinished()
        {
            showFinished.Val = !showFinished.Val;
        }

        public void UpdateShowFinished(bool _showFinished)
        {
            todoListView.UpdateShowFinished(showFinished);
            todoListView.UpdateToDoItems();
        }

        protected override void OnUpdate()
        {
            todoListView.OnUpdate();
        }

        public void OnDisable()
        {
            _toDoListCls.Save();
        }

        private void AddAction(string _todoName)
        {
            _toDoListCls.Add(_todoName, false);
            todoListView.UpdateToDoItems();
        }
    }
}