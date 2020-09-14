using System;
using _04ToDoList.Editor.FrameWork.Layout.Interface;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.SystemComponent.Question
{
    public class ProcessSystem
    {
        private QuestionQueue queue = new QuestionQueue();

        public static ProcessSystem CreateQuestions()
        {
            return new ProcessSystem();
        }

        public QuestionView BeginQuestion(string question = null, Action onYes = null, Action onNo = null)
        {
            var view = new QuestionView(question, onYes, onNo);
            queue.Add(view);
            view.System = this;
            return view;
        }

        private ProcessSystem Add(string question = null, Action onYes = null, Action onNo = null)
        {
            queue.Add(new QuestionView(question, onYes, onNo));
            return this;
        }

        public ProcessSystem AddTo(ILayout layout)
        {
            queue.AddTo(layout);
            return this;
        }

        public void StartProcess(Action onFinish)
        {
            queue.Process();
            queue.SetOnFinished(onFinish);
        }
    }
}