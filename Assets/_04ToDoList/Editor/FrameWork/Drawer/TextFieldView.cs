using System;
using _04ToDoList.Editor.FrameWork.DataBinding;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
    public class TextFieldView : View
    {
        public Property<string> Content { get; set; }

        public TextFieldView(string content, Action<string> _act = null)
        {
            Content = new Property<string>(content, _act);
            guiStyle = new GUIStyle(GUI.skin.textField);
        }

        protected override void OnGUI()
        {
            Content.Val = EditorGUILayout.TextArea(Content.Val, guiStyle, guiLayoutOptions);
        }
    }
}
