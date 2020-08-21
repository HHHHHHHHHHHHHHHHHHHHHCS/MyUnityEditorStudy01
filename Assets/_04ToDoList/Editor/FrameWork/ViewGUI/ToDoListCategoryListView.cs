using System;
using System.Collections.Generic;
using _04ToDoList.Editor.Component;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.Utils;
using _04ToDoList.Editor.FrameWork.Window;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListCategoryListView : VerticalLayout
    {
        private ToDoListCategorySubWindow _categorySubWindow;

        private VerticalLayout verticalLayout;

        private bool isDirty;

        private ToDoListCategorySubWindow categorySubWindow
        {
            get
            {
                if (_categorySubWindow == null)
                {
                    _categorySubWindow = ToDoListMainWindow.CreateCategorySubWindow(this);
                }

                return _categorySubWindow;
            }
        }


        static ToDoListCategoryListView()
        {
        }

        public ToDoListCategoryListView() : base("box")
        {
            new ButtonView("+", () => OpenSubWindow(), true)
                .BackgroundColor(Color.yellow).AddTo(this);

            new SpaceView(4f).AddTo(this);

            verticalLayout = new VerticalLayout("box").AddTo(this);
        }


        protected override void OnRefresh()
        {
            if (isDirty)
            {
                isDirty = false;
                Rebuild();
            }
        }

        public void UpdateToDoItems()
        {
            isDirty = true;

            //如果不focus 则会不刷新
            ToDoListMainWindow.instance.Focus();
        }


        private void Rebuild()
        {
            verticalLayout.Clear();

            var data = ToDoDataManager.Data;

            foreach (var item in data.categoryList)
            {
                var layout = new HorizontalLayout("box").AddTo(verticalLayout);

                //new BoxView(item.name).BackgroundColor(item.color.ToColor()).AddTo(layout);

                new CategoryComponent(item).AddTo(layout);


                new FlexibleSpaceView().AddTo(layout);

                new ImageButtonView(ImageButtonIcon.editorIcon, () => OpenSubWindow(item))
                    .Width(25).Height(25).BackgroundColor(Color.black).AddTo(layout);

                new ImageButtonView(ImageButtonIcon.deleteIcon, () =>
                    {

                        ToDoDataManager.RemoveToDoCategory(item);
                        UpdateToDoItems();
                    })
                    .Width(25).Height(25).BackgroundColor(Color.red).AddTo(layout);
            }
        }

        private void OpenSubWindow(TodoCategory item = null)
        {
            EnqueueCmd(() => { categorySubWindow.ShowWindow(item); });
        }

        protected override void OnShow()
        {
            isDirty = true;
        }

        protected override void OnHide()
        {
            base.OnHide();
            //_categorySubWindow 可能是空的 不一定要创建
            if (_categorySubWindow != null)
            {
                _categorySubWindow.Close();
            }
        }
    }
}