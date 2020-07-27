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
        private static readonly Texture2D editorIcon;
        private static readonly Texture2D deleteIcon;

        private ToDoListCategorySubWindow _categorySubWindow;

        private VerticalLayout verticalLayout;

        private Queue<Action> cmdQueue = new Queue<Action>();
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
            editorIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_04ToDoList/EditorIcons/Editor.png");
            deleteIcon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/_04ToDoList/EditorIcons/Delete.png");
        }

        public ToDoListCategoryListView() : base("box")
        {
            new ButtonView("+", () => OpenSubWindow(), true)
                .BackgroundColor(Color.yellow).AddTo(this);

            new SpaceView(4f).AddTo(this);

            verticalLayout = new VerticalLayout("box").AddTo(this);
        }


        public void OnUpdate()
        {
            if (isDirty)
            {
                isDirty = false;
                Rebuild();
            }

            while (cmdQueue.Count > 0)
            {
                cmdQueue.Dequeue()?.Invoke();
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

            foreach (var item in ToDoListCls.ModelData.categoryList)
            {
                var layout = new HorizontalLayout("box").AddTo(verticalLayout);

                //new BoxView(item.name).BackgroundColor(item.color.ToColor()).AddTo(layout);

                var categoryComponent = new CategoryComponent(item).AddTo(layout);


                new FlexibleSpaceView().AddTo(layout);

                new ImageButtonView(editorIcon, () => OpenSubWindow(item))
                    .Width(25).Height(25).BackgroundColor(Color.black).AddTo(layout);

                new ImageButtonView(deleteIcon, () =>
                    {
                        ToDoListCls.ModelData.categoryList.Remove(item);
                        ToDoListCls.ModelData.Save();
                        UpdateToDoItems();
                    })
                    .Width(25).Height(25).BackgroundColor(Color.red).AddTo(layout);
            }
        }

        private void OpenSubWindow(ToDoData.TodoCategory item = null)
        {
            cmdQueue.Enqueue(() => { categorySubWindow.ShowWindow(item); });
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