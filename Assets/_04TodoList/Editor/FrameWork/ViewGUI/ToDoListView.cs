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

        public ToDoListView() : base()
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

        public void ReBuildToDoItems()
        {
            todoListItemsLayout.Clear();

            var dataList = ToDoListCls.ModelData.todoList
                .Where(item => (item.state.Val == ToDoData.ToDoState.Done) == false)
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

        private void AddAction(string _todoName)
        {
            var index = todoListInputView.PopupIndex;
            TodoCategory category = null;
            if (index >= 0)
            {
                category = ToDoListCls.ModelData.categoryList[index];
            }
            ToDoListCls.ModelData.Add(_todoName, false, category);
            UpdateToDoItems();
        }

        private void ChangeProperty(ToDoListItemView item)
        {
            isDirty = true;
        }
    }
}