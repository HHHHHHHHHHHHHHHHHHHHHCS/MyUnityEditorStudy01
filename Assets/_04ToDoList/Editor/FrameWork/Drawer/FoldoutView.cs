using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using _04ToDoList.Editor.FrameWork.Layout;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
	public class FoldoutView : View
	{
		public Property<bool> Spread { get; }

		public string Content { get; set; }

		private List<IView> visibleViews;

		private List<IView> foldoutViews;

		public FoldoutView(bool _spread, string _content, Action<bool> changeAct = null)
		{
			Spread = new Property<bool>(_spread, changeAct);
			Content = _content;
			guiStyle = new GUIStyle(EditorStyles.foldout);
			visibleViews = new List<IView>();
			foldoutViews = new List<IView>();
		}

		protected override void OnGUI()
		{
			Spread.Val = EditorGUILayout.Foldout(Spread.Val, Content, true, guiStyle);

			foreach (var view in visibleViews)
			{
				view.DrawGUI();
			}

			if (Spread.Val)
			{
				foreach (var view in foldoutViews)
				{
					view.DrawGUI();
				}
			}
		}

		protected override void OnRefresh()
		{
			base.OnRefresh();
			foreach (var view in visibleViews)
			{
				view.Refresh();
			}

			if (Spread.Val)
			{
				foreach (var view in foldoutViews)
				{
					view.Refresh();
				}
			}
		}

		public FoldoutView AddVisibleView(IView view)
		{
			visibleViews.Add(view);
			return this;
		}

		public FoldoutView RemoveVisibleView(IView view)
		{
			visibleViews.Remove(view);
			return this;
		}

		public FoldoutView AddFoldoutView(IView view)
		{
			foldoutViews.Add(view);
			return this;
		}

		public FoldoutView RemoveFoldoutView(IView view)
		{
			foldoutViews.Remove(view);
			return this;
		}
	}
}