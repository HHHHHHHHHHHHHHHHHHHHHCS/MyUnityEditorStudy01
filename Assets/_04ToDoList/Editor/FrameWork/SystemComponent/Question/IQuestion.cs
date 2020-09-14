using System;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;

namespace _04ToDoList.Editor.FrameWork.SystemComponent.Question
{
    public interface IQuestion : IView
    {
        QuestionQueue Queue { get; set; }

        ProcessSystem System {  set; }

        ProcessSystem EndQuestion();

        void OnProcess(Action onProcess);
    }
}