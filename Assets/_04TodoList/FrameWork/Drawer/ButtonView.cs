using System;
using System.Collections;
using System.Collections.Generic;
using _04TodoList.FrameWork.Drawer.Interface;
using UnityEngine;

namespace _04TodoList.FrameWork.Drawer
{
    public class ButtonView : View
    {
        public string text { get; set; }

        public Action OnClickEvent { get; set; }

        public ButtonView(string _text, Action onClickEvent = null)
        {
            text = _text;
            OnClickEvent = onClickEvent;
        }

        protected override void OnGUI()
        {
            if (GUILayout.Button(text ?? string.Empty))
            {
                OnClickEvent?.Invoke();
            }
        }
    }
}