using System;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListInputView : HorizontalLayout
    {
        private static Texture2D addIcon;

        private string todoName = string.Empty;

        static ToDoListInputView()
        {
            addIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_04ToDoList/EditorIcons/Add.png");
        }

        public ToDoListInputView(Action<string> onInputClick)
            : base("box")
        {

            var inputTextArea = new TextAreaView(todoName).Height(20);
            inputTextArea.Content.Bind(x => todoName = x);
            Add(inputTextArea);

            var addBtn = new ImageButtonView(addIcon, () =>
            {
                if (!string.IsNullOrEmpty(todoName))
                {
                    onInputClick(todoName);
                    inputTextArea.Content.Val = string.Empty;
                    GUI.FocusControl(null);
                }
            }).Width(20).Height(20).BackgroundColor(Color.yellow);

            Add(addBtn);

        }
    }
}