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

        public ProcessSystem Add(string question, Action onYes = null, Action onNo = null)
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