using System;
using System.Linq;
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
		private BoxView stateBoxView;

		public ToDoListProductFoldoutDetailView(ToDoListProductDetailView _productDetailView
			, ToDoProduct _todoProduct, ToDoProductVersion _productVersion) : base("box")
		{
			productDetailView = _productDetailView;
			todoProduct = _todoProduct;
			productVersion = _productVersion;

			HorizontalLayout horView = new HorizontalLayout();

			new FlexibleSpaceView().AddTo(horView);

			stateBoxView = new BoxView(String.Empty).Height(20).AddTo(horView);

			new ImageButtonView(ImageButtonIcon.editorIcon,
				OpenVersionDetailWindow).BackgroundColor(Color.black).Width(30).Height(20).AddTo(horView);

			new ImageButtonView(ImageButtonIcon.deleteIcon,
				DeleteItem).BackgroundColor(Color.red).Width(30).Height(20).AddTo(horView);

			foldoutView = new FoldoutView(false, productVersion.name + "	" + productVersion.version, horView)
				.FontBold().FontSize(15).MarginLeft(30).TextMiddleLeft().AddTo(this);


			var input = new ToDoListInputView(AddFoldoutItem);
			input.Show();

			foldoutView.AddFoldoutView(input);

			RefreshStateWithView();

			foreach (var todoItem in productVersion.todos)
			{
				AddToDoToFoldoutView(todoItem);
			}
		}

		public void RefreshStateWithView()
		{
			productVersion.ToDoOrderBy(x => x.state.Val);

			string stateText = string.Empty;
			Color stateColor = Color.black;
			switch (productVersion.GetStates())
			{
				case ToDoData.ToDoState.NoStart:
					stateText = "暂无任务";
					stateColor = Color.yellow;
					break;
				case ToDoData.ToDoState.Started:
					stateText = "正在进行";
					stateColor = Color.red;
					break;
				case ToDoData.ToDoState.Done:
					stateText = "全部完成";
					stateColor = Color.green;
					break;
			}

			stateBoxView.Context = stateText;
			stateBoxView.MulBackgroundColor(stateColor, 2);

			// foldoutView.ClearFoldoutView();
			// foreach (var todoItem in productVersion.todos)
			// {
			// 	AddToDoToFoldoutView(todoItem);
			// }
		}

		public void OpenVersionDetailWindow()
		{
			//防止同一帧数内 颜色污染
			EnqueueCmd(() => { ToDoListVersionDetailSubWindow.Open(productDetailView, todoProduct, productVersion); });
		}

		public void DeleteItem()
		{
			todoProduct.versions.Remove(productVersion);

			//防止同一帧数内 颜色污染
			EnqueueCmd(() => { productDetailView.versionDetailView.Remove(this); });
		}

		private void AddToDoToFoldoutView(ToDoData todoItem)
		{
			var itemToDoView = new ToDoListItemView(todoItem, (_) => RefreshStateWithView());

			itemToDoView.deleteAct = (_) =>
			{
				EnqueueCmd(() =>
				{
					ToDoDataManager.RemoveProductToDoItem(productVersion, todoItem);
					foldoutView.RemoveFoldoutView(itemToDoView);
				});
			};

			foldoutView.AddFoldoutView(itemToDoView);
		}

		private void AddFoldoutItem(ToDoCategory category, string todoName)
		{
			EnqueueCmd(() =>
			{
				var item = ToDoDataManager.CreateToDoItem(todoName, false, category, productVersion.id);
				ToDoDataManager.AddProductToDoItem(productVersion, item);

				AddToDoToFoldoutView(item);
				RefreshStateWithView();
			});
		}
	}
}