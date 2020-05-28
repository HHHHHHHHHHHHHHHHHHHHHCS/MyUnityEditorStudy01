using System;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
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
            if (GUILayout.Button(text ?? string.Empty, GUILayout.Width(40)))
            {
                OnClickEvent?.Invoke();
            }
        }
    }
}