using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewController;
using _04ToDoList.Editor.FrameWork.Window;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListNoteView : ToDoListPage
    {
        private LabelView titleLabelView;
        private ButtonView createButtonView;
        private ToDoListNoteEditorView editorView;
        private ScrollLayout noteListScrollLayout;

        private ToDoListNoteConvertWindow convertWindow;

        private bool isDirty;

        public ToDoListNoteView(AbsViewController ctrl) : base(ctrl, "box")
        {
            titleLabelView = new LabelView("欢迎来到笔记界面")
                .TextMiddleCenter()
                .TheFontStyle(FontStyle.Bold)
                .FontSize(40).AddTo(this);
            createButtonView = new ButtonView("创建笔记", () => CreateNewEditor(), true).AddTo(this);
            editorView = new ToDoListNoteEditorView(SaveAction).AddTo(this);
            noteListScrollLayout = new ScrollLayout().AddTo(this);

            editorView.Hide();
        }

        protected override void OnRefresh()
        {
            if (isDirty)
            {
                isDirty = false;
                ReBuildToDoItems();
            }
        }


        protected override void OnShow()
        {
            base.OnShow();
            ReBuildToDoItems();
        }

        public void UpdateList()
        {
            isDirty = true;
        }

        public void ReBuildToDoItems()
        {
            noteListScrollLayout.Clear();

            var notes = ToDoDataManager.Data.noteList;

            foreach (var item in notes)
            {
                var hor = new HorizontalLayout().AddTo(noteListScrollLayout);
                var temp = item;
                new ImageButtonView(ImageButtonIcon.editorIcon, () => CreateNewEditor(temp))
                    .Width(25).Height(25).BackgroundColor(Color.black).AddTo(hor);
                new LabelView(item.content).FontSize(15).TheFontStyle(FontStyle.Bold)
                    .TextMiddleLeft().AddTo(hor);
                new ButtonView("处理", () => OpenProcessWindow(item))
                    .Width(40).Height(20).AddTo(hor);
                new ImageButtonView(ImageButtonIcon.deleteIcon, () => DeleteItemNote(item))
                    .Width(25).Height(25).BackgroundColor(Color.red).AddTo(hor);
            }
        }

        public void CreateNewEditor(ToDoNote note = null)
        {
            titleLabelView.Hide();
            createButtonView.Hide();
            editorView.ReInit(note);
            editorView.Show();
        }


        public void OpenProcessWindow(ToDoNote note)
        {
            convertWindow = ToDoListNoteConvertWindow.Open(this, note);
            convertWindow.Show();
        }

        private void DeleteItemNote(ToDoNote note)
        {
            EnqueueCmd(() =>
            {
                ToDoDataManager.RemoveToDoNote(note);
                UpdateList();
            });
        }

        public void SaveAction()
        {
            titleLabelView.Show();
            createButtonView.Show();
            editorView.Hide();
            isDirty = true;
        }

        protected override void OnHide()
        {
            base.OnHide();
            if (convertWindow)
            {
                convertWindow.Close();
            }
        }
    }
}