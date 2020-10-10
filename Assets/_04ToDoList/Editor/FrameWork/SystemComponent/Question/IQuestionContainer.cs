using System;

namespace _04ToDoList.Editor.FrameWork.SystemComponent.Question
{
    public interface IQuestionContainer
    {
        Choice GetChoice(string key);
    }

    public interface IQuestionContainer<T> : IQuestionContainer where T : IQuestionContainer
    {
        QuestionView<T> BeginQuestion();

        QuestionView<T> BeginQuestion(string title, string context, Action onYes, Action onNo);
    }
}