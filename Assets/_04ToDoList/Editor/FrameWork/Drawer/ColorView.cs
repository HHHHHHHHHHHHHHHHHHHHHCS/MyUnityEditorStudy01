using System;
using _04ToDoList.Editor.FrameWork.DataBinding;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
	public class ColorView : View
	{
		public Property<Color> ColorProperty { get; }

		public ColorView(Color color, Action<Color> _action = null)
		{
			ColorProperty = new Property<Color>(color, _action);
		}

		protected override void OnGUI()
		{
			ColorProperty.Val = EditorGUILayout.ColorField(ColorProperty.Val);
		}
	}
}