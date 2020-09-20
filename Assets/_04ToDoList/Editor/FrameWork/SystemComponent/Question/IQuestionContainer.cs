using System;

namespace _04ToDoList.Editor.FrameWork.SystemComponent.Question
{
    public interface IQuestionContainer
    {
    }

    public interface IQuestionContainer<T> : IQuestionContainer where T : IQuestionContainer
    {
        QuestionView<T> BeginQuestion(string question = null, Action onYes = null, Action onNo = null);
    }
}