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
		private ToDoVersion productVersion;
		private string productName;

		private LabelView majorView, middleView, smallView;


		public static ToDoListVersionCreateSubWindow Open(ToDoListProductView productView, Product product,
			string name = "Version Create")
		{
			var window = Open<ToDoListVersionCreateSubWindow>(name).Init(productView, product);
			return window;
		}


		private void Awake()
		{
			productVersion = new ToDoVersion(0, 0, 0);

			var verticalLayout = new VerticalLayout("box").AddTo(this);

			var addHor = new HorizontalLayout().AddTo(verticalLayout);
			new SpaceView(98).AddTo(addHor);
			new ButtonView("+", () => UpdateMajor(true)).Height(20).Width(20).FontSize(10).AddTo(addHor);
			new SpaceView(15).AddTo(addHor);
			new ButtonView("+", () => UpdateMiddle(true)).Height(20).Width(20).FontSize(10).AddTo(addHor);
			new SpaceView(15).AddTo(addHor);
			new ButtonView("+", () => UpdateSmall(true)).Height(20).Width(20).FontSize(10).AddTo(addHor);

			var versionHor = new HorizontalLayout().AddTo(verticalLayout);

			new LabelView("版本号:").Width(80).FontBold().FontSize(20).AddTo(versionHor);
			new LabelView("V").FontSize(10).Height(20).Width(11).FontBold().AddTo(versionHor);
			majorView = new LabelView("0").FontSize(20).Height(20).Width(20).FontBold()
				.AddTo(versionHor);
			new LabelView(".").FontSize(20).Height(20).Width(11).FontBold().AddTo(versionHor);
			middleView = new LabelView("0").FontSize(20).Height(20).Width(20).FontBold()
				.AddTo(versionHor);
			new LabelView(".").FontSize(20).Height(20).Width(11).FontBold().AddTo(versionHor);
			smallView = new LabelView("0").FontSize(20).Height(20).Width(20).FontBold()
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

			new SpaceView().AddTo(verticalLayout);

			var nameHor = new HorizontalLayout().AddTo(verticalLayout);

			new LabelView("版本名:").FontBold().Width(80).FontSize(20).AddTo(nameHor);
			new TextFieldView(productName, str => productName = str).FontSize(20).AddTo(nameHor);

			new SpaceView().AddTo(verticalLayout);

			new ButtonView("保存", SaveProduct, true).AddTo(verticalLayout);
		}

		private ToDoListVersionCreateSubWindow Init(ToDoListProductView _productView, Product _product)
		{
			productView = _productView;
			product = _product;

			productVersion.SetVersion(0, 0, 0);
			productName = string.Empty;

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
					++productVersion.Major;
				}
				else
				{
					--productVersion.Major;
				}
			}

			majorView.SetText(productVersion.Major.ToString());
		}

		public void UpdateMiddle(bool? isAdd = null)
		{
			if (isAdd.HasValue)
			{
				if (isAdd.Value)
				{
					++productVersion.Middle;
				}
				else
				{
					--productVersion.Middle;
				}
			}

			middleView.SetText(productVersion.Middle.ToString());
		}

		public void UpdateSmall(bool? isAdd = null)
		{
			if (isAdd.HasValue)
			{
				if (isAdd.Value)
				{
					++productVersion.Small;
				}
				else
				{
					--productVersion.Small;
				}
			}

			smallView.SetText(productVersion.Small.ToString());
		}

		public void SaveProduct()
		{
			var version = new ToDoProductVersion()
			{
				name = productName,
				version = productVersion
			};
			product.versions.Add(version);

			ToDoDataManager.Save();

			Close();

			ToDoListMainWindow.instance.Focus();
			productView.CreateAndInsert(product,version);
		}
	}
}