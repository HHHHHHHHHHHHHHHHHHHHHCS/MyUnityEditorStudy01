using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.Layout.Interface;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer.Interface
{
    public interface IView
    {
        ILayout Parent { get; set; }

        List<GUILayoutOption> guiLayouts { get; }

        void Show();

        void Hide();

        void Refresh();

        void DrawGUI();
    }
}