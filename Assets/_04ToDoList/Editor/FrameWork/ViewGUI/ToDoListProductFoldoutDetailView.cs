using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.Window;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
	public class ToDoListProductFoldoutDetailView : VerticalLayout
	{
		private ToDoListProductDetailView productDetailView;
		private ToDoProduct todoProduct;
		private ToDoProductVersion productVersion;

		private FoldoutView foldoutView;

		public ToDoListProductFoldoutDetailView(ToDoListProductDetailView _productDetailView
			, ToDoProduct _todoProduct, ToDoProductVersion _productVersion) : base("box")
		{
			productDetailView = _productDetailView;
			todoProduct = _todoProduct;
			productVersion = _productVersion;

			//支持删除  遍历全部的item  添加label 决定是否完成
			var editorBtn = new ImageButtonView(ImageButtonIcon.editorIcon,
				OpenVersionDetailWindow).BackgroundColor(Color.black).Width(30).Height(20);

			foldoutView = new FoldoutView(false, productVersion.name + "	" + productVersion.version, editorBtn)
				.FontBold().FontSize(15).MarginLeft(30).TextMiddleLeft().AddTo(this);


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
			//防止同一帧数内 颜色污染
			EnqueueCmd(() =>
			{
				ToDoListVersionDetailSubWindow.Open(productDetailView, todoProduct, productVersion);
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
					ToDoDataManager.Save();
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