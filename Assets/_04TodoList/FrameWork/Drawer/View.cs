using System.Collections;
using System.Collections.Generic;
using _04TodoList.FrameWork.Drawer;
using _04TodoList.FrameWork.Drawer.Interface;
using UnityEngine;

namespace _04TodoList.FrameWork.Drawer
{
    public abstract class View : IView
    {
        public bool Visible { get; set; } = true;

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