using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.Layout;

namespace _04ToDoList.Editor.FrameWork.SystemComponent.Question
{
    public class QuestionQueue : VerticalLayout
    {
        private Dictionary<string, Choice> choices = new Dictionary<string, Choice>();
        private Queue<IQuestion> queueViews = new Queue<IQuestion>();
        private IQuestion current;

        private Action onFinished;

        public void Add(IQuestion view)
        {
            view.AddTo(this);
            view.Hide();
            view.OnProcess(Next);
            //TODO;
            queueViews.Enqueue(view);
        }

        public void Process()
        {
            Next();
        }

        public void Next()
        {
            current?.Hide();

            if (queueViews.Count == 0)
            {
                onFinished?.Invoke();
                current = null;
                return;
            }

            current = queueViews.Dequeue();
            current.Show();
        }

        public void SetOnFinished(Action onAct)
        {
            onFinished = onAct;
        }

        public Choice GetChoice(string key) => choices[key];

        public Choice AddChoice(string key)
        {
            var choice = new Choice(key);
            choices.Add(key, choice);
            return choice;
        }
    }
}