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
    public class ToDoListItemView : VerticalLayout
    {
        private static Texture2D playIcon;
        private static Texture2D finishIcon;
        private static Texture2D resetIcon;
        private static Texture2D deleteIcon;


        private HorizontalLayout container;

        public ToDoData data;
        public Action<ToDoListItemView> removeAct;
        public bool showTime = false;


        private bool needFresh;
        private bool needRemove;


        static ToDoListItemView()
        {
            playIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_04ToDoList/EditorIcons/Play.png");
            finishIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_04ToDoList/EditorIcons/Finish.png");
            resetIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_04ToDoList/EditorIcons/Reset.png");
            deleteIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_04ToDoList/EditorIcons/Delete.png");
        }

        public ToDoListItemView(ToDoData _item, Action<ToDoListItemView> _removeAct, bool _showTime = false)
            : base()
        {
            this.data = _item;
            this.removeAct = _removeAct;
            this.showTime = _showTime;
            container = new HorizontalLayout();
            Add(container);
            Add(new SpaceView(4));
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
            container.Clear();

            if (data.state == ToDoData.ToDoState.NoStart)
            {
                var startBtn = new ImageButtonView(playIcon, () =>
                {
                    data.startTime = DateTime.Now;
                    data.state.Val = ToDoData.ToDoState.Started; //改变了state会自动储存
                    needFresh = true;
                }).Height(20).Width(40).BackgroundColor(Color.green);
                container.Add(startBtn);
            }
            else if (data.state == ToDoData.ToDoState.Started)
            {
                var finishedBtn = new ImageButtonView(finishIcon, () =>
                {
                    data.finishTime = DateTime.Now;
                    data.state.Val = ToDoData.ToDoState.Done;
                    needRemove = true;
                }).Height(20).Width(40).BackgroundColor(Color.green);
                container.Add(finishedBtn);
            }
            else if (data.state == ToDoData.ToDoState.Done)
            {
                var resetBtn = new ImageButtonView(resetIcon, () =>
                {
                    data.createTime = DateTime.Now;
                    data.state.Val = ToDoData.ToDoState.NoStart;
                    needRemove = true;
                }).Height(20).Width(40).BackgroundColor(Color.grey);
                container.Add(resetBtn);
            }

            var priorityVal = data.priority.Val;
            Color priorityColor = Color.clear;
            switch (priorityVal)
            {
                case ToDoData.ToDoPriority.A:
                    priorityColor = Color.red;
                    break;
                case ToDoData.ToDoPriority.B:
                    priorityColor = Color.yellow;
                    break;
                case ToDoData.ToDoPriority.C:
                    priorityColor = Color.cyan;
                    break;
                case ToDoData.ToDoPriority.D:
                    priorityColor = Color.blue;
                    break;
                case ToDoData.ToDoPriority.None:
                    priorityColor = Color.clear;
                    break;
            }

            var priority = new EnumPopupView<ToDoData.ToDoPriority>(priorityVal)
                .Width(30).Height(20).BackgroundColor(priorityColor).AddTo(container);

            priority.ValueProperty.RegisterValueChanged((val) =>
            {
                data.priority.Val = val;

                switch (val)
                {
                    case ToDoData.ToDoPriority.A:
                        priority.BackgroundColor(Color.red);
                        break;
                    case ToDoData.ToDoPriority.B:
                        priority.BackgroundColor(Color.yellow);
                        break;
                    case ToDoData.ToDoPriority.C:
                        priority.BackgroundColor(Color.cyan);
                        break;
                    case ToDoData.ToDoPriority.D:
                        priority.BackgroundColor(Color.blue);
                        break;
                    case ToDoData.ToDoPriority.None:
                        priority.BackgroundColor(Color.clear); 
                        break;
                }
            });

            var contentLabel = new LabelView(data.content).Height(20).FontSize(15).TextMiddleLeft();
            container.Add(contentLabel);

            if (showTime)
            {
                container.Add(new LabelView(data.finishTime.ToString("完成于 HH:mm:ss"))
                    .Height(20).Width(80).TextLowRight());
                container.Add(new LabelView(data.UsedTimeText)
                    .Height(20).Width(100).TextLowRight());
            }

            var deleteBtn = new ImageButtonView(deleteIcon, () =>
            {
                data.finished.ClearValueChanged();
                var todoListCls = ToDoListCls.ModelData;
                todoListCls.todoList.Remove(data);
                todoListCls.Save();
                needRemove = true;
            }).Height(20).Width(30).BackgroundColor(Color.red);
            container.Add(deleteBtn);
        }
    }
}