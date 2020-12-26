using System;
using System.Linq;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.Window;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
	public class ToDoListProductDetailView : VerticalLayout
	{
		public readonly VerticalLayout featureDetailView;
		public readonly VerticalLayout versionDetailView;

		private ToDoListProductView productView;
		private ToDoProduct todoProduct;

		public ToDoListProductDetailView(ToDoListProductView _productView, ToDoProduct _todoProduct, bool isNewPage
			, Action _rebuildProductViews)
			: base("box")
		{
			productView = _productView;
			todoProduct = _todoProduct;

			if (isNewPage)
			{
				new LabelView(_todoProduct.name).FontBold().FontSize(30).TextMiddleCenter().AddTo(this);
				new SpaceView(8).AddTo(this);
				new ButtonView("返回", () => EnqueueCmd(_rebuildProductViews), true).FontSize(20).TextMiddleCenter()
					.AddTo(this);
			}

			new SpaceView(8).AddTo(this);

			new LabelView("描述: " + _todoProduct.description).FontBold().FontSize(25).TextMiddleLeft().MarginLeft(30)
				.AddTo(this);

			//Feature Detail-------------------

			new SpaceView(8).AddTo(this);

			 featureDetailView = new VerticalLayout("box");
			
			var createFeatureBtn = new ButtonView("添加功能", () =>
				{
					var newFeature = new ToDoFeature("AA", "BB");
					todoProduct.features.Add(newFeature);
					ToDoDataManager.Save();
					new ToDoListFeatureView(newFeature).AddTo(featureDetailView);
				}, true)
				.Height(20).Width(100).FontSize(15).TextMiddleCenter();
			
			var featureFoldout = new FoldoutView(false, "功能:", createFeatureBtn)
				.FontSize(25)
				.FontBold().MarginLeft(15).AddTo(this);
			
			featureFoldout.AddFoldoutView(featureDetailView); 

			foreach (var feature in todoProduct.features)
			{
				new ToDoListFeatureView(feature).AddTo(featureDetailView);
			}


			//Version Detail-----------
			new SpaceView(8).AddTo(this);

			versionDetailView = new VerticalLayout("box");
			var createVersionBtn =
				new ButtonView("创建版本", OpenVersionDetailWindow, true)
					.Height(20).Width(100).FontSize(15).TextMiddleCenter();
			var versionFoldout = new FoldoutView(false, "版本:", createVersionBtn)
				.FontSize(25).FontBold().MarginLeft(15).AddTo(this);

			versionFoldout.AddFoldoutView(versionDetailView);

			RefreshProductFoldoutDetailView();
		}


		public void OpenVersionDetailWindow()
		{
			//防止同一帧数内 颜色污染
			EnqueueCmd(() =>
			{
				ToDoListVersionDetailSubWindow.Open(this, todoProduct);
			});
		}


		public void RefreshProductFoldoutDetailView()
		{
			versionDetailView.Clear();

			foreach (var item in todoProduct.versions.OrderByDescending(x =>
				x.version))
			{
				new ToDoListProductFoldoutDetailView(this, todoProduct, item).AddTo(versionDetailView);
			}
		}

		public void CreateAndInsertProductVersion(ToDoProductVersion version)
		{
			int index = -1;
			foreach (var item in todoProduct.versions.OrderByDescending(x =>
				x.version))
			{
				++index;
				if (item == version)
				{
					new ToDoListProductFoldoutDetailView(this, todoProduct, version).InsertTo(index, versionDetailView);
				}
			}
		}
	}
}