using System;
using System.Collections;
using System.Collections.Generic;
using _04TodoList.FrameWork.DataBinding;
using _04TodoList.FrameWork.Drawer.Interface;
using JetBrains.Annotations;
using UnityEngine;

namespace _04TodoList.FrameWork.Drawer
{
    public class ToggleView : View
    {
        public string text { get; set; }
        public Property<bool> IsToggle { get; private set; }

        public ToggleView(string _text = null, bool isToggle = false)
        {
            this.text = _text;
            IsToggle = new Property<bool>(isToggle);
        }

        protected override void OnGUI()
        {
            IsToggle.Val = GUILayout.Toggle(IsToggle.Val, text);
        }
    }
}