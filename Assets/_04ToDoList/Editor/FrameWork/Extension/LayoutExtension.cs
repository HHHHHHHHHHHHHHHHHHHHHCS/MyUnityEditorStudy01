using System.Collections;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using _04ToDoList.Editor.FrameWork.Layout.Interface;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork
{
	public static class LayoutExtension
	{
		public static T AddTo<T>(this T view, ILayout parent) where T : IView
		{
			parent.Add(view);
			return view;
		}

		public static T InsertTo<T>(this T view, int index, ILayout parent) where T : IView
		{
			parent.Insert(index, view);
			return view;
		}
	}
}