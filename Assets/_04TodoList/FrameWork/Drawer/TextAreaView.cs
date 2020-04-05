using System.Collections;
using System.Collections.Generic;
using _04TodoList.FrameWork.DataBinding;
using UnityEngine;

namespace _04TodoList.FrameWork.Drawer
{
    public class TextAreaView : IView
    {
        public Property<string> Content { get; set; }

        public TextAreaView(string content)
        {
            Content = new Property<string>(content);
        }

        public void OnGUI()
        {
            Content.Val = GUILayout.TextArea(Content);
        }
    }
}