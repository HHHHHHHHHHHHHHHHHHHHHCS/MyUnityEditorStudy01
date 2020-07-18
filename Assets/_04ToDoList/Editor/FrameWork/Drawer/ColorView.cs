using System;
using _04ToDoList.Editor.FrameWork.DataBinding;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
    public class ColorView : View
    {
        public Property<Color> colorProperty;

        public ColorView(Color color, Action<Color> _action = null)
        {
            colorProperty = new Property<Color>(color, _action);
        }

        protected override void OnGUI()
        {
            colorProperty.Val = EditorGUILayout.ColorField(colorProperty.Val);
        }
    }
}