using _04ToDoList.Editor.FrameWork.Drawer;
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

        public static T FontSize<T>(this T view, int fontSize) where T : View
        {
            view.style.fontSize = fontSize;
            return view;
        }

        public static T TextPosition<T>(this T view, TextAnchor textAnchor) where T : View
        {
            view.style.alignment = textAnchor;
            return view;
        }

        public static T TextMiddleCenter<T>(this T view) where T : View
        {
            return TextPosition(view, TextAnchor.MiddleCenter);
        }

        public static T TextMiddleLeft<T>(this T view) where T : View
        {
            return TextPosition(view, TextAnchor.MiddleLeft);
        }

        public static T BackgroundColor<T>(this T view, Color color) where T : View
        {
            view.backgroundColor = color;
            return view;
        }
    }
}