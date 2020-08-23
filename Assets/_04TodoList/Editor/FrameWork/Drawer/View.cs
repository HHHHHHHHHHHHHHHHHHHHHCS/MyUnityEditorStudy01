using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using _04ToDoList.Editor.FrameWork.Layout.Interface;
using _04ToDoList.Util;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
    public abstract class View : IView
    {
        public bool Visible { get; set; } = true;

        public ILayout Parent { get; set; }

        public List<GUILayoutOption> guiLayouts { get; } = new List<GUILayoutOption>();

        public GUIStyle guiStyle { get; protected set; } = new GUIStyle();
        public Color backgroundColor { get; set; } = GUI.backgroundColor;


        protected bool beforeDrawCalled = false;
        protected GUILayoutOption[] guiLayoutOptions;

        protected HashSet<EventRecord> eventRecords { get; } = new HashSet<EventRecord>();

        public void Show()
        {
            Visible = true;
            OnShow();
        }

        protected virtual void OnShow()
        {
        }

        public void Hide()
        {
            Visible = false;
            OnHide();
        }


        protected virtual void OnHide()
        {
        }

        public void Refresh()
        {
            OnRefresh();
        }

        protected virtual void OnRefresh()
        {
        }

        public virtual void OnRemove()
        {
        }

        public void OnBeforeDraw()
        {
            if (beforeDrawCalled)
            {
                return;
            }

            beforeDrawCalled = true;
            guiLayoutOptions = guiLayouts.ToArray();
        }

        public void DrawGUI()
        {
            if (Visible)
            {
                var lastBgColor = GUI.backgroundColor;
                GUI.backgroundColor = backgroundColor;
                OnBeforeDraw();
                OnGUI();
                GUI.backgroundColor = lastBgColor;
            }
        }

        public void ParentRemoveThis()
        {
            Parent.Remove(this);
        }

        public T AddTo<T>(ILayout parent) where T : View
        {
            parent.Add(this);
            return this as T;
        }

        public void Dispose()
        {
            UnRegisterAll();
            OnDisposed();
        }

        protected virtual void OnDisposed()
        {
            if (Parent != null)
            {
                ParentRemoveThis();
            }
        }

        protected abstract void OnGUI();

        protected void RegisterEvent(int key, Action<object> onEvent)
        {
            EventDispatcher.Register(key, onEvent);

            eventRecords.Add(new EventRecord()
            {
                key = key,
                onAction = onEvent
            });
        }

        protected void UnRegisterAll()
        {
            foreach (var record in eventRecords)
            {
                EventDispatcher.Remove(record.key, record.onAction);
            }
        }
    }
}