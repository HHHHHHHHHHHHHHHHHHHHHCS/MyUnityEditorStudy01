using System;
using System.Linq;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewController;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListFinishedView : ToDoListPage
    {
        private bool isDirty;

        private VerticalLayout todosParent;

        public ToDoListFinishedView(AbsViewController ctrl) : base(ctrl)
        {
            Add(new SpaceView());
            var scrollLayout = new ScrollLayout();
            todosParent = new VerticalLayout();
            scrollLayout.Add(todosParent);
            Add(scrollLayout);
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
            var dataList = ToDoDataManager.Data.todoList;

            todosParent.children.Clear();

            var groupsByDay = dataList.Where(item => item.state.Val == ToDoData.ToDoState.Done)
                .GroupBy(item => item.finishTime.Date)
                .OrderByDescending(item => item.Key);

            foreach (var group in groupsByDay)
            {
                TimeSpan totalTime = TimeSpan.Zero;
                foreach (var item in group)
                {
                    totalTime += item.UsedTime;
                }

                todosParent.Add(
                    new LabelView(group.Key.ToString("yyyy年MM月dd日 (共" + ToDoData.UsedTimeToString(totalTime) + ")"))
                        .FontSize(20).TextLowCenter());

                todosParent.Add(new SpaceView(4));


                foreach (var item in group.OrderByDescending(val => val.finishTime))
                {
                    todosParent.Add(new ToDoListItemView(item, RemoveFromParent, true));
                    todosParent.Add(new SpaceView(4));
                }
            }


            RefreshVisible();
        }

        private void RefreshVisible()
        {
            todosParent.Style = todosParent.children.Count > 0 ? "box" : null;
        }

        private void RemoveFromParent(ToDoListItemView item)
        {
            isDirty = true;
        }
    }
}