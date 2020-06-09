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
            var horizontalLayout = new HorizontalLayout("box");

            var inputTextArea = new TextAreaView(todoName);
            inputTextArea.Content.Bind(x => todoName = x);
            horizontalLayout.Add(inputTextArea);

            var inputBtn = new ButtonView("添加", () =>
            {
                if (!string.IsNullOrEmpty(todoName))
                {
                    onInputClick(todoName);
                    inputTextArea.Content.Val = string.Empty;
                    GUI.FocusControl(null);
                }
            });

            horizontalLayout.Add(inputBtn);

            Add(horizontalLayout);

        }
    }
}