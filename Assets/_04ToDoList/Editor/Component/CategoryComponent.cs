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

        public CategoryComponent(ToDoData.TodoCategory category)
        {
            if (category != null)
            {
                boxView = new BoxView(category.name).BackgroundColor(category.color.ToColor());
            }
            else
            {
                boxView = new BoxView("NULL").BackgroundColor(Color.gray);
            }
        }

        protected override void OnGUI()
        {
            boxView.DrawGUI();
        }
    }
}