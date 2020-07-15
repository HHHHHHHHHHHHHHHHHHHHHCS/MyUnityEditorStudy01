using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListCategoryListView : VerticalLayout
    {
        public ToDoListCategoryListView()
        {
            new LabelView("this is category list view").AddTo(this);
        }

    }
}