﻿using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Layout
{
    public class ScrollLayout : Layout
    {
        private Vector2 scrollPos = Vector2.zero;

        protected override void OnGUIBegin()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        }

        protected override void OnGUIEnd()
        {
            EditorGUILayout.EndScrollView();
        }
    }
}