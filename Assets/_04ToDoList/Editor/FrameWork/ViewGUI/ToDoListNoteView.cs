using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListNoteView:VerticalLayout
    {
        public ToDoListNoteView()
        {
            Style = "box";

            new LabelView("欢迎来到笔记界面").AddTo(this);
        }
    }
}