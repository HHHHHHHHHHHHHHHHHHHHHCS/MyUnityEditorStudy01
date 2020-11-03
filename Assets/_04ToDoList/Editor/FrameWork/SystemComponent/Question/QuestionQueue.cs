using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.Layout;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.SystemComponent.Question
{
	public class QuestionQueue : VerticalLayout, IQuestionContainer
	{
		public ProcessSystem System { private get; set; }

		private Queue<IQuestion> queueViews = new Queue<IQuestion>();
		private IQuestion current;

		private Action onFinished;

		public void Add(IQuestion view)
		{
			view.AddTo(this);
			view.Hide();
			view.OnProcess(Next);
			view.OnChoice(key =>
			{
				var choice = GetChoice(key);
				choice.OnProcess(Next);
				current?.Hide();
				choice.Show();
			});
			queueViews.Enqueue(view);
		}

		public void Process()
		{
			Next();
		}

		public void Next()
		{
			current?.Hide();

			if (queueViews.Count == 0)
			{
				onFinished?.Invoke();
				current = null;
				return;
			}

			current = queueViews.Dequeue();
			current.Show();
		}

		public void SetOnFinished(Action onAct)
		{
			onFinished = onAct;
		}

		public Choice GetChoice(string key) => System.GetChoice(key);

		public Choice AddChoice(string key)
		{
			var choice = System.GetChoice(key);
			if (choice == null)
			{
				choice = new Choice(key).AddTo(this);
				System.AddChoice(key, choice);
			}
			return choice;
		}
	}
}