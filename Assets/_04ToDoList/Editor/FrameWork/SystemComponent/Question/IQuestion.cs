using System;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;

namespace _04ToDoList.Editor.FrameWork.SystemComponent.Question
{
    public interface IQuestion:IView
    {
        void OnProcess(Action onProcess);
    }
}