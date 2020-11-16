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

			var addHor = new HorizontalLayout().AddTo(verticalLayout);
			new SpaceView(18).AddTo(addHor);
			new ButtonView("+", () => { }).Height(20).Width(20).FontSize(10).AddTo(addHor);
			new SpaceView(14).AddTo(addHor);
			new ButtonView("+", () => { }).Height(20).Width(20).FontSize(10).AddTo(addHor);
			new SpaceView(14).AddTo(addHor);
			new ButtonView("+", () => { }).Height(20).Width(20).FontSize(10).AddTo(addHor);

			var versionHor = new HorizontalLayout().AddTo(verticalLayout);
			new LabelView("V").FontSize(10).Height(20).Width(11).TheFontStyle(FontStyle.Bold).AddTo(versionHor);
			new LabelView("0").FontSize(12).Height(20).Width(20).TheFontStyle(FontStyle.Bold).AddTo(versionHor);
			new LabelView(".").FontSize(10).Height(20).Width(11).TheFontStyle(FontStyle.Bold).AddTo(versionHor);
			new LabelView("0").FontSize(12).Height(20).Width(20).TheFontStyle(FontStyle.Bold).AddTo(versionHor);
			new LabelView(".").FontSize(10).Height(20).Width(11).TheFontStyle(FontStyle.Bold).AddTo(versionHor);
			new LabelView("0").FontSize(12).Height(20).Width(20).TheFontStyle(FontStyle.Bold).AddTo(versionHor);

			var reduceHor = new HorizontalLayout().AddTo(verticalLayout);
			new SpaceView(18).AddTo(reduceHor);
			new ButtonView("-", () => { }).TextMiddleCenter().Height(20).Width(20).FontSize(10).AddTo(reduceHor);
			new SpaceView(14).AddTo(reduceHor);
			new ButtonView("-", () => { }).TextMiddleCenter().Height(20).Width(20).FontSize(10).AddTo(reduceHor);
			new SpaceView(14).AddTo(reduceHor);
			new ButtonView("-", () => { }).TextMiddleCenter().Height(20).Width(20).FontSize(10).AddTo(reduceHor);
			

			new LabelView("版本名:").TheFontStyle(FontStyle.Bold).FontSize(20).AddTo(verticalLayout);


			return this;
		}
	}
}