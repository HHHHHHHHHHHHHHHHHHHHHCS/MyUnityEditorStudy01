using System.Collections;
using System.Collections.Generic;
using _04TodoList.FrameWork.DataBinding;
using _04TodoList.FrameWork.Drawer.Interface;
using UnityEditor;
using UnityEngine;

namespace _04TodoList.FrameWork.Drawer
{
    public class TextAreaView : View
    {
        public Property<string> Content { get; set; }

        public TextAreaView(string content)
        {
            Content = new Property<string>(content);
        }

        protected override void OnGUI()
        {
            Content.Val = EditorGUILayout.TextArea(Content);
        }
    }
}