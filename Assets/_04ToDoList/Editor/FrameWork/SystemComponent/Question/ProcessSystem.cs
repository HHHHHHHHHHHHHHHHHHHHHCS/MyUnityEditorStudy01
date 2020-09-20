using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.Layout.Interface;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.SystemComponent.Question
{
    public class ProcessSystem : IQuestionContainer<ProcessSystem>
    {
        private QuestionQueue queue = new QuestionQueue();
        private Dictionary<string, Choice> choices = new Dictionary<string, Choice>();


        public static ProcessSystem CreateQuestions()
        {
            return new ProcessSystem();
        }

        public QuestionView<ProcessSystem> BeginQuestion(string question = null, Action onYes = null,
            Action onNo = null)
        {
            var view = new QuestionView<ProcessSystem>(question, onYes, onNo);
            queue.Add(view);
            view.Container = this;
            return view;
        }


        public Choice BeginChoice(string choiceName)
        {
            var ch = new Choice(choiceName);
            choices.Add(choiceName, ch);
            ch.System = this;
            return ch;
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