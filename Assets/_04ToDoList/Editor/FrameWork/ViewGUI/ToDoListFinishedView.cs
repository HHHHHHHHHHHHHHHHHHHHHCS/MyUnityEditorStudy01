using System.Linq;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListFinishedView : VerticalLayout
    {
        private bool isDirty;


        public ToDoListFinishedView() : base("box")
        {
        }

        public void UpdateToDoItems()
        {
            isDirty = true;
        }

        public void OnUpdate()
        {
            Refresh();
            if (isDirty)
            {
                isDirty = false;
                ReBuildToDoItems();
            }
        }

        public void ReBuildToDoItems()
        {
            var dataList = ToDoListCls.ModelData.todoList;

            children.Clear();

            var groupsByDay = dataList.Where(item => item.state.Val == ToDoData.ToDoState.Done)
                .GroupBy(item => item.finishTime.Date);

            foreach (var group in groupsByDay)
            {
                Add(new LabelView(group.Key.ToString("yyyy年MM月dd日"))
                    .FontSize(20).TextMiddleCenter());
                foreach (var item in group)
                {
                    Add(new ToDoListItemView(item, RemoveFromParent, true));
                    Add(new SpaceView(4));
                }
            }


            RefreshVisible();
        }

        private void RefreshVisible()
        {
            Style = children.Count > 0 ? "box" : null;
        }

        private void RemoveFromParent(ToDoListItemView item)
        {
            isDirty = true;
        }
    }
}