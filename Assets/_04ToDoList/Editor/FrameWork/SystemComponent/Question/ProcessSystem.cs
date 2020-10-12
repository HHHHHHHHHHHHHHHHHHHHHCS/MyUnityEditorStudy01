using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.Layout.Interface;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.SystemComponent.Question
{
    public class ProcessSystem : IQuestionContainer<ProcessSystem>
    {
        private QuestionQueue queue;
        private Dictionary<string, Choice> choices;

        public static ProcessSystem CreateQuestions()
        {
            return new ProcessSystem();
        }

        public ProcessSystem()
        {
            choices = new Dictionary<string, Choice>();
            queue = new QuestionQueue {System = this};
        }

        public QuestionView<ProcessSystem> BeginQuestion()
        {
            var view = new QuestionView<ProcessSystem>();
            queue.Add(view);
            view.Container = this;
            return view;
        }

        public QuestionView<ProcessSystem> BeginQuestion(string title, string context, Action onYes,
            Action onNo)
        {
            var view = new QuestionView<ProcessSystem>(title, context, onYes, onNo);
            queue.Add(view);
            view.Container = this;
            return view;
        }


        public Choice BeginChoice(string choiceName)
        {
            var ch = queue.AddChoice(choiceName);
            ch.System = this;
            return ch;
        }


        public ProcessSystem AddTo(ILayout layout)
        {
            queue.AddTo(layout);
            return this;
        }

        public ProcessSystem StartProcess(Action onFinish)
        {
            queue.Process();
            queue.SetOnFinished(onFinish);
            return this;
        }

        public Choice GetChoice(string key)
        {
            return choices[key];
        }

        public Choice AddChoice(string key,Choice choice)
        {
            choices.Add(key, choice);
            return choice;
        }
    }
}