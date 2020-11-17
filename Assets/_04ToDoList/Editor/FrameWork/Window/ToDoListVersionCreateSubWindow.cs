using System;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewGUI;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Window
{
	public class ToDoListVersionCreateSubWindow : SubWindow
	{
		private ToDoListProductView productView;
		private Product product;
		private ToDoVersion version;


		private LabelView majorView, middleView, smallView;


		public static ToDoListVersionCreateSubWindow Open(ToDoListProductView productView, Product product,
			string name = "Version Create")
		{
			var window = Open<ToDoListVersionCreateSubWindow>(name).Init(productView, product);
			return window;
		}


		private void Awake()
		{
			version = new ToDoVersion(0, 0, 0);

			var verticalLayout = new VerticalLayout("box").AddTo(this);

			var addHor = new HorizontalLayout().AddTo(verticalLayout);
			new SpaceView(98).AddTo(addHor);
			new ButtonView("+", () => UpdateMajor(true)).Height(20).Width(20).FontSize(10).AddTo(addHor);
			new SpaceView(15).AddTo(addHor);
			new ButtonView("+", () => UpdateMiddle(true)).Height(20).Width(20).FontSize(10).AddTo(addHor);
			new SpaceView(15).AddTo(addHor);
			new ButtonView("+", () => UpdateSmall(true)).Height(20).Width(20).FontSize(10).AddTo(addHor);

			var versionHor = new HorizontalLayout().AddTo(verticalLayout);
			
			new LabelView("版本号:").Width(80).TheFontStyle(FontStyle.Bold).FontSize(20).AddTo(versionHor);
			new LabelView("V").FontSize(10).Height(20).Width(11).TheFontStyle(FontStyle.Bold).AddTo(versionHor);
			majorView = new LabelView("0").FontSize(12).Height(20).Width(20).TheFontStyle(FontStyle.Bold)
				.AddTo(versionHor);
			new LabelView(".").FontSize(10).Height(20).Width(11).TheFontStyle(FontStyle.Bold).AddTo(versionHor);
			middleView = new LabelView("0").FontSize(12).Height(20).Width(20).TheFontStyle(FontStyle.Bold)
				.AddTo(versionHor);
			new LabelView(".").FontSize(10).Height(20).Width(11).TheFontStyle(FontStyle.Bold).AddTo(versionHor);
			smallView = new LabelView("0").FontSize(12).Height(20).Width(20).TheFontStyle(FontStyle.Bold)
				.AddTo(versionHor);

			var reduceHor = new HorizontalLayout().AddTo(verticalLayout);
			new SpaceView(98).AddTo(reduceHor);
			new ButtonView("-", () => UpdateMajor(false)).TextMiddleCenter().Height(20).Width(20).FontSize(10)
				.AddTo(reduceHor);
			new SpaceView(15).AddTo(reduceHor);
			new ButtonView("-", () => UpdateMiddle(false)).TextMiddleCenter().Height(20).Width(20).FontSize(10)
				.AddTo(reduceHor);
			new SpaceView(15).AddTo(reduceHor);
			new ButtonView("-", () => UpdateSmall(false)).TextMiddleCenter().Height(20).Width(20).FontSize(10)
				.AddTo(reduceHor);


			new LabelView("版本名:").TheFontStyle(FontStyle.Bold).FontSize(20).AddTo(verticalLayout);
		}

		private ToDoListVersionCreateSubWindow Init(ToDoListProductView _productView, Product _product)
		{
			productView = _productView;
			product = _product;

			version.SetVersion(0, 0, 0);

			UpdateMajor();
			UpdateMiddle();
			UpdateSmall();

			return this;
		}

		public void UpdateMajor(bool? isAdd = null)
		{
			if (isAdd.HasValue)
			{
				if (isAdd.Value)
				{
					++version.Major;
				}
				else
				{
					--version.Major;
				}
			}

			majorView.SetText(version.Major.ToString());
		}

		public void UpdateMiddle(bool? isAdd = null)
		{
			if (isAdd.HasValue)
			{
				if (isAdd.Value)
				{
					++version.Middle;
				}
				else
				{
					--version.Middle;
				}
			}

			middleView.SetText(version.Middle.ToString());
		}

		public void UpdateSmall(bool? isAdd = null)
		{
			if (isAdd.HasValue)
			{
				if (isAdd.Value)
				{
					++version.Small;
				}
				else
				{
					--version.Small;
				}
			}

			smallView.SetText(version.Small.ToString());
		}
	}
}