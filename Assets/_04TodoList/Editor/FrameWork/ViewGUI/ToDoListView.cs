using System.Linq;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewController;
using _04ToDoList.Util;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
	public class ToDoListView : ToDoListPage
	{
		private ToolBarView todoListToolBarView;


		private ToDoListToDoView todoListToDoView;
		private ToDoListHideView todoListHideView;
		private ToDoListFinishedView todoListFinishedView;

		public ToDoListView(AbsViewController ctrl) : base(ctrl)
		{
			todoListToDoView = new ToDoListToDoView(eventKey);
			todoListHideView = new ToDoListHideView(eventKey);
			todoListFinishedView = new ToDoListFinishedView(eventKey);


			todoListToolBarView = new ToolBarView {style = "box"}.FontSize(15).Height(40).AddTo(this);


			AddMenu(todoListToolBarView,
				("进行", todoListToDoView),
				("隐藏", todoListHideView),
				("完成", todoListFinishedView));
		}


		protected override void OnShow()
		{
			base.OnShow();
			todoListToolBarView.ForceClick(0);
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
				kv.page.AddTo(this);
			}
		}

		protected override void OnDisposed()
		{
			base.OnDisposed();
			todoListToDoView.Dispose();
			todoListHideView.Dispose();
			todoListFinishedView.Dispose();
			EventDispatcher.RemoveAll(eventKey);
		}
	}
}