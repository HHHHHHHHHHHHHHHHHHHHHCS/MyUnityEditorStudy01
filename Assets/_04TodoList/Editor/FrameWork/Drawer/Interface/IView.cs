using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.Layout.Interface;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer.Interface
{
	public interface IView : IDisposable
	{
		ILayout Parent { get; set; }

		void Show();

		void Hide();

		void OnRemove();

		void Refresh();

		void AddGUILayouts(GUILayoutOption option);

		void DrawGUI();
	}
}