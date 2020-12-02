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

		public void RebuildProductViews()
		{
			productViews.Clear();

			productViews.Show();
			detailViews.Hide();
			product = null;


			//new LabelView("这个是产品页面").AddTo(productViews).TextMiddleCenter().FontBold().FontSize(30);

			new ButtonView("创建产品", () => OpenProductEditorWindow(null), true).Height(40).AddTo(productViews);

			var data = ToDoDataManager.Data.productList;
			for (int i = data.Count - 1; i >= 0; i--)
			{
				var product = data[i];

				var hor = new HorizontalLayout().AddTo(productViews);

				new ImageButtonView(ImageButtonIcon.openIcon, () => EnqueueCmd(() => RebuildDetailViews(product)))
					.Width(40).Height(25).BackgroundColor(Color.black).AddTo(hor);

				new LabelView(product.name).FontSize(20).FontBold().Height(20).Width(80).AddTo(hor);

				new LabelView(product.description).FontSize(12).FontBold().Height(20).AddTo(hor);

				new ImageButtonView(ImageButtonIcon.editorIcon, () => OpenProductEditorWindow(product))
					.Width(40).Height(25).BackgroundColor(Color.black).AddTo(hor);

				new ImageButtonView(ImageButtonIcon.deleteIcon, () => RemoveProduct(product)).Width(25).Height(25)
					.BackgroundColor(Color.red).AddTo(hor);
			}
		}


		public void RebuildDetailViews(Product _product)
		{
			product = _product;

			detailViews.Clear();

			productViews.Hide();
			detailViews.Show();

			new LabelView(product.name).FontBold().FontSize(30).TextMiddleCenter().AddTo(detailViews);
			new SpaceView(5).AddTo(detailViews);
			new LabelView(product.description).FontSize(20).TextMiddleCenter().AddTo(detailViews);
			new ButtonView("创建版本", () => { OpenProductDetailWindow(product); }, true).FontBold()
				.FontSize(25).TextMiddleCenter().AddTo(detailViews);
			new SpaceView(5).AddTo(detailViews);
			new ButtonView("返回", () => EnqueueCmd(RebuildProductViews), true).FontSize(20).TextMiddleCenter()
				.AddTo(detailViews);


			foreach (var item in product.versions)
			{
				var fold = new FoldoutView(false, item.name + "	" + item.version).FontBold().FontSize(15)
					.TextMiddleLeft().AddTo(detailViews);

				var input = new ToDoListInputView((cate, name) => AddFoldoutItem(fold, item, cate, name));
				input.Show();

				fold.AddFoldoutView(input);
				foreach (var todoItem in item.todos)
				{
					AddTodoToFoldout(fold, item, todoItem);
				}
			}
		}


		public void OpenProductDetailWindow(Product _product)
		{
			EnqueueCmd(() =>
			{
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