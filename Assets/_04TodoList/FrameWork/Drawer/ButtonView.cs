using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _04TodoList.FrameWork.Drawer
{
    public class ButtonView : IView
    {
        public string text { get; set; }

        public Action OnClickEvent { get; set; }

        public ButtonView(string _text, Action onClickEvent = null)
        {
            text = _text;
            OnClickEvent = onClickEvent;
        }


        public void OnGUI()
        {
            if (GUILayout.Button(text ?? string.Empty))
            {
                OnClickEvent?.Invoke();
            }
        }
    }
}