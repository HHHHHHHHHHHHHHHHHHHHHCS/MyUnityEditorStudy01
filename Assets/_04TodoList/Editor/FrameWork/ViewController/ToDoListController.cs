using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.ViewGUI;
using _04ToDoList.Util;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewController
{
	public class ToDoListController : AbsViewController
	{
		private bool isShow;

		private ToolBarView todoListToolBarView;

		private ToDoListNoteView todoListNoteView;
		private ToDoListView todoListView;
		private ToDoListHideView todoListHideView;
		private ToDoListCategoryListView todoListCategoryListView;
		private ToDoListFinishedView todoListFinishedView;
		private ToDoListProductView todoListProductView;


		protected override void SetUpView()
		{
			eventKey = GetHashCode();

			todoListNoteView = new ToDoListNoteView(this);
			todoListView = new ToDoListView(this);
			todoListHideView = new ToDoListHideView(this);
			todoListCategoryListView = new ToDoListCategoryListView(this);
			todoListFinishedView = new ToDoListFinishedView(this);
			todoListProductView = new ToDoListProductView(this);

			todoListToolBarView = new ToolBarView {style = "box"}.FontSize(15).Height(40);
			
			// AddView(todoListToolBarView);
			// todoListToolBarView
			// 	.AddMenu("笔记", () => ChangePage(todoListNoteView.eventKey))
			// 	.AddMenu("清单", () => ChangePage(todoListView.eventKey))
			// 	.AddMenu("隐藏", () => ChangePage(todoListHideView.eventKey))
			// 	.AddMenu("分类管理", () => ChangePage(todoListCategoryListView.eventKey))
			// 	.AddMenu("已完成", () => ChangePage(todoListFinishedView.eventKey))
			// 	.AddMenu("产品", () => ChangePage(todoListProductView.eventKey));
			// views.Add(todoListNoteView);
			// views.Add(todoListView);
			// views.Add(todoListHideView);
			// views.Add(todoListCategoryListView);
			// views.Add(todoListFinishedView);
			// views.Add(todoListProductView);
			
			AddMenu(todoListToolBarView,
				("笔记", todoListNoteView),
				("清单", todoListView),
				("隐藏", todoListHideView),
				("分类管理", todoListCategoryListView),
				("已完成", todoListFinishedView),
				("产品", todoListProductView)
			);


			todoListToolBarView.ForceClick(1);
		}

		public void ChangePage(int clickPage)
		{
			EventDispatcher.Send(eventKey, clickPage);
		}

		public void AddMenu(ToolBarView bar, params (string title, ToDoListPage page)[] tilePages)
		{
			AddView(bar);
			foreach (var kv in tilePages)
			{
				bar.AddMenu(kv.title, () => ChangePage(kv.page.eventKey));
				AddView(kv.page);
			}
		}

		protected override void OnUpdate()
		{
		}

		public void OnDisable()
		{
			ToDoDataManager.Save();
		}
	}
}