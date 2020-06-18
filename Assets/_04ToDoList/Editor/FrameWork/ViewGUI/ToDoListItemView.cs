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

        private static Texture2D playIcon;
        private static Texture2D finishIcon;
        private static Texture2D resetIcon;
        private static GUIStyle fontStyle;

        static ToDoListItemView()
        {
            playIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_04ToDoList/EditorIcons/Play.png");
            finishIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_04ToDoList/EditorIcons/Finish.png");
            resetIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_04ToDoList/EditorIcons/Reset.png");
            fontStyle = new GUIStyle() {fontSize = 14};
        }

        public ToDoListItemView(ToDoData _item, Action<ToDoListItemView> _removeAct)
            : base()
        {
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

            if (data.state == ToDoData.ToDoState.NoStart)
            {
                var startBtn = new ImageButtonView(playIcon, () =>
                {
                    data.state.Val = ToDoData.ToDoState.Started;
                    needFresh = true;
                }).Height(20).Width(40);
                Add(startBtn);
            }
            else if (data.state == ToDoData.ToDoState.Started)
            {
                var finishedBtn = new ImageButtonView(finishIcon, () =>
                {
                    data.state.Val = ToDoData.ToDoState.Done;
                    needRemove = true;
                }).Height(20).Width(40);
                Add(finishedBtn);
            }
            else if (data.state == ToDoData.ToDoState.Done)
            {
                var resetBtn = new ImageButtonView(resetIcon, () =>
                {
                    data.state.Val = ToDoData.ToDoState.NoStart;
                    needRemove = true;
                }).Height(20).Width(40);
                Add(resetBtn);
            }


            var contentLabel = new CustomView(() =>
            {
                GUILayout.Label(data.content
                    , fontStyle, GUILayout.Height(30));
            });
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