using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListNoteView : VerticalLayout
    {
        private bool needAdd;

        public ToDoListNoteView()
        {
            Style = "box";

            new LabelView("欢迎来到笔记界面").AddTo(this);
            new ButtonView("创建笔记", () => { needAdd = true; }, true).AddTo(this);
        }

        public void OnUpdate()
        {
            if (!needAdd)
            {
                return;
            }

            needAdd = false;
            Clear();
            new ToDoListNoteEditorView().AddTo(this);
        }
    }
}