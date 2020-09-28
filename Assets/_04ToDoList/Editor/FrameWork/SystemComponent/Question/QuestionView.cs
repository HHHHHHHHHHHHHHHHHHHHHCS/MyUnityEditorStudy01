using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.SystemComponent.Question;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.SystemComponent
{
    public class QuestionView<T> : VerticalLayout, IQuestion<T> where T : IQuestionContainer
    {
        private LabelView titleLabel;
        private List<ButtonView> btnViews;

        private Action onNext = null;
        private Action<string> onChoice = null;


        public QuestionQueue Queue { get; set; }
        public T Container { private get; set; }


        public QuestionView(Action nextAct = null)
        {
            titleLabel = new LabelView(string.Empty).FontSize(25).TextMiddleCenter().AddTo(this);
            btnViews = new List<ButtonView>();
            AddNextAction(nextAct);
        }


        public QuestionView(string questionText, Action onYes, Action onNo, Action nextAct = null)
        {
            titleLabel = new LabelView(questionText).FontSize(25).TextMiddleCenter().AddTo(this);
            var horizontalLayout = new HorizontalLayout().AddTo(this);

            //0 yes  1 no
            btnViews = new List<ButtonView>();
            btnViews.Add(new ButtonView("是", onYes, true).AddTo(horizontalLayout));
            btnViews.Add(new ButtonView("否", onNo, true).AddTo(horizontalLayout));

            AddNextAction(nextAct);
        }


        public QuestionView<T> SetTitle(string text)
        {
            titleLabel.SetText(text);
            return this;
        }

        public QuestionView<T> NewBtn(int index, string text, Action act = null)
        {
            var btn = new ButtonView(text, act, true).FontSize(25).TextMiddleCenter().AddTo(this);
            btnViews.Add(btn);
            if (onNext != null)
            {
                btn.OnClickEvent += onNext;
            }

            return this;
        }

        public QuestionView<T> NewChoice(int index, string text, string dest)
        {
            var btn = new ButtonView(text, () => { onChoice?.Invoke(text); }, true).FontSize(25).TextMiddleCenter()
                .AddTo(this);
            // if (onNext != null)
            // {
            //     btn.OnClickEvent += onNext;
            // }

            return this;
        }

        public QuestionView<T> SetBtnText(int index, string text)
        {
            btnViews[index].text = text;
            return this;
        }

        public QuestionView<T> SetBtnAct(int index, Action act)
        {
            var btn = btnViews[index];
            btn.OnClickEvent = act;
            if (onNext != null)
            {
                btn.OnClickEvent += onNext;
            }

            return this;
        }

        public QuestionView<T> SetBtn(int index, string text, Action act)
        {
            var btn = btnViews[index];
            btn.text = text;
            btn.OnClickEvent = act;
            if (onNext != null)
            {
                btn.OnClickEvent += onNext;
            }

            return this;
        }


        public QuestionView<T> CreateChoice(string name, string destMenuName)
        {
            new ButtonView(name, () => { onChoice?.Invoke(destMenuName); }).AddTo(this);
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
                for (int i = 0; i < btnViews.Count; ++i)
                {
                    var btn = btnViews[i];
                    if (btn.OnClickEvent != null)
                    {
                        btn.OnClickEvent -= onNext;
                    }
                }
            }

            onNext = nextAct;
            if (nextAct != null)
            {
                for (int i = 0; i < btnViews.Count; ++i)
                {
                    var btn = btnViews[i];
                    if (btn.OnClickEvent != null)
                    {
                        btn.OnClickEvent += onNext;
                    }
                }
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

        void IQuestion.OnChoice(Action<string> choice)
        {
            onChoice = choice;
        }
    }
}