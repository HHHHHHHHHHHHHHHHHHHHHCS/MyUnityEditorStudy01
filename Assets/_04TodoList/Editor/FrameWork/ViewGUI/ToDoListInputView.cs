using System;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListInputView : HorizontalLayout
    {
        private string todoName = string.Empty;

        public ToDoListInputView(Action<string> onInputClick)
            : base("box")
        {
            var inputTextArea = new TextAreaView(todoName);
            inputTextArea.Content.Bind(x => todoName = x);
            Add(inputTextArea);

            Add(new ButtonView("添加", () =>
            {
                onInputClick(todoName);
                inputTextArea.Content.Val = string.Empty;
                GUI.FocusControl(null);
            }));
        }
    }
}