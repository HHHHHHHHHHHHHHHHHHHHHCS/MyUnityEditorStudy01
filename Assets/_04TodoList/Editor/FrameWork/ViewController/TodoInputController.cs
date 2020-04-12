using System;
using System.Collections.Generic;
using _04TodoList.Editor.FrameWork.DataBinding;
using _04TodoList.Editor.FrameWork.Drawer;
using _04TodoList.Editor.FrameWork.Drawer.Interface;
using _04TodoList.Editor.FrameWork.Layout;
using _04TodoList.Editor.FrameWork.ViewController.Interface;
using UnityEditor;
using UnityEngine;

namespace _04TodoList.Editor.FrameWork.ViewController
{
    public class TodoInputController : IViewController
    {
        private string todoName = string.Empty;

        private Action<string> onInputClick;

        private VerticalLayout layout;
        private TextAreaView inputTextArea;
        private ButtonView unfinishedBtn, finishedBtn;

        public TodoInputController(Action<string> _onInputClick)
        {
            onInputClick = _onInputClick;
            Init();
        }

        private void Init()
        {
            layout = new VerticalLayout("box");
            inputTextArea = new TextAreaView(todoName);
            inputTextArea.Content.Bind(x => todoName = x);
            layout.Add(inputTextArea);
            layout.Add(new ButtonView("添加", () =>
            {
                onInputClick(todoName);
                inputTextArea.Content.Val = string.Empty;
                GUI.FocusControl(null);
            }));
            layout.Add(new SpaceView(5));
        }

        public void Draw()
        {
            layout.DrawGUI();
        }
    }
}