using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewGUI;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Window
{
	public class ToDoListEditorProductSubWindow : SubWindow
	{
		public static ToDoListEditorProductSubWindow Open(ToDoListProductView productView,
			string name = " Product Editor")
		{
			var window = Open<ToDoListEditorProductSubWindow>(name);
			window.productView = productView;
			return window;
		}

		private ToDoListProductView productView;

		private void Awake()
		{
			var verticalLayout = new VerticalLayout("box").AddTo(this);

			var productName = string.Empty;
			var productDescription = string.Empty;

			new LabelView("名称:").TextMiddleCenter().TheFontStyle(FontStyle.Bold).FontSize(20).AddTo(verticalLayout);
			new TextAreaView(string.Empty, pName => productName = pName).Height(30).AddTo(verticalLayout);
			new LabelView("描述:").TextMiddleCenter().TheFontStyle(FontStyle.Bold).FontSize(20).AddTo(verticalLayout);
			new TextAreaView(string.Empty, pDesc => productDescription = pDesc).Height(60).AddTo(verticalLayout);
			new ButtonView("保存", () => { AddProduct(productName, productDescription); }, true).Height(20)
				.AddTo(verticalLayout);
		}

		private void AddProduct(string productName, string productDescription)
		{
			ToDoDataManager.AddProduct(productName, productDescription);
			productView.Rebuild();
			Close();
		}
	}
}