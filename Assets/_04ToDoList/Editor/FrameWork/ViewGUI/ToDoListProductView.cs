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


		private ToDoListVersionCreateSubWindow versionCreate;
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

		public void CreateAndInsert(Product product, ToDoProductVersion version)
		{
			int index = -1;
			foreach (var item in product.versions.OrderByDescending(x =>
				x.version))
			{
				++index;
				if (item == version)
				{
					AddFoldoutToDetailView(item, clickCreateBtnViews, index);
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

				new LabelView("描述: " + product.description)
					.FontSize(15).FontBold().PaddingLeft(80).Height(20).TextMiddleLeft().AddTo(title);

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
			var detailViews = new VerticalLayout("box");

			if (isNewPage)
			{
				new LabelView(_product.name).FontBold().FontSize(30).TextMiddleCenter().AddTo(views);
				new SpaceView(5).AddTo(views);
				new LabelView(_product.description).FontSize(20).TextMiddleCenter().AddTo(views);
				new ButtonView("创建版本", () => { OpenProductDetailWindow(_product, detailViews); }, true).FontBold()
					.FontSize(25).TextMiddleCenter().AddTo(views);
				new SpaceView(5).AddTo(views);
				new ButtonView("返回", () => EnqueueCmd(RebuildProductViews), true).FontSize(20).TextMiddleCenter()
					.AddTo(views);
			}
			else
			{
				//new LabelView(_product.description).FontSize(20).TextMiddleCenter().AddTo(views);
				new ButtonView("创建版本", () => { OpenProductDetailWindow(_product, detailViews); }, true).FontBold()
					.FontSize(15).TextMiddleCenter().AddTo(views);
			}
			
			detailViews.AddTo(views);


			foreach (var item in _product.versions.OrderByDescending(x =>
				x.version))
			{
				AddFoldoutToDetailView(item, detailViews);
			}
		}

		public void AddFoldoutToDetailView(ToDoProductVersion version, VerticalLayout views, int insertIndex = -1)
		{
			var fold = new FoldoutView(false, version.name + "	" + version.version).FontBold().FontSize(15)
				.TextMiddleLeft();

			if (insertIndex < 0)
			{
				fold.AddTo(views);
			}
			else
			{
				fold.InsertTo(insertIndex, views);
			}

			var input = new ToDoListInputView((cate, name) => AddFoldoutItem(fold, version, cate, name));
			input.Show();

			fold.AddFoldoutView(input);
			foreach (var todoItem in version.todos)
			{
				AddTodoToFoldout(fold, version, todoItem);
			}
		}


		public void OpenProductDetailWindow(Product _product, VerticalLayout views)
		{
			EnqueueCmd(() =>
			{
				clickCreateBtnViews = views;
				versionCreate = ToDoListVersionCreateSubWindow.Open(this, _product);
				versionCreate.Show();
			});
		}

		public void OpenProductEditorWindow(Product product = null)
		{
			EnqueueCmd(() =>
			{
				productSubWindow = ToDoListEditorProductSubWindow.Open(this, product);
				productSubWindow.Show();
			});
		}

		public void RemoveProduct(Product product)
		{
			ToDoDataManager.Data.RemoveProduct(product);
			EnqueueCmd(Rebuild);
		}


		protected override void OnShow()
		{
			RebuildProductViews();
		}

		protected override void OnHide()
		{
			if (versionCreate != null)
			{
				versionCreate.Close();
				versionCreate = null;
			}

			if (productSubWindow != null)
			{
				productSubWindow.Close();
				productSubWindow = null;
			}
		}

		private void AddTodoToFoldout(FoldoutView foldoutView, ToDoProductVersion productVersion, ToDoData todoItem)
		{
			var itemToDoView = new ToDoListItemView(todoItem, (_) => { });

			itemToDoView.deleteAct = (_) =>
			{
				EnqueueCmd(() =>
				{
					productVersion.todos.Remove(todoItem);
					ToDoDataManager.Data.Save();
					foldoutView.RemoveFoldoutView(itemToDoView);
				});
			};

			foldoutView.AddFoldoutView(itemToDoView);
		}

		private void AddFoldoutItem(FoldoutView foldoutView, ToDoProductVersion version, ToDoCategory category,
			string todoName)
		{
			EnqueueCmd(() =>
			{
				var item = ToDoDataManager.AddToDoItem(todoName, false, category);
				version.todos.Add(item);
				ToDoDataManager.Data.Save();

				AddTodoToFoldout(foldoutView, version, item);
			});
		}
	}
}