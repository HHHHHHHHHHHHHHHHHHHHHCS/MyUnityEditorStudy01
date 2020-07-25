using System;
using _04ToDoList.Editor.FrameWork.DataBinding;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
    public class PopupView : View
    {
        public Property<int> ValueProperty { get; }

        public string[] MenuArray { get; }

        public PopupView(int initValue, string[] menuArray, Action<int> changeAct = null)
        {
            ValueProperty = new Property<int>(initValue, changeAct);
            MenuArray = menuArray;
            guiStyle = EditorStyles.popup;
        }

        protected override void OnGUI()
        {
            ValueProperty.Val = EditorGUILayout.Popup(ValueProperty.Val, MenuArray, guiStyle, guiLayoutOptions);
        }
    }
}