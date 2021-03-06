﻿using System;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
    public class CustomView : View
    {
        public Action OnGUIAction { get; set; }


        public CustomView(Action onGuiAction)
        {
            OnGUIAction = onGuiAction;
        }

        protected override void OnGUI()
        {
            OnGUIAction?.Invoke();
        }
    }
}