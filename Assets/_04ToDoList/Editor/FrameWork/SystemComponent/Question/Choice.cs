using System;
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

        public QuestionView<Choice> BeginQuestion(string question = null, Action onYes = null, Action onNo = null)
        {
            Queue = new QuestionQueue();
            View = new QuestionView<Choice>(question, onYes, onNo);
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
            Queue.SetOnFinished(onProcess);
        }
    }
}