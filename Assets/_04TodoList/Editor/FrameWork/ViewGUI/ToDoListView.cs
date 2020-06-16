using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewGUI;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewController
{
    public class ToDoListView : VerticalLayout
    {
        private bool isDirty;

        private ToDoListInputView todoListInputView;
        private VerticalLayout todoListItemsLayout;

        private string itemsStyle;

        public ToDoListView() : base()
        {
            itemsStyle = null;
            todoListInputView = new ToDoListInputView(AddAction);
            todoListItemsLayout = new VerticalLayout();
            Add(todoListInputView);
            Add(new SpaceView(4));
            Add(todoListItemsLayout);

        }

        public void UpdateToDoItems()
        {
            isDirty = true;
        }

        public void OnUpdate()
        {
            todoListItemsLayout.Refresh();
            if (isDirty)
            {
                isDirty = false;
                ReBuildToDoItems();
            }
        }

        public void ReBuildToDoItems()
        {
            var dataList = ToDoListCls.ModelData.todoList;

            todoListItemsLayout.Clear();

            for (int i = dataList.Count - 1; i >= 0; --i)
            {
                var item = dataList[i];
                if ((item.state.Val == ToDoData.ToDoState.Done) == false)
                {
                    todoListItemsLayout.Add(new ToDoListItemView(item, RemoveFromParent));
                }
            }

            RefreshVisible();
        }

        private void RefreshVisible()
        {
            todoListItemsLayout.Style = children.Count > 0 ? "box" : null;
        }

        private void AddAction(string _todoName)
        {
            ToDoListCls.ModelData.Add(_todoName, false);
            UpdateToDoItems();
        }

        private void RemoveFromParent(ToDoListItemView item)
        {
            todoListItemsLayout.Remove(item);
        }
    }
}