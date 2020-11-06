using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.ViewController;
using _04ToDoList.Editor.FrameWork.Window;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
	public class ToDoListProductView : ToDoListPage
	{
		public ToDoListProductView(AbsViewController _ctrl) : base(_ctrl, "box")
		{
			new ButtonView("创建产品", OpenProductEditorWindow, true).AddTo(this);
			new LabelView("这个是产品页面").AddTo(this).TheFontStyle(FontStyle.Bold).FontSize(30);
		}

		public void OpenProductEditorWindow()
		{
			ToDoListEditorProductSubWindow.Open(this);
		}
	}
}