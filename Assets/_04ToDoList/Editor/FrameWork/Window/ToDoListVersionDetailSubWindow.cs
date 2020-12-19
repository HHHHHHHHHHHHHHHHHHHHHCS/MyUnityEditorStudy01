using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewGUI;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Window
{
	public class ToDoListVersionDetailSubWindow : SubWindow
	{
		private ToDoListProductView productView;
		private Product product;
		private ToDoVersion todoVersion;
		private ToDoProductVersion productVersion;
		private string productName;

		private LabelView majorView, middleView, smallView;
		private TextFieldView versionNameView;


		public static ToDoListVersionDetailSubWindow Open(ToDoListProductView productView, Product product,
			ToDoProductVersion version = null, string name = "Version Create")
		{
			var window = Open<ToDoListVersionDetailSubWindow>(name).Init(productView, product, version);
			return window;
		}


		private void Awake()
		{
			todoVersion = new ToDoVersion(0, 0, 0);

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
			versionNameView = new TextFieldView(productName, str => productName = str).FontSize(20).AddTo(nameHor);

			new SpaceView().AddTo(verticalLayout);

			new ButtonView("保存", SaveProduct, true).AddTo(verticalLayout);
		}

		private ToDoListVersionDetailSubWindow Init(ToDoListProductView _productView, Product _product,
			ToDoProductVersion _version = null)
		{
			productView = _productView;
			product = _product;
			productVersion = _version;

			if (_version != null)
			{
				todoVersion.SetVersion(_version.version);
				versionNameView.Content.Val = _version.name;
			}
			else
			{
				todoVersion.SetVersion(0, 0, 0);
				productName = string.Empty;
			}


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
					++todoVersion.Major;
				}
				else
				{
					--todoVersion.Major;
				}
			}

			majorView.SetText(todoVersion.Major.ToString());
		}

		public void UpdateMiddle(bool? isAdd = null)
		{
			if (isAdd.HasValue)
			{
				if (isAdd.Value)
				{
					++todoVersion.Middle;
				}
				else
				{
					--todoVersion.Middle;
				}
			}

			middleView.SetText(todoVersion.Middle.ToString());
		}

		public void UpdateSmall(bool? isAdd = null)
		{
			if (isAdd.HasValue)
			{
				if (isAdd.Value)
				{
					++todoVersion.Small;
				}
				else
				{
					--todoVersion.Small;
				}
			}

			smallView.SetText(todoVersion.Small.ToString());
		}

		public void SaveProduct()
		{
			if (productVersion != null)
			{
				productVersion.name = productName;
				productVersion.version.SetVersion(todoVersion);;
				productView.CreateAndInsertProductVersion(product, productVersion);
			}
			else
			{
				var version = new ToDoProductVersion()
				{
					name = productName,
					version = todoVersion
				};
				product.versions.Add(version);
				productView.CreateAndInsertProductVersion(product, version);
			}

			ToDoDataManager.Save();
			Close();
			ToDoListMainWindow.instance.Focus();
		}
	}
}