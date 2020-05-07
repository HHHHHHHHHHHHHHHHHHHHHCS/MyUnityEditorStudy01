using _04ToDoList.Editor.FrameWork.DataBinding;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
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
            Content.Val = EditorGUILayout.TextArea(Content.Val);
        }
    }
}