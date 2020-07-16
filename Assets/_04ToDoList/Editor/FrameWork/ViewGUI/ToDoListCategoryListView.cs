using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.Window;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListCategoryListView : VerticalLayout
    {
        public ToDoListCategoryListView()
        {
            new LabelView("this is category list view").AddTo(this);

            new ButtonView("+", () =>
            {
                Debug.Log("add");

                AbsWindow.Open<ToDoListCategoryWindow>("123");
            }).AddTo(this);
        }
    }

    public class ToDoListCategoryWindow : AbsWindow
    {
        protected override void OnGUI()
        {
            
        }

        protected override void OnInit()
        {
        }

        protected override void Disable()
        {
        }

        protected override void Dispose()
        {
        }
    }
}