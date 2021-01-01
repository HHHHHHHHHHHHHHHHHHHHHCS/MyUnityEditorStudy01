using _04ToDoList.Editor.FrameWork;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Utils;
using UnityEngine;

namespace _04ToDoList.Editor.Component
{
	public class CategoryComponent : View
	{
		private BoxView boxView { get; }

		public CategoryComponent(ToDoCategory category)
		{
			if (category != null)
			{
				boxView = new BoxView(category.name).MulBackgroundColor(category.color.ToColor(), 2);
			}
			else
			{
				boxView = new BoxView("NULL").MulBackgroundColor(Color.gray, 2);
			}
		}

		protected override void OnGUI()
		{
			boxView.DrawGUI();
		}
	}
}