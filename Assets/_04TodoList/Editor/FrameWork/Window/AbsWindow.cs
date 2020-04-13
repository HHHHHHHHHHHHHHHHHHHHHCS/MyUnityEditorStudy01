using System;
using UnityEditor;
using UnityEngine;

namespace _04TodoList.Editor.FrameWork.Window
{
    public abstract class AbsWindow : EditorWindow
    {
        protected bool isShow;

        public static void OnOpen<T>(bool isUtility , string windowName,bool focus ) where T : AbsWindow
        {
            var window = GetWindow<T>(isUtility, windowName, focus);

            if (window.isShow)
            {
                window.Close();
                window.isShow = false;
            }
            else
            {
                //var texture = Resources.Load<Texture2D>("main");
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