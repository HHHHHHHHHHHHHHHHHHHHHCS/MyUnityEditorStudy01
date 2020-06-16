using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.Layout.Interface;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListItemView : HorizontalLayout
    {
        public ToDoData data;
        public Action<ToDoListItemView> removeAct;

        private bool needFresh;
        private bool needRemove;

        private Texture2D playIcon;

        public ToDoListItemView(ToDoData _item, Action<ToDoListItemView> _removeAct)
            : base()
        {
            playIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_04ToDoList/EditorIcons/Play.png");

            this.data = _item;
            this.removeAct = _removeAct;
            BuildItem();
        }

        protected override void OnRefresh()
        {
            if (needRemove)
            {
                removeAct(this);
            }
            else if (needFresh)
            {
                needFresh = false;
                BuildItem();
            }
        }

        public void BuildItem()
        {
            Clear();

            Add(new CustomView(() =>
            {
                GUILayout.Button(playIcon, GUILayout.Width(40), GUILayout.Height(20));
            }));

            if (data.state == ToDoData.ToDoState.NoStart)
            {
                var startBtn = new ButtonView("开始", () =>
                {
                    data.state.Val = ToDoData.ToDoState.Started;
                    needFresh = true;
                });
                Add(startBtn);
            }
            else if (data.state == ToDoData.ToDoState.Started)
            {
                var finishedBtn = new ButtonView("完成", () =>
                {
                    data.state.Val = ToDoData.ToDoState.Done;
                    needRemove = true;
                });
                Add(finishedBtn);
            }
            else if (data.state == ToDoData.ToDoState.Done)
            {
                var finishedBtn = new ButtonView("重置", () =>
                {
                    data.state.Val = ToDoData.ToDoState.NoStart;
                    needRemove = true;
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
                needRemove = true;
            });
            Add(deleteBtn);
        }
    }
}