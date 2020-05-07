﻿using System.Collections;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewController
{
    public class ToDoListView : VerticalLayout
    {
        protected bool showFinished;
        private bool isDirty;


        public ToDoListView(bool _showFinished) : base("box")
        {
            UpdateShowFinished(_showFinished);
        }

        public void UpdateShowFinished(bool _showFinished)
        {
            showFinished = _showFinished;
        }

        public void UpdateToDoItems()
        {
            isDirty = true;
        }

        public void OnUpdate()
        {
            if (isDirty)
            {
                isDirty = false;
                ReBuildToDoItems();
            }
        }

        public void ReBuildToDoItems()
        {
            var todoListCls = ToDoListCls.ModelData;

            children.Clear();
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
                toggle.IsToggle.SetValueChanged((_val) => { item.finished.Val = _val; });
                horizontalLayout.Add(toggle);
                var tempIndex = i; //这个是匿名函数嵌套 用的
                var deleteBtn = new ButtonView("删除", () =>
                {
                    item.finished.ClearValueChanged();
                    todoListCls.todoList.RemoveAt(tempIndex);
                    todoListCls.Save();
                    UpdateToDoItems();
                });
                horizontalLayout.Add(deleteBtn);
                children.AddLast(horizontalLayout);
            }
        }
    }
}