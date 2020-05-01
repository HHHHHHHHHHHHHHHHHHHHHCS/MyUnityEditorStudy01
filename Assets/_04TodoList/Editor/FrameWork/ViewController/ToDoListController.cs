﻿using System;
using System.Collections.Generic;
using _04TodoList.Editor.FrameWork.DataBinding;
using _04TodoList.Editor.FrameWork.Drawer;
using _04TodoList.Editor.FrameWork.Drawer.Interface;
using _04TodoList.Editor.FrameWork.Layout;
using _04TodoList.Editor.FrameWork.ViewGUI;
using UnityEngine;

namespace _04TodoList.Editor.FrameWork.ViewController
{
    public class ToDoListController : AbsViewController
    {
        private bool isShow;

        private TodoListCls todoListCls;
        private readonly Property<bool> showFinished = new Property<bool>(false);

        private ToDoListInputView todoListInputView;
        private ToDoListView todoListView;

        private ButtonView unfinishedBtn;
        private ButtonView finishedBtn;


        protected override void SetUpView()
        {
            todoListCls = TodoListCls.ModelData;

            todoListInputView = new ToDoListInputView(AddAction);
            todoListView = new ToDoListView(showFinished);
            unfinishedBtn = new ButtonView("显示未完成", TurnShowFinished);
            finishedBtn = new ButtonView("显示已完成", TurnShowFinished);

            showFinished.Bind(UpdateShowFinished);
            UpdateShowFinished(showFinished.Val);

            views.Add(new CustomView(() => { GUILayout.Toolbar(0, new string[] {"1", "2"}); }));
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
            todoListCls.Save();
        }

        private void AddAction(string _todoName)
        {
            todoListCls.Add(_todoName, false);
            todoListView.UpdateToDoItems();
        }
    }
}