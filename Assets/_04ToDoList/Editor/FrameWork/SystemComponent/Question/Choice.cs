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

        public QuestionView<Choice> BeginQuestion()
        {
            View = new QuestionView<Choice>().AddTo(this);
            View.Container = this;
            Queue.Add(View);
            return View;
        }

        public QuestionView<Choice> BeginQuestion(string title, string context, Action onYes, Action onNo)
        {
            View = new QuestionView<Choice>(title, context, onYes, onNo).AddTo(this);
            View.Container = this;
            Queue.Add(View);
            return View;
        }

        public ProcessSystem EndChoice()
        {
            Queue.System = System;
            return System;
        }

        public void OnProcess(Action onProcess)
        {
            Queue.Process(); //Show
            Queue.SetOnFinished(onProcess);
        }

        public Choice GetChoice(string key)
        {
            return System.GetChoice(key);
        }
    }
}