using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewExtension
{
    public static class ViewExtension 
    {
        public static T Width<T>(this T view, float width) where T : IView
        {
            view.guiLayouts.Add(GUILayout.Width(width));
            return view;
        }
    }
}
