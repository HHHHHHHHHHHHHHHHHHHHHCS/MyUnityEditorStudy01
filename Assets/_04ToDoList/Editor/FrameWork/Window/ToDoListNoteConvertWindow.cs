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
                .SetTitle("这个是什么?")
                .NewBtn(0, "资料")
                .NewBtn(1, "想法")
                .NewChoice(2, "事项", "事项")
                .EndQuestion()
                .BeginChoice("事项")
                .BeginQuestion()
                .SetTitle("现在是否可以执行!")
                .NewBtn(0, "是", () => isHide = false)
                .NewBtn(1, "否", () => isHide = true)
                .EndQuestion()
                .EndQuestion()
                .AddTo(verticalLayout)
                .StartProcess(finishedBtn.Show);
        }
    }
}