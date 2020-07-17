using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.Window;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListCategoryListView : VerticalLayout
    {
        private SubWindow categorySubWindow;

        public ToDoListCategoryListView() : base("box")
        {
            new LabelView("this is category list view").AddTo(this);

            new ButtonView("+", CreateSubWindow, true).AddTo(this);
        }

        private void CreateSubWindow()
        {
            categorySubWindow = ToDoListMainWindow.instance.CreateSubWindow();

            categorySubWindow.Clear();

            new ButtonView("添加", () => { Debug.Log("add"); }, true)
                .AddTo(categorySubWindow);

            new ButtonView("关闭", () => { categorySubWindow.Close(); }, true)
                .AddTo(categorySubWindow);

            categorySubWindow.Show();
        }

        private void CloseSubWindow()
        {
            if (categorySubWindow != null)
            {
                categorySubWindow.Close();
            }
        }

        protected override void OnHide()
        {
            base.OnHide();
            CloseSubWindow();
        }
    }
}