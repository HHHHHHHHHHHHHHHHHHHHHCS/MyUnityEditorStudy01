using System.Collections;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewController
{
    public class ToDoListView : VerticalLayout
    {
        protected bool showFinished;
        private bool isDirty;


        public ToDoListView(bool _showFinished) : base("box")
        {
            UpdateShowFinished(_showFinished);
        }

        public void UpdateShowFinished(bool _showFinished)
        {
            showFinished = _showFinished;
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
            var todoListCls = ToDoListCls.ModelData;

            children.Clear();
            var dataList = todoListCls.todoList;
            if (dataList.Count == 0)
            {
                Style = null;
            }
            else
            {
                Style = "box";

                for (int i = dataList.Count - 1; i >= 0; --i)
                {
                    var item = todoListCls.todoList[i];


                    if ((item.state == ToDoData.ToDoState.Done) != showFinished)
                    {
                        continue;
                    }

                    HorizontalLayout horizontalLayout = new HorizontalLayout();

                    if (item.state == ToDoData.ToDoState.NoStart)
                    {
                        var startBtn = new ButtonView("开始", () =>
                        {
                            item.state.Val = ToDoData.ToDoState.Started;
                            isDirty = true;
                        });
                        horizontalLayout.Add(startBtn);
                    }
                    else if (item.state == ToDoData.ToDoState.Started)
                    {
                        var finishedBtn = new ButtonView("完成", () =>
                        {
                            item.state.Val = ToDoData.ToDoState.Done;
                            isDirty = true;
                        });
                        horizontalLayout.Add(finishedBtn);
                    }
                    else if (item.state == ToDoData.ToDoState.Done)
                    {
                        var finishedBtn = new ButtonView("重置", () =>
                        {
                            item.state.Val = ToDoData.ToDoState.NoStart;
                            isDirty = true;
                        });
                        horizontalLayout.Add(finishedBtn);
                    }

                    //                    var toggle = new ToggleView(item.content, item.finished);
                    //                    toggle.IsToggle.SetValueChanged((_val) =>
                    //                    {
                    //                        if (_val)
                    //                        {
                    //                            item.state.Val = ToDoData.ToDoState.Done;
                    //                        }
                    //                        else
                    //                        {
                    //                            item.state.Val = ToDoData.ToDoState.NoStart;
                    //                        }
                    //
                    //                        item.finished.Val = _val;
                    //                    });
                    //                    horizontalLayout.Add(toggle);

                    var contentLabel = new CustomView(()=>
                    {
                        GUILayout.Label(item.content);
                    });
                    horizontalLayout.Add(contentLabel);

                    var tempIndex = i; //这个是匿名函数嵌套 用的
                    var deleteBtn = new ButtonView("删除", () =>
                    {
                        item.finished.ClearValueChanged();
                        todoListCls.todoList.RemoveAt(tempIndex);
                        todoListCls.Save();
                        UpdateToDoItems();
                    });
                    horizontalLayout.Add(deleteBtn);
                    children.AddLast(horizontalLayout);
                }
            }
        }
    }
}