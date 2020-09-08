using System;
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
            //TODO:http://www.sikiedu.com/course/474/task/37227/show
            queue.Add(new QuestionView(question, onYes, onNo));
            return this;
        }
    }
}