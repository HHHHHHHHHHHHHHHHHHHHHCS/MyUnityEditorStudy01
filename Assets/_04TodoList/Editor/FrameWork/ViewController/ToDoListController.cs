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
		private ToDoListCategoryListView todoListCategoryListView;
		private ToDoListProductView todoListProductView;


		protected override void SetUpView()
		{
			eventKey = GetHashCode();

			todoListNoteView = new ToDoListNoteView(this);
			todoListView = new ToDoListView(this);
			todoListCategoryListView = new ToDoListCategoryListView(this);
			todoListProductView = new ToDoListProductView(this);

			todoListToolBarView = new ToolBarView {style = "box"}.FontSize(15).Height(40);
			AddView(todoListToolBarView);

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
				("分类管理", todoListCategoryListView),
				("产品", todoListProductView)
			);


			todoListToolBarView.ForceClick(1);
		}

		public void ChangeMenuPage(int clickPage)
		{
			EventDispatcher.Send(eventKey, clickPage);
		}

		public void AddMenu(ToolBarView bar, params (string title, ToDoListPage page)[] tilePages)
		{
			foreach (var kv in tilePages)
			{
				bar.AddMenu(kv.title, () => ChangeMenuPage(kv.page.eventKey));
				AddView(kv.page);
			}
		}

		protected override void OnUpdate()
		{
		}

		public void OnDisable()
		{
			todoListNoteView.Dispose();
			todoListView.Dispose();
			todoListCategoryListView.Dispose();
			todoListProductView.Dispose();
			EventDispatcher.RemoveAll(eventKey);
			ToDoDataManager.Save();
		}
	}
}