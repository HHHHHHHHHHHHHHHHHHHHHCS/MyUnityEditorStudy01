using System;
using _04TodoList.Editor.FrameWork.Drawer;
using _04TodoList.Editor.FrameWork.Layout;
using UnityEngine;

namespace _04TodoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListInputView : VerticalLayout
    {
        private string todoName = string.Empty;

        public ToDoListInputView(Action<string> onInputClick)
            : base("box")
        {
            Add(new SpaceView(1));
            var inputTextArea = new TextAreaView(todoName);
            inputTextArea.Content.Bind(x => todoName = x);
            Add(inputTextArea);
            Add(new SpaceView(0.1f));

            Add(new ButtonView("添加", () =>
            {
                onInputClick(todoName);
                inputTextArea.Content.Val = string.Empty;
                GUI.FocusControl(null);
            }));
            Add(new SpaceView(3));
        }
    }
}