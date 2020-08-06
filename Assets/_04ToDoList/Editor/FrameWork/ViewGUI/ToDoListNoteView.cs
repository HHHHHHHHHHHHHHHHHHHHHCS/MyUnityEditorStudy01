using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListNoteView : VerticalLayout
    {
        private LabelView titleLabelView;
        private ButtonView createButtonView;
        private ToDoListNoteEditorView editorView;


        private bool isDirty;

        public ToDoListNoteView()
        {
            Style = "box";

            titleLabelView = new LabelView("欢迎来到笔记界面")
                .TextMiddleCenter()
                .TheFontStyle(FontStyle.Bold)
                .FontSize(40).AddTo(this);
            createButtonView = new ButtonView("创建笔记", CreateNewEditor, true).AddTo(this);
            editorView = new ToDoListNoteEditorView(SaveAction).AddTo(this);

            editorView.Hide();
            isDirty = true;
        }

        protected override void OnRefresh()
        {
            if (!isDirty)
            {
                return;
            }

            isDirty = false;
            var notes = ToDoListCls.ModelData.notes;


            foreach (var item in notes)
            {
                new LabelView(item.content).AddTo(this);
            }
        }

        public void CreateNewEditor()
        {
            titleLabelView.Hide();
            createButtonView.Hide();
            editorView.Show();
            EnqueueCmd(() =>
            {
                
            });
        }

        public void SaveAction()
        {
            titleLabelView.Show();
            createButtonView.Show();
            editorView.Hide();
            isDirty = true;
        }
    }
}