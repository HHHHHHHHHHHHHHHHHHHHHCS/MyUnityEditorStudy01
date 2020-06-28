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

        private ToDoListView todoListView;
        private ToDoListFinishedView todoListFinishedView;


        protected override void SetUpView()
        {
            _toDoListCls = ToDoListCls.ModelData;
            todoListToolBarView = new ToolBarView {style = "box"}.FontSize(15);
            todoListToolBarView
                .AddMenu("清单", () => { showFinished.Val = false; })
                .AddMenu("已完成", () => { showFinished.Val = true; });

            todoListView = new ToDoListView();
            todoListFinishedView = new ToDoListFinishedView();

            showFinished.Bind(UpdateShowFinished);
            UpdateShowFinished(showFinished.Val);

            views.Add(todoListToolBarView);
            views.Add(todoListView);
            views.Add(todoListFinishedView);
        }

        public void TurnShowFinished()
        {
            showFinished.Val = !showFinished.Val;
        }

        public void UpdateShowFinished(bool _showFinished)
        {
            if (_showFinished)
            {
                todoListView.Hide();
                todoListFinishedView.Show();
                todoListFinishedView.ReBuildToDoItems();
            }
            else
            {
                todoListView.Show();
                todoListFinishedView.Hide();
                todoListView.ReBuildToDoItems();
            }
        }

        protected override void OnUpdate()
        {
            todoListView.OnUpdate();
            todoListFinishedView.OnUpdate();
        }

        public void OnDisable()
        {
            _toDoListCls.Save();
        }
    }
}