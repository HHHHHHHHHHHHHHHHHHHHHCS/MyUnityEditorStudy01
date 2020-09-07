using System;
using System.Collections;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.Layout;

namespace _04ToDoList.Editor.FrameWork.SystemComponent
{
    public class QuestionQueue : VerticalLayout
    {
        private Queue<QuestionView> queueViews = new Queue<QuestionView>();

        private Action onFinished;

        public void Add(QuestionView view)
        {
            view.AddTo(this);
            view.Hide();
            view.OnProcess(Next);
            queueViews.Enqueue(view);
        }

        public void Process()
        {
            Next();
        }

        public void Next()
        {
            if (queueViews.Count == 0)
            {
                onFinished?.Invoke();
                return;
            }
            queueViews.Dequeue().Show();
        }

        public void SetOnFinished(Action onAct)
        {
            onFinished = onAct;
        }
    }
}