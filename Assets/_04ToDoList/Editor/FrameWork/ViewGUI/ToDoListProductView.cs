using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.ViewController;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
	public class ToDoListProductView : ToDoListPage
	{
		public ToDoListProductView(AbsViewController _ctrl) : base(_ctrl, "box")
		{
			new LabelView("这个是产品页面").AddTo(this).TheFontStyle(FontStyle.Bold).FontSize(30);
		}
	}
}
