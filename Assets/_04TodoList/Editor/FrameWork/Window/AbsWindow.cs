using System;
using _04ToDoList.Editor.FrameWork.ViewController;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Window
{
    public abstract class AbsWindow : EditorWindow
    {
        protected bool isShow;

        public static T CreateViewController<T>() where T : AbsViewController, new()
        {
            T t = new T();
            return t;
        }

        public static void OnOpen<T>(bool isUtility, string windowName, bool focus) where T : AbsWindow
        {
            var window = GetWindow<T>(isUtility, windowName, focus);

            if (window.isShow)
            {
                window.Close();
                window.isShow = false;
            }
            else
            {
                //if is isUtility can't show icon
                //var texture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_04ToDoList/EditorIcons/Main.png");
                //window.titleContent = new GUIContent("ToDoLists", texture);

                window.ShowUtility();
                window.isShow = true;
            }
        }

        protected void OnEnable()
        {
            OnInit();
        }

        public void OnDisable()
        {
            Disable();
        }

        public void OnDestroy()
        {
            Dispose();
        }

        protected abstract void OnGUI();

        protected abstract void OnInit();
        protected abstract void Disable();
        protected abstract void Dispose();
    }
}