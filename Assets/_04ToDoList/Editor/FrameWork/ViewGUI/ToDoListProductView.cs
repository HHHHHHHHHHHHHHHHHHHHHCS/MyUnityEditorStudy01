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
		private ToDoListProductDetailView clickView;


		private ToDoProduct todoProduct;


		public ToDoListProductView(AbsViewController _ctrl) : base(_ctrl, "box")
		{
			var scrollLayout  = new ScrollLayout().AddTo(this);

			productViews = new VerticalLayout("box").AddTo(scrollLayout);
			detailViews = new VerticalLayout("box").AddTo(scrollLayout);

			// Rebuild();
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
			//防止删除的时候  editorwindow 还开着可以编辑
			CloseAllEditorWindow();
			
			if (productViews.Visible)
			{
				RebuildProductViews();
			}
			else
			{
				RebuildDetailViews(todoProduct);
			}
		}


		public void RebuildProductViews()
		{
			productViews.Clear();

			detailViews.Hide();
			todoProduct = null;

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

				foldout.AddFoldoutView(CreateDetailView(product, false).AddTo(detailViews));

				foldout.AddFoldoutView(new SpaceView(15f));
			}

			productViews.Show();
		}


		public void RebuildDetailViews(ToDoProduct todoProduct)
		{
			this.todoProduct = todoProduct;

			detailViews.Clear();

			productViews.Hide();

			CreateDetailView(todoProduct, true).AddTo(detailViews);
			detailViews.Show();
		}

		private ToDoListProductDetailView CreateDetailView(ToDoProduct todoProduct, bool isNewPage)
		{
			return new ToDoListProductDetailView(this, todoProduct, isNewPage, RebuildProductViews);
		}


		public void OpenProductEditorWindow(ToDoProduct todoProduct = null)
		{
			//防止同一帧数内 颜色污染
			EnqueueCmd(() =>
			{
				ToDoListEditorProductSubWindow.Open(this, todoProduct);
			});
		}

		public void RemoveProduct(ToDoProduct todoProduct)
		{
			ToDoDataManager.Data.RemoveProduct(todoProduct);
			EnqueueCmd(Rebuild);
		}

		protected override void OnShow()
		{
			RebuildProductViews();
		}

		protected override void OnHide()
		{
			CloseAllEditorWindow();
		}

		public void CloseAllEditorWindow()
		{
			if (ToDoListEditorProductSubWindow.instance)
			{
				ToDoListEditorProductSubWindow.instance.Close();
			}
			
			if (ToDoListVersionDetailSubWindow.instance)
			{
				ToDoListVersionDetailSubWindow.instance.Close();
			}

			if (ToDoListFeaturesSubWindow.instance)
			{
				ToDoListFeaturesSubWindow.instance.Close();
			}
			
		}
	}
}