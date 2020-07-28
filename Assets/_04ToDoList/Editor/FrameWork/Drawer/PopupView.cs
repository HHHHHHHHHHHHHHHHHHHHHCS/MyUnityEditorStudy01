using System;
using _04ToDoList.Editor.FrameWork.DataBinding;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
    public class PopupView : View
    {
        public Property<int> ValueProperty { get; }

        public string[] MenuArray { get; set; }

        public PopupView(int initValue, string[] menuArray, Action<int> changeAct = null)
        {
            ValueProperty = new Property<int>(initValue, changeAct);
            MenuArray = menuArray;
            guiStyle = EditorStyles.popup;
        }

        protected override void OnGUI()
        {
            if (ValueProperty.Val >= 0 && MenuArray != null)
            {
                ValueProperty.Val = EditorGUILayout.Popup(ValueProperty.Val, MenuArray, guiStyle, guiLayoutOptions);
            }
        }
    }
}