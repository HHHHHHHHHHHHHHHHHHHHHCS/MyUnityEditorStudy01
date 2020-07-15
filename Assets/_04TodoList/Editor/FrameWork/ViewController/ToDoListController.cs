using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewGUI;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewController
{
    public class ToDoListController : AbsViewController
    {
        private bool isShow;

        private ToDoListCls _toDoListCls;

        private ToolBarView todoListToolBarView;

        private ToDoListView todoListView;
        private ToDoListCategoryListView todoListCategoryListView;
        private ToDoListFinishedView todoListFinishedView;


        protected override void SetUpView()
        {
            _toDoListCls = ToDoListCls.ModelData;
            todoListToolBarView = new ToolBarView {style = "box"}.FontSize(15);
            todoListToolBarView
                .AddMenu("清单", () => { ChangePage(0); })
                .AddMenu("分类管理", () => { ChangePage(1); })
                .AddMenu("已完成", () => { ChangePage(2); });

            todoListView = new ToDoListView();
            todoListCategoryListView =  new ToDoListCategoryListView();
            todoListFinishedView = new ToDoListFinishedView();

            ChangePage(0);

            views.Add(todoListToolBarView);
            views.Add(todoListView);
            views.Add(todoListCategoryListView);
            views.Add(todoListFinishedView);
        }

        public void ChangePage(int i)
        {
            switch (i)
            {
                case 0:
                {
                    todoListView.Show();
                    todoListFinishedView.Hide();
                    todoListCategoryListView.Hide();
                    todoListView.ReBuildToDoItems();
                    break;
                }

                case 1:
                {
                    todoListView.Hide();
                    todoListFinishedView.Hide();
                    todoListCategoryListView.Show();
                    break;
                }

                case 2:
                {
                    todoListView.Hide();
                    todoListFinishedView.Show();
                    todoListCategoryListView.Hide();
                    todoListFinishedView.ReBuildToDoItems();
                    break;
                }

                default:
                {
                    Debug.Log(i + " page index is no exits!");
                    break;
                }
            }
        }

        protected override void OnUpdate()
        {
            todoListView.OnUpdate();
            todoListFinishedView.OnUpdate();
        }

        public void OnDisable()
        {
            _toDoListCls.Save();
        }
    }
}