using System;
using System.Linq;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewController;
using _04ToDoList.Editor.FrameWork.Window;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
	public class ToDoListProductView : ToDoListPage
	{
		private bool isDirty;

		private VerticalLayout productViews;
		private VerticalLayout detailViews;
		private VerticalLayout clickCreateBtnViews;


		private ToDoListVersionDetailSubWindow versionDetail;
		private ToDoListEditorProductSubWindow productSubWindow;

		private Product product;


		public ToDoListProductView(AbsViewController _ctrl) : base(_ctrl, "box")
		{
			productViews = new VerticalLayout("box").AddTo(this);
			detailViews = new VerticalLayout("box").AddTo(this);


			Rebuild();
		}

		protected override void OnRefresh()
		{
			if (isDirty)
			{
				isDirty = false;
				Rebuild();
			}
		}

		public void UpdateToDoItems()
		{
			isDirty = true;

			//如果不focus 则会不刷新
			ToDoListMainWindow.instance.Focus();
		}


		public void Rebuild()
		{
			if (productViews.Visible)
			{
				RebuildProductViews();
			}
			else
			{
				RebuildDetailViews(product);
			}
		}

		public void CreateAndInsertProductVersion(Product _product, ToDoProductVersion version)
		{
			int index = -1;
			foreach (var item in _product.versions.OrderByDescending(x =>
				x.version))
			{
				++index;
				if (item == version)
				{
					new ToDoProductFoldoutDetailView(this, _product, item, clickCreateBtnViews, index);
				}
			}
		}

		public void RebuildProductViews()
		{
			productViews.Clear();

			productViews.Show();
			detailViews.Hide();
			product = null;


			//new LabelView("这个是产品页面").AddTo(productViews).TextMiddleCenter().FontBold().FontSize(30);

			new ButtonView("创建产品", () => OpenProductEditorWindow(null), true).Height(40).AddTo(productViews);

			new SpaceView(10f).AddTo(productViews);

			var data = ToDoDataManager.Data.productList;
			for (int i = data.Count - 1; i >= 0; i--)
			{
				var product = data[i];

				var title = new HorizontalLayout();

				new FlexibleSpaceView().AddTo(title);

				new ImageButtonView(ImageButtonIcon.openIcon, () => EnqueueCmd(() => RebuildDetailViews(product)))
					.Width(40).Height(25).BackgroundColor(Color.black).AddTo(title);

				new ImageButtonView(ImageButtonIcon.editorIcon, () => OpenProductEditorWindow(product))
					.Width(40).Height(25).BackgroundColor(Color.black).AddTo(title);

				new ImageButtonView(ImageButtonIcon.deleteIcon, () => RemoveProduct(product)).Width(25).Height(25)
					.BackgroundColor(Color.red).AddTo(title);


				var foldout = new FoldoutView(false, product.name, title).FontSize(25)
					.FontBold().AddTo(productViews);

				new SpaceView(10f).AddTo(productViews);

				var ver = new VerticalLayout("box");
				CreateDetailView(product, ver, false);
				foldout.AddFoldoutView(ver);

				foldout.AddFoldoutView(new SpaceView(20f));
			}
		}


		public void RebuildDetailViews(Product _product)
		{
			product = _product;

			detailViews.Clear();

			productViews.Hide();
			detailViews.Show();

			CreateDetailView(_product, detailViews, true);
		}

		private void CreateDetailView(Product _product, VerticalLayout views, bool isNewPage)
		{
			var detailLayout = new VerticalLayout("box");

			int createBtnFontSize = 15;
			if (isNewPage)
			{
				new LabelView(_product.name).FontBold().FontSize(30).TextMiddleCenter().AddTo(views);
				new SpaceView(8).AddTo(views);
				new ButtonView("返回", () => EnqueueCmd(RebuildProductViews), true).FontSize(20).TextMiddleCenter()
					.AddTo(views);
				createBtnFontSize = 25;
			}
			
			//TODO:NEW LABEL FOR 描述
			//TODO:ADD feature 写成单独的view

			var functionFoldout = new FoldoutView(false, "描述: "+_product.description+"	功能:").FontSize(25)
				.FontBold().MarginLeft(15);
			
			functionFoldout.AddTo(views);

			
			
			var createBtn = new ButtonView("创建版本", () => { OpenVersionDetailWindow(_product, detailLayout); }, true)
				.FontBold().Width(120)
				.FontSize(createBtnFontSize).TextMiddleCenter();
			var versionFoldout = new FoldoutView(false, "版本: ", createBtn).FontSize(25)
				.FontBold().MarginLeft(15);


			versionFoldout.AddFoldoutView(detailLayout);
			if (isNewPage)
			{
				versionFoldout.Visible = true;
			}

			versionFoldout.AddTo(views);


			foreach (var item in _product.versions.OrderByDescending(x =>
				x.version))
			{
				new ToDoProductFoldoutDetailView(this, _product, item, detailLayout);
			}
		}

		public void RefreshProductFoldoutDetailView(Product _product, VerticalLayout _detailLayout)
		{
			_detailLayout.Clear();

			foreach (var item in _product.versions.OrderByDescending(x =>
				x.version))
			{
				new ToDoProductFoldoutDetailView(this, _product, item, _detailLayout);
			}
		}

		public void OpenVersionDetailWindow(Product _product, VerticalLayout _views)
		{
			EnqueueCmd(() =>
			{
				clickCreateBtnViews = _views;
				versionDetail = ToDoListVersionDetailSubWindow.Open(this, _product, _views);
				versionDetail.Show();
			});
		}

		public void OpenProductEditorWindow(Product _product = null)
		{
			EnqueueCmd(() =>
			{
				productSubWindow = ToDoListEditorProductSubWindow.Open(this, _product);
				productSubWindow.Show();
			});
		}

		public void RemoveProduct(Product _product)
		{
			ToDoDataManager.Data.RemoveProduct(_product);
			EnqueueCmd(Rebuild);
		}

		protected override void OnShow()
		{
			RebuildProductViews();
		}

		protected override void OnHide()
		{
			if (versionDetail != null)
			{
				versionDetail.Close();
				versionDetail = null;
			}

			if (productSubWindow != null)
			{
				productSubWindow.Close();
				productSubWindow = null;
			}
		}
	}
}