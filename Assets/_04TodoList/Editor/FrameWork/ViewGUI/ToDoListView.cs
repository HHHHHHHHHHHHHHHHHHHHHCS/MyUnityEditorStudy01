using System.Linq;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewController;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
	public class ToDoListView : ToDoListPage
	{
		private bool isDirty;

		private ToDoListInputView todoListInputView;
		private VerticalLayout todoListItemsLayout;

		public ToDoListView(AbsViewController ctrl) : base(ctrl)
		{
			todoListInputView = new ToDoListInputView(AddAction);
			todoListItemsLayout = new VerticalLayout();
			new SpaceView(4).AddTo(this);
			todoListInputView.AddTo(this);
			new SpaceView(4).AddTo(this);
			todoListItemsLayout.AddTo(new ScrollLayout().AddTo(this));
		}

		public void UpdateToDoItems()
		{
			isDirty = true;
		}

		protected override void OnRefresh()
		{
			if (isDirty)
			{
				isDirty = false;
				ReBuildToDoItems();
			}
		}

		protected override void OnShow()
		{
			base.OnShow();
			todoListInputView.Show();
			ReBuildToDoItems();
		}

		public void ReBuildToDoItems()
		{
			todoListItemsLayout.Clear();

			var dataList = ToDoDataManager.Data.todoList
				.Where(item => ((item.state.Val == ToDoData.ToDoState.Done) == false)
				               && item.isHide == false)
				.OrderByDescending(item => item.state.Val)
				.ThenBy(item => item.priority.Val);

			foreach (var item in dataList)
			{
				new ToDoListItemView(item, ChangeProperty).AddTo(todoListItemsLayout);
				new SpaceView(4).AddTo(todoListItemsLayout);
			}

			RefreshVisible();
		}

		private void RefreshVisible()
		{
			todoListItemsLayout.Style = todoListItemsLayout.children.Count > 0 ? "box" : null;
		}

		private void AddAction(ToDoCategory category, string _todoName)
		{
			ToDoDataManager.AddToDoItem(_todoName, false, category);
			UpdateToDoItems();
		}

		private void ChangeProperty(ToDoListItemView item)
		{
			//isDirty = true;
			ReBuildToDoItems();
		}
	}
}