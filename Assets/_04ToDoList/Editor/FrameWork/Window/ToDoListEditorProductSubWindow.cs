using System;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewGUI;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Window
{
	public class ToDoListEditorProductSubWindow : SubWindow
	{
		public static ToDoListEditorProductSubWindow instance { get; private set; }
		
		private ToDoProduct todoProduct;

		public static ToDoListEditorProductSubWindow Open(ToDoListProductView _productView, ToDoProduct todoProduct = null,
			string name = "Product Editor")
		{
			var window = Open<ToDoListEditorProductSubWindow>(name).Init(_productView, todoProduct);
			instance = window;
			window.Show();
			return window;
		}

		private ToDoListProductView productView;


		private ToDoListEditorProductSubWindow Init(ToDoListProductView _productView, ToDoProduct todoProduct)
		{
			productView = _productView;
			this.todoProduct = todoProduct;

			Clear();

			var verticalLayout = new VerticalLayout("box").AddTo(this);

			var productName = this.todoProduct == null ? string.Empty : this.todoProduct.name;
			var productDescription = this.todoProduct == null ? string.Empty : this.todoProduct.description;

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

		private void OnDisable()
		{
			instance = null;
		}
		
		private void SaveProduct(string productName, string productDescription)
		{
			if (todoProduct == null)
			{
				ToDoDataManager.AddProduct(productName, productDescription);
			}
			else
			{
				todoProduct.name = productName;
				todoProduct.description = productDescription;
				ToDoDataManager.Save();
			}

			productView.Rebuild();
			Close();
		}


		
	}
}