using System;
using System.Collections;
using System.Collections.Generic;
using _04TodoList.FrameWork.Drawer;
using UnityEngine;

namespace _04TodoList.FrameWork.Drawer
{
    public class CustomView : IView
    {
        public Action OnGUIAction { get; set; }


        public CustomView(Action onGuiAction)
        {
            OnGUIAction = onGuiAction;
        }

        public void OnGUI()
        {
            OnGUIAction?.Invoke();
        }
    }
}