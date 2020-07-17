using System;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
    public class ButtonView : View
    {
        public string text { get; set; }

        public Action OnClickEvent { get; set; }

        public bool fullSize = false;

        public ButtonView(string _text, Action onClickEvent = null, bool _fullSize = false)
        {
            text = _text;
            OnClickEvent = onClickEvent;
            fullSize = _fullSize;
        }

        protected override void OnGUI()
        {
            bool isClick;

            if (guiLayouts != null && guiLayouts.Count > 0)
            {
                isClick = GUILayout.Button(text ?? string.Empty, guiLayouts.ToArray());
            }
            else if (fullSize)
            {
                isClick = GUILayout.Button(text ?? string.Empty);
            }
            else
            {
                isClick = GUILayout.Button(text ?? string.Empty, GUILayout.Width(40));
            }


            if (isClick)
            {
                OnClickEvent?.Invoke();
            }
        }
    }
}