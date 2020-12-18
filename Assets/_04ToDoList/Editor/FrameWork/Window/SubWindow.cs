using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using _04ToDoList.Editor.FrameWork.Layout.Interface;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Window
{
	public class SubWindow : EditorWindow, ILayout
	{
		//显示屏蔽接口 不能被外部无法访问
		ILayout IView.Parent { get; set; }
		public List<GUILayoutOption> guiLayouts { get; }

		public readonly List<IView> children = new List<IView>();

		public static T Open<T>(string name = "SubWindow") where T : SubWindow
		{
			var window = GetWindow<T>(true, name, true);

			float width = 1920; //Screen.width
			float height = 1080; //Screen.height


			window.position = new Rect(width / 2f - 150, height / 2f - 150, 300, 300);
			return window;
		}

		//显示屏蔽接口 不能被外部使用 
		void IView.Hide()
		{
		}

		void IView.OnRemove()
		{
		}


		void IDisposable.Dispose()
		{
		}

		private void Update()
		{
			Refresh();
		}

		public void Refresh()
		{
			OnRefresh();
		}

		protected virtual void OnRefresh()
		{
			for (int i = children.Count - 1; i >= 0; --i)
			{
				children[i].Refresh();
			}
		}

		protected void OnGUI()
		{
			DrawGUI();
		}

		public void DrawGUI()
		{
			using (var ptr = children.GetEnumerator())
			{
				while (ptr.MoveNext())
				{
					ptr.Current?.DrawGUI();
				}
			}
		}

		public void Add(IView view)
		{
			children.Add(view);
			view.Parent = this;
		}

		public void Insert(int index, IView view)
		{
			view.Parent = this;
			children.Insert(index, view);
		}

		public void Remove(IView view)
		{
			children.Remove(view);
			view.Parent = null;
		}

		public void Clear()
		{
			foreach (var item in children)
			{
				item.Parent = null;
			}

			children.Clear();
		}
	}
}