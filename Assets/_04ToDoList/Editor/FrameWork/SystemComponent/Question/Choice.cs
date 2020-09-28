﻿using System;
using _04ToDoList.Editor.FrameWork.Layout;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.SystemComponent.Question
{
    public class Choice : VerticalLayout, IQuestionContainer<Choice>
    {
        public string choiceName;

        public QuestionView<Choice> View { get; set; }

        public QuestionQueue Queue { get; set; }

        public ProcessSystem System { private get; set; }

        public Choice(string choiceName)
        {
            this.choiceName = choiceName;
            Queue = new QuestionQueue();
        }

        public QuestionView<Choice> BeginQuestion()
        {
            Queue = new QuestionQueue();
            View = new QuestionView<Choice>().AddTo(this);
            View.Container = this;
            Queue.Add(View);
            return View;
        }

        public QuestionView<Choice> BeginQuestion(string question, Action onYes, Action onNo)
        {
            Queue = new QuestionQueue();
            View = new QuestionView<Choice>(question, onYes, onNo).AddTo(this);
            View.Container = this;
            Queue.Add(View);
            return View;
        }

        public ProcessSystem EndQuestion()
        {
            return System;
        }

        public void OnProcess(Action onProcess)
        {
            Queue.Process(); //Show
            Queue.SetOnFinished(onProcess);
        }
    }
}