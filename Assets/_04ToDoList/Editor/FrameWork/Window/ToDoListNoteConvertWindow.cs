using System;
using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewGUI;

namespace _04ToDoList.Editor.FrameWork.Window
{
    public class ToDoListNoteConvertWindow : SubWindow
    {
        private ToDoListNoteView listView;
        private ToDoNote note;

        public static ToDoListNoteConvertWindow Open(ToDoListNoteView _todoListNoteView, ToDoNote _note,
            string name = "ToDo Note Convert")
        {
            var window = Open<ToDoListNoteConvertWindow>(name);
            window.listView = _todoListNoteView;
            window.note = _note;
            return window;
        }

        private void Awake()
        {
            var verticalLayout = new VerticalLayout().AddTo(this);

            new LabelView("现在是否可以执行").FontSize(25).TextMiddleCenter().AddTo(verticalLayout);

            var horizontalLayout = new HorizontalLayout().AddTo(verticalLayout);

            new ButtonView("是", () => { }, true).AddTo(horizontalLayout);
            new ButtonView("否", () => { }, true).AddTo(horizontalLayout);

            new ButtonView("转换", () =>
            {
                ToDoDataManager.ConvertToDoNote(note);
                listView.UpdateList();
                Close();
                ToDoListMainWindow.instance.Focus();
            }, true).AddTo(verticalLayout);
        }
    }
}