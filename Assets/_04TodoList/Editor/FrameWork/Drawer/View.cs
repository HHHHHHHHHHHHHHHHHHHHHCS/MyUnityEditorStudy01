using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using _04ToDoList.Editor.FrameWork.Layout.Interface;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
    public abstract class View : IView
    {
        public bool Visible { get; set; } = true;

        public ILayout Parent { get; set; }

        public List<GUILayoutOption> guiLayouts { get; } = new List<GUILayoutOption>();

        protected bool beforeDrawCalled = false;
        protected GUILayoutOption[] guiLayoutOptions;

        public void Show()
        {
            Visible = true;
        }

        public void Hide()
        {
            Visible = false;
        }

        public void OnBeforeDraw()
        {
            if (!beforeDrawCalled)
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
                OnBeforeDraw();
                OnGUI();
            }
        }

        protected abstract void OnGUI();
    }
}