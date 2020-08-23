using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using _04ToDoList.Editor.FrameWork.Layout.Interface;
using _04ToDoList.Util;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Layout
{
    public abstract class Layout : ILayout
    {
        public bool Visible { get; set; } = true;

        public string Style { get; set; }

        public readonly List<IView> children = new List<IView>();

        public List<GUILayoutOption> guiLayouts { get; } = new List<GUILayoutOption>();

        public ILayout Parent { get; set; }

        public Queue<Action> cmdQueue = new Queue<Action>();

        protected HashSet<EventRecord> eventRecords { get; } = new HashSet<EventRecord>();

        protected Layout(string style = null)
        {
            Style = style;
        }

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
            if (!Visible)
            {
                return;
            }

            while (cmdQueue.Count > 0)
            {
                cmdQueue.Dequeue()?.Invoke();
            }

            OnRefresh();

            for (int i = children.Count - 1; i >= 0; --i)
            {
                children[i].Refresh();
            }
        }

        protected virtual void OnRefresh()
        {

        }

        public virtual void OnRemove()
        {

        }

        public void Add(IView view)
        {
            view.Parent = this;
            children.Add(view);
        }

        public void Remove(IView view)
        {
            view.Parent = null;
            children.Remove(view);
            view.OnRemove();
        }

        public void ParentRemoveThis()
        {
            Parent.Remove(this);
        }

        public void EnqueueCmd(Action act)
        {
            cmdQueue.Enqueue(act);
        }

        public void Clear()
        {
            foreach (var child in children)
            {
                child.Parent = null;
            }

            children.Clear();
        }


        public void DrawGUI()
        {
            if (Visible)
            {
                OnGUIBegin();
                OnGUI();
                OnGUIEnd();
            }
        }

        protected abstract void OnGUIBegin();

        protected virtual void OnGUI()
        {
            using (var ptr = children.GetEnumerator())
            {
                while (ptr.MoveNext())
                {
                    ptr.Current?.DrawGUI();
                }
            }
        }

        protected abstract void OnGUIEnd();

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