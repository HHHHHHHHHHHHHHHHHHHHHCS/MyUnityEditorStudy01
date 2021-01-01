using System;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
    public class ButtonView : View
    {
        public string Text { get; set; }

        public Action OnClickEvent { get; set; }

        public bool fullSize = false;

        public ButtonView(string _text, Action onClickEvent = null, bool _fullSize = false)
        {
            Text = _text;
            OnClickEvent = onClickEvent;
            fullSize = _fullSize;
            guiStyle = new GUIStyle(GUI.skin.button);
        }

        protected override void OnGUI()
        {
            bool isClick;

            if (guiLayoutOptions != null && guiLayoutOptions.Length > 0)
            {
                isClick = GUILayout.Button(Text ?? string.Empty, guiStyle, guiLayoutOptions);
            }
            else if (fullSize)
            {
                isClick = GUILayout.Button(Text ?? string.Empty, guiStyle);
            }
            else
            {
                isClick = GUILayout.Button(Text ?? string.Empty, guiStyle, GUILayout.Width(40));
            }


            if (isClick)
            {
                OnClickEvent?.Invoke();
            }
        }
    }
}