using System;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.ViewGUI;
using _04ToDoList.Util;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewController
{
    public class ToDoListController : AbsViewController
    {
        private bool isShow;


        private ToolBarView todoListToolBarView;

        private ToDoListNoteView todoListNoteView;
        private ToDoListView todoListView;
        private ToDoListCategoryListView todoListCategoryListView;
        private ToDoListFinishedView todoListFinishedView;


        protected override void SetUpView()
        {
            eventKey = GetHashCode();

            todoListNoteView = new ToDoListNoteView(this);
            todoListView = new ToDoListView(this);
            todoListCategoryListView = new ToDoListCategoryListView(this);
            todoListFinishedView = new ToDoListFinishedView(this);

            todoListToolBarView = new ToolBarView {style = "box"}.FontSize(15);
            todoListToolBarView.Height(40)
                .AddMenu("笔记", () => ChangePage(todoListNoteView.eventKey))
                .AddMenu("清单", () => { ChangePage(todoListView.eventKey); })
                .AddMenu("分类管理", () => { ChangePage(todoListCategoryListView.eventKey); })
                .AddMenu("已完成", () => { ChangePage(todoListFinishedView.eventKey); });

            views.Add(todoListToolBarView);
            views.Add(todoListNoteView);
            views.Add(todoListView);
            views.Add(todoListCategoryListView);
            views.Add(todoListFinishedView);


            todoListToolBarView.ForceClick(1);
        }

        public void ChangePage(int clickPage)
        {
            EventDispatcher.Send(eventKey, clickPage);
        }

        protected override void OnUpdate()
        {
        }

        public void OnDisable()
        {
            ToDoDataManager.Save();
        }
    }
}