using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork
{
    public static class ViewExtension 
    {
        public static T Width<T>(this T view, float width) where T : IView
        {
            view.guiLayouts.Add(GUILayout.Width(width));
            return view;
        }

        public static T Height<T>(this T view, float height) where T : IView
        {
            view.guiLayouts.Add(GUILayout.Height(height));
            return view;
        }
    }
}
