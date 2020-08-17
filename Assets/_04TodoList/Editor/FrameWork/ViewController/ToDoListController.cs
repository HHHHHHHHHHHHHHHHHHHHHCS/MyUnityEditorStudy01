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

        private ToDoListNoteView todoListNoteView;
        private ToDoListView todoListView;
        private ToDoListCategoryListView todoListCategoryListView;
        private ToDoListFinishedView todoListFinishedView;


        protected override void SetUpView()
        {
            _toDoListCls = ToDoListCls.ModelData;
            todoListToolBarView = new ToolBarView {style = "box"}.FontSize(15);
            todoListToolBarView.Height(40)
                .AddMenu("笔记", () => { ChangePage(0); })
                .AddMenu("清单", () => { ChangePage(1); })
                .AddMenu("分类管理", () => { ChangePage(2); })
                .AddMenu("已完成", () => { ChangePage(3); });

            todoListNoteView = new ToDoListNoteView();
            todoListView = new ToDoListView();
            todoListCategoryListView = new ToDoListCategoryListView();
            todoListFinishedView = new ToDoListFinishedView();


            views.Add(todoListToolBarView);
            views.Add(todoListNoteView);
            views.Add(todoListView);
            views.Add(todoListCategoryListView);
            views.Add(todoListFinishedView);


            todoListToolBarView.ForceClick(1);
        }

        public void ChangePage(int i)
        {
            switch (i)
            {
                case 0:
                {
                    todoListNoteView.Show();
                    todoListView.Hide();
                    todoListCategoryListView.Hide();
                    todoListFinishedView.Hide();
                    todoListView.ReBuildToDoItems();
                    break;
                }

                case 1:
                {
                    todoListNoteView.Hide();
                    todoListView.Show();
                    todoListCategoryListView.Hide();
                    todoListFinishedView.Hide();
                    todoListView.ReBuildToDoItems();
                    break;
                }

                case 2:
                {
                    todoListNoteView.Hide();
                    todoListView.Hide();
                    todoListCategoryListView.Show();
                    todoListFinishedView.Hide();
                    break;
                }

                case 3:
                {
                    todoListNoteView.Hide();
                    todoListView.Hide();
                    todoListCategoryListView.Hide();
                    todoListFinishedView.Show();
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
        }

        public void OnDisable()
        {
            _toDoListCls.Save();
        }
    }
}