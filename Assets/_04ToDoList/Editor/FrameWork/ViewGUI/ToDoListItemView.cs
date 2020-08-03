using System;
using System.Collections.Generic;
using _04ToDoList.Editor.Component;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.Layout.Interface;
using _04ToDoList.Editor.FrameWork.Window;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListItemView : VerticalLayout
    {
        private static readonly Texture2D playIcon;
        private static readonly Texture2D finishIcon;
        private static readonly Texture2D resetIcon;
        private static readonly Texture2D editorIcon;
        private static readonly Texture2D deleteIcon;

        private static Queue<Action> cmdQueue = new Queue<Action>();

        private HorizontalLayout container;

        public ToDoData data;
        public Action<ToDoListItemView> changeProperty;
        public bool showTime = false;

        private bool needFresh;

        static ToDoListItemView()
        {
            playIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_04ToDoList/EditorIcons/Play.png");
            finishIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_04ToDoList/EditorIcons/Finish.png");
            resetIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_04ToDoList/EditorIcons/Reset.png");
            editorIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_04ToDoList/EditorIcons/Editor.png");
            deleteIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_04ToDoList/EditorIcons/Delete.png");
        }

        public ToDoListItemView(ToDoData _item, Action<ToDoListItemView> _changeProperty, bool _showTime = false)
            : base()
        {
            this.data = _item;
            this.changeProperty = _changeProperty;
            this.showTime = _showTime;
            container = new HorizontalLayout();
            Add(container);
            Add(new SpaceView(4));
            BuildItem();
        }

        protected override void OnRefresh()
        {
            if (needFresh)
            {
                needFresh = false;
                changeProperty(this);
            }

            while (cmdQueue.Count > 0)
            {
                cmdQueue.Dequeue()?.Invoke();
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
                    needFresh = true;
                }).Height(20).Width(40).BackgroundColor(Color.green);
                container.Add(finishedBtn);
            }
            else if (data.state == ToDoData.ToDoState.Done)
            {
                var resetBtn = new ImageButtonView(resetIcon, () =>
                {
                    data.createTime = DateTime.Now;
                    data.state.Val = ToDoData.ToDoState.NoStart;
                    needFresh = true;
                }).Height(20).Width(40).BackgroundColor(Color.grey);
                container.Add(resetBtn);
            }

            var boxView = new BoxView("无").AddTo(container)
                .TextMiddleCenter().Width(20).FontSize(12)
                .FontColor(Color.white).TheFontStyle(FontStyle.Bold);

            new CategoryComponent(data.category).AddTo(container);

            var contentLabel = new LabelView(data.content).Height(20).FontSize(15).TextMiddleCenter();
            container.Add(contentLabel);

            if (showTime)
            {
                container.Add(new LabelView(data.finishTime.ToString("完成于 HH:mm:ss"))
                    .Height(20).Width(80).TextLowRight());
                container.Add(new LabelView(data.UsedTimeText)
                    .Height(20).Width(100).TextLowRight());
            }

            var priorityVal = data.priority.Val;
            Color priorityColor = Color.clear;
            switch (priorityVal)
            {
                case ToDoData.ToDoPriority.A:
                    boxView.Text = "A";
                    priorityColor = Color.red;
                    boxView.BackgroundColor(Color.red);
                    break;
                case ToDoData.ToDoPriority.B:
                    boxView.Text = "B";
                    priorityColor = Color.yellow;
                    boxView.BackgroundColor(Color.yellow);
                    break;
                case ToDoData.ToDoPriority.C:
                    boxView.Text = "C";
                    priorityColor = Color.cyan;
                    boxView.BackgroundColor(Color.cyan);
                    break;
                case ToDoData.ToDoPriority.D:
                    boxView.Text = "D";
                    priorityColor = Color.blue;
                    boxView.BackgroundColor(Color.blue);
                    break;
                case ToDoData.ToDoPriority.None:
                    boxView.Text = "无";
                    priorityColor = Color.gray;
                    boxView.BackgroundColor(Color.gray);
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
                        boxView.Text = "A";
                        priority.BackgroundColor(Color.red);
                        boxView.BackgroundColor(Color.red);
                        break;
                    case ToDoData.ToDoPriority.B:
                        boxView.Text = "B";
                        priority.BackgroundColor(Color.yellow);
                        boxView.BackgroundColor(Color.yellow);
                        break;
                    case ToDoData.ToDoPriority.C:
                        boxView.Text = "C";
                        priority.BackgroundColor(Color.cyan);
                        boxView.BackgroundColor(Color.cyan);
                        break;
                    case ToDoData.ToDoPriority.D:
                        boxView.Text = "D";
                        priority.BackgroundColor(Color.blue);
                        boxView.BackgroundColor(Color.blue);
                        break;
                    case ToDoData.ToDoPriority.None:
                        boxView.Text = "无";
                        priority.BackgroundColor(Color.gray);
                        boxView.BackgroundColor(Color.gray);
                        break;
                }

                changeProperty(this);
            });

            new ImageButtonView(editorIcon, () => { OpenSubWindow(); })
                .Width(25).Height(25).BackgroundColor(Color.black).AddTo(container);

            var deleteBtn = new ImageButtonView(deleteIcon, () =>
            {
                data.finished.ClearValueChanged();
                var todoListCls = ToDoListCls.ModelData;
                todoListCls.todoList.Remove(data);
                todoListCls.Save();
                needFresh = true;
            }).Height(20).Width(30).BackgroundColor(Color.red);
            container.Add(deleteBtn);
        }

        public void UpdateItem()
        {
            needFresh = true;

            //如果不focus 则会不刷新
            ToDoListMainWindow.instance.Focus();
        }

        private void OpenSubWindow()
        {
            cmdQueue.Enqueue(() =>
                {
                    var window = ToDoListEditorSubWindow.Open(this, "ToDo 编辑器");
                    window.ShowWindow(this);
                }
            );
        }
    }
}