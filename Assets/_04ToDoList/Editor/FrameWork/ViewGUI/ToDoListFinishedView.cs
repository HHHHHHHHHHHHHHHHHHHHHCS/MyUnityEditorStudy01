using _04ToDoList.Editor.FrameWork.DataBinding;
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

            for (int i = dataList.Count - 1; i >= 0; --i)
            {
                var item = dataList[i];
                if ((item.state.Val == ToDoData.ToDoState.Done) == true)
                {
                    children.AddLast(new ToDoListItemView(item, UpdateToDoItems));
                }
            }

            RefreshVisible();
        }

        private void RefreshVisible()
        {
            Style = children.Count > 0 ? "box" : null;
        }
    }
}
