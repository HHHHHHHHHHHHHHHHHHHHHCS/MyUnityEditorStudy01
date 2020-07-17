using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using _04ToDoList.Editor.FrameWork.Layout.Interface;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Window
{
    public class SubWindow : EditorWindow, ILayout
    {
        //隐式屏蔽接口 无法访问
        ILayout IView.Parent { get; set; }
        public List<GUILayoutOption> guiLayouts { get; }

        public readonly List<IView> children = new List<IView>();

        public static SubWindow Open(string name = "SubWindow")
        {
            var window = GetWindow<SubWindow>(true, name, true);
            window.position = new Rect(Screen.width / 2, Screen.height / 2, 300, 300);
            return window;
        }

        //隐式屏蔽接口 不能使用  约等于空方法
        void IView.Hide()
        {
        }

        public void Refresh()
        {
            OnRefresh();
        }

        protected virtual void OnRefresh()
        {
            for (int i = children.Count - 1; i >= 0; --i)
            {
                children[i].Refresh();
            }
        }

        protected void OnGUI()
        {
            DrawGUI();
        }

        public void DrawGUI()
        {
            using (var ptr = children.GetEnumerator())
            {
                while (ptr.MoveNext())
                {
                    ptr.Current?.DrawGUI();
                }
            }
        }

        public void Add(IView view)
        {
            children.Add(view);
            view.Parent = this;
        }

        public void Remove(IView view)
        {
            children.Remove(view);
            view.Parent = null;
        }

        public void Clear()
        {
            foreach (var item in children)
            {
                item.Parent = null;
            }

            children.Clear();
        }
    }
}