using System.Linq;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewController;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListHideView : ToDoListPage
    {
        private bool isDirty;

        private VerticalLayout todoListItemsLayout;

        public ToDoListHideView(AbsViewController ctrl) : base(ctrl)
        {
            todoListItemsLayout = new VerticalLayout();
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
            ReBuildToDoItems();
        }

        public void ReBuildToDoItems()
        {
            todoListItemsLayout.Clear();

            var dataList = ToDoDataManager.Data.todoList
                .Where(item => (item.state.Val == ToDoData.ToDoState.Done) == false
                               && item.isHide == true)
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
            if (todoListItemsLayout.children.Count > 0)
            {
                todoListItemsLayout.Style =  "box";
            }
            else
            {
                todoListItemsLayout.Style =  null;
            }
        }


        private void ChangeProperty(ToDoListItemView item)
        {
            isDirty = true;
        }
    }
}