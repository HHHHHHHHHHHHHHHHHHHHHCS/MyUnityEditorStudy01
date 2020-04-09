using System.Collections;
using System.Collections.Generic;
using _04TodoList.FrameWork.Drawer.Interface;
using UnityEngine;

namespace _04TodoList.FrameWork.Layout
{
    public abstract class Layout : IView
    {
        public bool Visible { get; set; } = true;

        public string Style { get; set; }

        protected readonly LinkedList<IView> children = new LinkedList<IView>();

        protected Layout(string style)
        {
            Style = style;
        }

        public void Show()
        {
            Visible = true;
        }

        public void Hide()
        {
            Visible = false;
        }

        public void Add(IView view)
        {
            children.AddLast(view);
        }

        public void Remove(IView view)
        {
            children.Remove(view);
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