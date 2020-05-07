using _04ToDoList.Editor.FrameWork.Layout.Interface;

namespace _04ToDoList.Editor.FrameWork.Drawer.Interface
{
    public interface IView
    {
        ILayout Parent { get; set; }

        void DrawGUI();
    }
}