using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
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

			foreach (var product in ToDoDataManager.Data.productList)
			{
				var hor =  new HorizontalLayout().AddTo(this);
				
				new LabelView(product.name).FontSize(20).TheFontStyle(FontStyle.Bold).Height(20).Width(60).AddTo(hor);
				
				new LabelView(product.description).FontSize(12).TheFontStyle(FontStyle.Bold).Height(20).AddTo(hor);
			}
		}

		public void OpenProductEditorWindow()
		{
			ToDoListEditorProductSubWindow.Open(this);
		}
	}
}