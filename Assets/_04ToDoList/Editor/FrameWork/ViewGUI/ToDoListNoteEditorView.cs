using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListNoteEditorView : VerticalLayout
    {
        public ToDoListNoteEditorView()
        {
            new LabelView("111")
                .TheFontStyle(FontStyle.Bold)
                .FontSize(40)
                .AddTo(this);
        }
    }
}