using System;
using System.Collections.Generic;
using _04TodoList.FrameWork.Drawer;
using _04TodoList.FrameWork.Layout.Interface;
using JetBrains.Annotations;
using UnityEngine;

namespace _04TodoList.FrameWork.Layout
{
    public class VerticalLayout : ILayout
    {
        public string Style { get; set; }

        private LinkedList<IView> children = new LinkedList<IView>();

        public VerticalLayout(string style)
        {
            Style = style;
        }

        public void Add(IView view)
        {
            children.AddLast(view);
        }

        public void Remove(IView view)
        {
            children.Remove(view);
        }

        public void OnGUI()
        {
            if (string.IsNullOrEmpty(Style))
            {
                GUILayout.BeginVertical();
            }
            else
            {
                GUILayout.BeginVertical(Style);
            }
            using (var ptr = children.GetEnumerator())
            {
                while (ptr.MoveNext())
                {
                    ptr.Current?.OnGUI();
                }
            }

            GUILayout.EndVertical();
        }
    }
}