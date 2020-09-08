using System;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.SystemComponent.Question;

namespace _04ToDoList.Editor.FrameWork.SystemComponent
{
    public class QuestionView : VerticalLayout, IQuestion
    {
        private Action onProcessed = null;
        private ButtonView yesBtn;
        private ButtonView noBtn;

        public QuestionView(string questionText, Action onYes, Action onNo, Action nextAct = null)
        {
            new LabelView(questionText).FontSize(25).TextMiddleCenter().AddTo(this);
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