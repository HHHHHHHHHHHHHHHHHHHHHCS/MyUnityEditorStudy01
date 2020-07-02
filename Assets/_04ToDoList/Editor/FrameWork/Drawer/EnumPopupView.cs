using System;
using _04ToDoList.Editor.FrameWork.DataBinding;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
    public class EnumPopupView<T> : View where T : Enum
    {
        public Property<T> ValueProperty { get; }

        private T value { get; }

        public EnumPopupView(T _val, Action<T> bindAct = null)
        {
            value = _val;
            ValueProperty = new Property<T>(_val, bindAct);
        }

        protected override void OnGUI()
        {
            ValueProperty.Val = (T) EditorGUILayout.EnumPopup(ValueProperty.Val, guiLayoutOptions);
        }
    }
}