using System;
using System.Collections;
using System.Collections.Generic;
using _04TodoList.FrameWork.Drawer;
using _04TodoList.FrameWork.Drawer.Interface;
using UnityEngine;

namespace _04TodoList.FrameWork.Drawer
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