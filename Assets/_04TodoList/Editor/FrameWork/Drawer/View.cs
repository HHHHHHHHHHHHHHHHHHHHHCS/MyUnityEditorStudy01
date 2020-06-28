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
        public GUIStyle guiStyle { get; protected set; } = new GUIStyle();
        public Color backgroundColor { get; set; } = GUI.backgroundColor;

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

        protected abstract void OnGUI();
    }
}