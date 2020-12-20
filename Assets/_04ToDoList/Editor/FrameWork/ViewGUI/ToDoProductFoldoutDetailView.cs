using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.Window;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
	public class ToDoProductFoldoutDetailView : VerticalLayout
	{
		private ToDoListProductView todoListProductView;
		private Product product;
		private ToDoProductVersion productVersion;
		private VerticalLayout productViews;
		private ToDoListVersionDetailSubWindow versionDetailSubWindow;

		private FoldoutView foldoutView;

		public ToDoProductFoldoutDetailView(ToDoListProductView _todoListProductView,
			Product _product, ToDoProductVersion _productVersion,
			VerticalLayout _productViews, int insertIndex = -1) : base("box")
		{
			todoListProductView = _todoListProductView;
			product = _product;
			productVersion = _productVersion;
			productViews = _productViews;

			var editorBtn = new ImageButtonView(ImageButtonIcon.editorIcon,
				OpenVersionDetailWindow).BackgroundColor(Color.black).Width(30).Height(20);

			foldoutView = new FoldoutView(false, productVersion.name + "	" + productVersion.version, editorBtn)
				.FontBold().FontSize(15).TextMiddleLeft().AddTo(this);

			if (insertIndex < 0)
			{
				this.AddTo(_productViews);
			}
			else
			{
				this.InsertTo(insertIndex, _productViews);
			}

			var input = new ToDoListInputView(AddFoldoutItem);
			input.Show();

			foldoutView.AddFoldoutView(input);
			foreach (var todoItem in productVersion.todos)
			{
				AddToDoToFoldoutView(todoItem);
			}
		}

		public void OpenVersionDetailWindow()
		{
			EnqueueCmd(() =>
			{
				versionDetailSubWindow =
					ToDoListVersionDetailSubWindow.Open(todoListProductView, product, productViews, productVersion);
				versionDetailSubWindow.Show();
			});
		}


		private void AddToDoToFoldoutView(ToDoData todoItem)
		{
			var itemToDoView = new ToDoListItemView(todoItem, null);

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

		private void AddFoldoutItem(ToDoCategory category, string todoName)
		{
			EnqueueCmd(() =>
			{
				var item = ToDoDataManager.AddToDoItem(todoName, false, category);
				productVersion.todos.Add(item);
				ToDoDataManager.Data.Save();

				AddToDoToFoldoutView(item);
			});
		}
	}
}