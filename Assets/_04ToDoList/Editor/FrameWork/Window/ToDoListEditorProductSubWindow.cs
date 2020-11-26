using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewGUI;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Window
{
	public class ToDoListEditorProductSubWindow : SubWindow
	{
		private Product product;

		public static ToDoListEditorProductSubWindow Open(ToDoListProductView _productView, Product _product = null,
			string name = "Product Editor")
		{
			var window = Open<ToDoListEditorProductSubWindow>(name).Init(_productView, _product);

			return window;
		}

		private ToDoListProductView productView;


		private ToDoListEditorProductSubWindow Init(ToDoListProductView _productView, Product _product)
		{
			productView = _productView;
			product = _product;

			Clear();

			var verticalLayout = new VerticalLayout("box").AddTo(this);

			var productName = product == null ? string.Empty : product.name;
			var productDescription = product == null ? string.Empty : product.description;

			new LabelView("名称:").TextMiddleCenter().FontBold().FontSize(20).AddTo(verticalLayout);
			new TextAreaView(productName, pName => productName = pName).Height(30)
				.AddTo(verticalLayout);
			new LabelView("描述:").TextMiddleCenter().FontBold().FontSize(20).AddTo(verticalLayout);
			new TextAreaView(productDescription, pDesc => productDescription = pDesc)
				.Height(60).AddTo(verticalLayout);
			new ButtonView("保存", () => { SaveProduct(productName, productDescription); }, true).Height(20)
				.AddTo(verticalLayout);

			return this;
		}

		private void SaveProduct(string productName, string productDescription)
		{
			if (product == null)
			{
				ToDoDataManager.AddProduct(productName, productDescription);
			}
			else
			{
				product.name = productName;
				product.description = productDescription;
				ToDoDataManager.Save();
			}

			productView.Rebuild();
			Close();
		}
	}
}