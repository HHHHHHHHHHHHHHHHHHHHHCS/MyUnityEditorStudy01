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
        private readonly Property<bool> showFinished = new Property<bool>(false);

        private ToolBarView todoListToolBarView;

        private ToDoListInputView todoListInputView;
        private ToDoListView todoListView;

        private ButtonView unfinishedBtn;
        private ButtonView finishedBtn;


        protected override void SetUpView()
        {
            _toDoListCls = ToDoListCls.ModelData;

            todoListToolBarView =
                new ToolBarView(new[] {"11", "22"}, new Action[] {() => Debug.Log(1), () => Debug.Log(2)});
            todoListInputView = new ToDoListInputView(AddAction);
            todoListView = new ToDoListView(showFinished);
            unfinishedBtn = new ButtonView("显示未完成", TurnShowFinished);
            finishedBtn = new ButtonView("显示已完成", TurnShowFinished);

            showFinished.Bind(UpdateShowFinished);
            UpdateShowFinished(showFinished.Val);

            views.Add(todoListToolBarView);
            views.Add(todoListInputView);
            views.Add(unfinishedBtn);
            views.Add(finishedBtn);
            views.Add(todoListView);
        }

        public void TurnShowFinished()
        {
            showFinished.Val = !showFinished.Val;
        }

        public void UpdateShowFinished(bool _showFinished)
        {
            if (showFinished)
            {
                unfinishedBtn.Show();
                finishedBtn.Hide();
            }
            else
            {
                unfinishedBtn.Hide();
                finishedBtn.Show();
            }

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