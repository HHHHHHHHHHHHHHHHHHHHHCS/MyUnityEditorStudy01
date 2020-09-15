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

        private Action onNext = null;

        public QuestionQueue Queue { get; set; }
        public ProcessSystem System { private get; set; }


        public QuestionView(string questionText = null, Action onYes = null, Action onNo = null, Action nextAct = null)
        {
            titleLabel = new LabelView(questionText).FontSize(25).TextMiddleCenter().AddTo(this);
            var horizontalLayout = new HorizontalLayout().AddTo(this);

            yesBtn = new ButtonView("是", onYes, true).AddTo(horizontalLayout);
            noBtn = new ButtonView("否", onNo, true).AddTo(horizontalLayout);

            AddNextAction(nextAct);

//            yesBtn.OnClickEvent += Hide;
//            noBtn.OnClickEvent += Hide;
        }

        public QuestionView SetTitle(string text)
        {
            titleLabel.SetText(text);
            return this;
        }

        public QuestionView SetYesBtn(string text)
        {
            yesBtn.text = text;
            return this;
        }

        public QuestionView SetYesBtn(string text, Action act)
        {
            yesBtn.text = text;
            yesBtn.OnClickEvent = act;
            if (onNext != null)
            {
                yesBtn.OnClickEvent += onNext;
            }
            return this;
        }

        public QuestionView SetNoBtn(string text)
        {
            noBtn.text = text;
            return this;
        }

        public QuestionView SetNoBtn(string text, Action act)
        {
            noBtn.text = text;
            noBtn.OnClickEvent = act;
            if (onNext != null)
            {
                noBtn.OnClickEvent += onNext;
            }
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
                if (yesBtn.OnClickEvent != null)
                {
                    yesBtn.OnClickEvent -= nextAct;
                }
                if (noBtn.OnClickEvent != null)
                {
                    noBtn.OnClickEvent -= nextAct;
                }
            }

            onNext = nextAct;
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
            AddNextAction(nextAct);
        }
    }
}