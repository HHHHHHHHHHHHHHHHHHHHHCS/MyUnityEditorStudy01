using System;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.SystemComponent.Question;

namespace _04ToDoList.Editor.FrameWork.SystemComponent
{
    public class QuestionView : VerticalLayout, IQuestion
    {
        private LabelView titleLabel;
        private ButtonView yesBtn;
        private ButtonView noBtn;

        private Action onProcessed = null;

        public QuestionQueue Queue { get; set; }
        public ProcessSystem System { private get; set; }


        public QuestionView(string questionText = null, Action onYes = null, Action onNo = null, Action nextAct = null)
        {
            titleLabel = new LabelView(questionText).FontSize(25).TextMiddleCenter().AddTo(this);
            var horizontalLayout = new HorizontalLayout().AddTo(this);

            yesBtn = new ButtonView("是", onYes, true).AddTo(horizontalLayout);
            noBtn = new ButtonView("否", onNo, true).AddTo(horizontalLayout);

            if (nextAct != null)
            {
                yesBtn.OnClickEvent += nextAct;
                noBtn.OnClickEvent += nextAct;
            }


//            yesBtn.OnClickEvent += Hide;
//            noBtn.OnClickEvent += Hide;
        }

        public QuestionView SetTitle(string text)
        {
            titleLabel.SetText(text);
            return this;
        }

        public void Add(QuestionQueue queue)
        {
            queue.Add(this);
        }

        public QuestionView AddNextAction(Action nextAct)
        {
            if (nextAct != null)
            {
                yesBtn.OnClickEvent += nextAct;
                noBtn.OnClickEvent += nextAct;
            }

            return this;
        }


        public ProcessSystem EndQuestion()
        {
            return System;
        }

        void IQuestion.OnProcess(Action nextAct)
        {
            if (nextAct != null)
            {
                yesBtn.OnClickEvent += nextAct;
                noBtn.OnClickEvent += nextAct;
            }
        }
    }
}