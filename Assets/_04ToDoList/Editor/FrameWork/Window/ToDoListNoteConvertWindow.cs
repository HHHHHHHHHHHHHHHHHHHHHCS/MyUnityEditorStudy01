using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.SystemComponent;
using _04ToDoList.Editor.FrameWork.SystemComponent.Question;
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

            bool isHide = true;


            ButtonView finishedBtn = new ButtonView("转换", () =>
            {
                ToDoDataManager.ConvertToDoNote(note, isHide);
                listView.UpdateList();
                Close();
                ToDoListMainWindow.instance.Focus();
            }, true).AddTo(verticalLayout);

            finishedBtn.Hide();

            ProcessSystem.CreateQuestions()
                .BeginQuestion()
                .SetTitle("现在是否可以执行1")
                .SetYesBtn("是", () => isHide = false)
                .SetYesBtn("否", () => isHide = true)
                .EndQuestion()
                .BeginQuestion(onYes: () => isHide = false, onNo: () => isHide = true)
                .SetTitle("现在是否可以执行2")
                .SetYesBtn("是", () => isHide = false)
                .SetYesBtn("否", () => isHide = true)
                .EndQuestion()
                .AddTo(verticalLayout)
                .StartProcess(finishedBtn.Show);
        }
    }
}