using System;
using System.Collections.Generic;
using _04TodoList.FrameWork.Drawer;
using _04TodoList.FrameWork.Drawer.Interface;
using _04TodoList.FrameWork.Layout.Interface;
using JetBrains.Annotations;
using UnityEngine;

namespace _04TodoList.FrameWork.Layout
{
    public class VerticalLayout : Layout
    {
        public VerticalLayout(string style)
            : base(style)
        {
        }

        protected override void OnGUIBegin()
        {
            if (string.IsNullOrEmpty(Style))
            {
                GUILayout.BeginVertical();
            }
            else
            {
                GUILayout.BeginVertical(Style);
            }
        }

        protected override void OnGUI()
        {
            using (var ptr = children.GetEnumerator())
            {
                while (ptr.MoveNext())
                {
                    ptr.Current?.DrawGUI();
                }
            }
        }

        protected override void OnGUIEnd()
        {
            GUILayout.EndVertical();
        }
    }
}