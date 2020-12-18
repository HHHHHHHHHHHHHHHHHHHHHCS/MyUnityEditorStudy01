using _04ToDoList.Editor.FrameWork.Drawer.Interface;

namespace _04ToDoList.Editor.FrameWork.Layout.Interface
{
	public interface ILayout : IView
	{
		void Add(IView view);

		void Insert(int index, IView view);

		void Remove(IView view);
	}
}