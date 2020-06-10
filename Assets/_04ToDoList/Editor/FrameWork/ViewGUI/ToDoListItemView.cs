using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.Layout.Interface;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListItemView : HorizontalLayout
    {
        public ToDoData data;

        public ToDoListItemView(ToDoData item, Action UpdateToDoItems)
            : base()
        {
            data = item;

            if (data.state == ToDoData.ToDoState.NoStart)
            {
                var startBtn = new ButtonView("开始", () =>
                {
                    data.state.Val = ToDoData.ToDoState.Started;
                    UpdateToDoItems();
                });
                Add(startBtn);
            }
            else if (data.state == ToDoData.ToDoState.Started)
            {
                var finishedBtn = new ButtonView("完成", () =>
                {
                    data.state.Val = ToDoData.ToDoState.Done;
                    UpdateToDoItems();
                });
                Add(finishedBtn);
            }
            else if (data.state == ToDoData.ToDoState.Done)
            {
                var finishedBtn = new ButtonView("重置", () =>
                {
                    data.state.Val = ToDoData.ToDoState.NoStart;
                    UpdateToDoItems();
                });
                Add(finishedBtn);
            }


            var contentLabel = new CustomView(() => { GUILayout.Label(data.content); });
            Add(contentLabel);

            var deleteBtn = new ButtonView("删除", () =>
            {
                data.finished.ClearValueChanged();
                var todoListCls = ToDoListCls.ModelData;
                todoListCls.todoList.Remove(data);
                todoListCls.Save();
                UpdateToDoItems();
            });
            Add(deleteBtn);
        }

        public bool RefreshState(bool isFinished)
        {
            if ((data.state.Val == ToDoData.ToDoState.Done) == isFinished)
            {
                Show();
            }
            else
            {
                Hide();
            }

            return Visible;
        }
    }
}