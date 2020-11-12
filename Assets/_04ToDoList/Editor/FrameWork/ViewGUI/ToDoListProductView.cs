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
		public ToDoListProductView(AbsViewController _ctrl) : base(_ctrl, "box")
		{
			Rebuild();
		}

		public void Rebuild()
		{
			Clear();

			//new LabelView("这个是产品页面").AddTo(this).TextMiddleCenter().TheFontStyle(FontStyle.Bold).FontSize(30);

			new ButtonView("创建产品", () => OpenProductEditorWindow(null), true).Height(40).AddTo(this);

			var data = ToDoDataManager.Data.productList;
			for (int i = data.Count - 1; i >= 0; i--)
			{
				var product = data[i];

				var hor = new HorizontalLayout().AddTo(this);

				new ImageButtonView(ImageButtonIcon.openIcon, () => OpenProductEditorWindow(product))
					.Width(40).Height(25).BackgroundColor(Color.black).AddTo(hor);

				new LabelView(product.name).FontSize(20).TheFontStyle(FontStyle.Bold).Height(20).Width(60).AddTo(hor);

				new LabelView(product.description).FontSize(12).TheFontStyle(FontStyle.Bold).Height(20).AddTo(hor);

				new ImageButtonView(ImageButtonIcon.editorIcon, () => OpenProductEditorWindow(product))
					.Width(40).Height(25).BackgroundColor(Color.black).AddTo(hor);

				new ImageButtonView(ImageButtonIcon.deleteIcon, () => RemoveProduct(product)).Width(25).Height(25)
					.BackgroundColor(Color.red).AddTo(hor);
			}
		}

		public void OpenProductEditorWindow(Product product = null)
		{
			EnqueueCmd(() =>
			{
				ToDoListEditorProductSubWindow.Open(this, product);
			});
		}

		public void RemoveProduct(Product product)
		{
			ToDoDataManager.Data.RemoveProduct(product);
			EnqueueCmd(Rebuild);
		}
	}
}