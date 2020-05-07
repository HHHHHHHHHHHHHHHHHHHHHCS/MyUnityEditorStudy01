﻿using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using _04ToDoList.Editor.FrameWork.Layout.Interface;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
    public abstract class View : IView
    {
        public bool Visible { get; set; } = true;

        public ILayout Parent { get; set; }

        public void Show()
        {
            Visible = true;
        }

        public void Hide()
        {
            Visible = false;
        }


        public void DrawGUI()
        {
            if (Visible)
            {
                OnGUI();
            }
        }

        protected abstract void OnGUI();
    }
}