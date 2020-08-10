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
        private VerticalLayout noteListView;


        private bool isDirty;

        public ToDoListNoteView()
        {
            Style = "box";

            titleLabelView = new LabelView("欢迎来到笔记界面")
                .TextMiddleCenter()
                .TheFontStyle(FontStyle.Bold)
                .FontSize(40).AddTo(this);
            createButtonView = new ButtonView("创建笔记", () => CreateNewEditor(), true).AddTo(this);
            editorView = new ToDoListNoteEditorView(SaveAction).AddTo(this);
            noteListView = new VerticalLayout("box").AddTo(this);

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

            noteListView.Clear();

            var notes = ToDoListCls.ModelData.notes;

            foreach (var item in notes)
            {
                var hor = new HorizontalLayout().AddTo(noteListView);
                var temp = item;
                new ImageButtonView(ImageButtonIcon.editorIcon, () => { CreateNewEditor(temp); })
                    .Width(25).Height(25).AddTo(hor);
                new LabelView(item.content).FontSize(15).TheFontStyle(FontStyle.Bold)
                    .TextMiddleLeft().Height(25).AddTo(hor);
            }
        }

        public void CreateNewEditor(ToDoNote note = null)
        {
            titleLabelView.Hide();
            createButtonView.Hide();
            editorView.ReInit(note);
            editorView.Show();
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