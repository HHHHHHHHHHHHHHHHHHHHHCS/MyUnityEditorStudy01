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

            new LabelView("名字").AddTo(categorySubWindow);

            string itemName = string.Empty;

            new TextAreaView(itemName, (s) => itemName = s).AddTo(categorySubWindow);

            new LabelView("颜色").AddTo(categorySubWindow);

            Color itemColor = Color.black;

            new ColorView(itemColor, (c) => itemColor = c).AddTo(categorySubWindow);

            new ButtonView("添加", () =>
                {
                    ToDoListCls.ModelData.categoryList.Add(new ToDoData.TodoCategory(itemName, itemColor.ToText()));
                    ToDoListCls.ModelData.Save();
                    CloseSubWindow();
                }, true)
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