using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
	public class ToDoProductFoldoutDetailView : View
	{
		private Product product;
		private ToDoProductVersion version;
		private VerticalLayout views;

		
		public ToDoProductFoldoutDetailView(Product _product, ToDoProductVersion _version, VerticalLayout _views)
		{
			product = _product;
			version = _version;
			views = _views;
			
			
			// var editorBtn = new ImageButtonView(ImageButtonIcon.editorIcon,
			// 		() => OpenVersionDetailWindow(product, detailViews, version))
			// 	.BackgroundColor(Color.black).Width(30).Height(20);
			//
			// var fold = new FoldoutView(false, version.name + "	" + version.version, editorBtn).FontBold().FontSize(15)
			// 	.TextMiddleLeft();
			//
			// if (insertIndex < 0)
			// {
			// 	fold.AddTo(views);
			// }
			// else
			// {
			// 	fold.InsertTo(insertIndex, views);
			// }
			//
			// var input = new ToDoListInputView((cate, name) => AddFoldoutItem(fold, version, cate, name));
			// input.Show();
			//
			// fold.AddFoldoutView(input);
			// foreach (var todoItem in version.todos)
			// {
			// 	AddTodoToFoldout(fold, version, todoItem);
			// }
		}

		protected override void OnGUI()
		{
			//TODO:如果修改则不全局是刷新 进行修改
			
			//foldout.draw!!!
			
		}
	}
}