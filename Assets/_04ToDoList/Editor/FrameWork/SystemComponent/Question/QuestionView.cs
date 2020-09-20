using System;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.SystemComponent.Question;

namespace _04ToDoList.Editor.FrameWork.SystemComponent
{
    public class QuestionView<T> : VerticalLayout, IQuestion<T> where T : IQuestionContainer
    {
        private LabelView titleLabel;
        private ButtonView yesBtn;
        private ButtonView noBtn;

        private Action onNext = null;

        public QuestionQueue Queue { get; set; }
        public T Container { private get; set; }


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

        //TODO:much button

        public QuestionView<T> SetTitle(string text)
        {
            titleLabel.SetText(text);
            return this;
        }

        public QuestionView<T> SetYesBtn(string text)
        {
            yesBtn.text = text;
            return this;
        }

        public QuestionView<T> SetYesBtn(string text, Action act)
        {
            yesBtn.text = text;
            yesBtn.OnClickEvent = act;
            if (onNext != null)
            {
                yesBtn.OnClickEvent += onNext;
            }

            return this;
        }

        public QuestionView<T> SetNoBtn(string text)
        {
            noBtn.text = text;
            return this;
        }

        public QuestionView<T> SetNoBtn(string text, Action act)
        {
            noBtn.text = text;
            noBtn.OnClickEvent = act;
            if (onNext != null)
            {
                noBtn.OnClickEvent += onNext;
            }

            return this;
        }

        public QuestionView<T> CreateChoice(string name, string dstMenuName)
        {
            new ButtonView(name, () =>
            {
                onNext?.Invoke();
            }).AddTo(this);
            return this;
        }

        public void Add(QuestionQueue queue)
        {
            queue.Add(this);
        }

        public QuestionView<T> AddNextAction(Action nextAct)
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


        public T EndQuestion()
        {
            return Container;
        }

        void IQuestion.OnProcess(Action action)
        {
            AddNextAction(action);
        }
    }
}