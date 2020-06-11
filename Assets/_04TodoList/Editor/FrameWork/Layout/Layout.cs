﻿using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using _04ToDoList.Editor.FrameWork.Layout.Interface;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Layout
{
    public abstract class Layout : IView
    {
        public bool Visible { get; set; } = true;

        public string Style { get; set; }

        protected readonly List<IView> children = new List<IView>();

        public List<GUILayoutOption> guiLayouts { get; } = new List<GUILayoutOption>();

        public ILayout Parent { get; set; }

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
            OnRefresh();
        }

        protected virtual void OnRefresh()
        {
            for (int i = children.Count - 1; i >= 0; --i)
            {
                children[i].Refresh();
            }
 
        }

        public void Add(IView view)
        {
            view.Parent = this as ILayout;
            children.Add(view);
        }

        public void Remove(IView view)
        {
            view.Parent = null;
            children.Remove(view);
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

        protected abstract void OnGUI();

        protected abstract void OnGUIEnd();
    }
}