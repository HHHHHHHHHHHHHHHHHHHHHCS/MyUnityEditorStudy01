using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewGUI;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Window
{
	public class ToDoListVersionCreateSubWindow : SubWindow
	{
		public ToDoListProductView productView;
		public Product product;


		public static ToDoListVersionCreateSubWindow Open(ToDoListProductView productView, Product product,
			string name = "Version Create")
		{
			var window = Open<ToDoListVersionCreateSubWindow>(name).Init(productView, product);
			return window;
		}

		private ToDoListVersionCreateSubWindow Init(ToDoListProductView _productView, Product _product)
		{
			productView = _productView;
			product = _product;

			Clear();

			var verticalLayout = new VerticalLayout("box").AddTo(this);

			new LabelView("版本号:").TheFontStyle(FontStyle.Bold).FontSize(20).AddTo(verticalLayout);

			new CustomView(() =>
			{
				GUILayout.BeginHorizontal();
				GUILayout.Label("v", GUILayout.Width(10));
				GUILayout.TextField("0", 2, GUILayout.Width(15));
				GUILayout.Label(".", GUILayout.Width(7));
				GUILayout.TextField("0", 2, GUILayout.Width(15));
				GUILayout.Label(".", GUILayout.Width(7));
				GUILayout.TextField("0", 2, GUILayout.Width(15));
				GUILayout.EndHorizontal();
			}).AddTo(verticalLayout);

			new LabelView("版本名:").TheFontStyle(FontStyle.Bold).FontSize(20).AddTo(verticalLayout);


			return this;
		}
	}
}