using _04TodoList.Editor.FrameWork.Layout.Interface;

namespace _04TodoList.Editor.FrameWork.Drawer.Interface
{
    public interface IView
    {
        ILayout Parent { get; set; }

        void DrawGUI();
    }
}