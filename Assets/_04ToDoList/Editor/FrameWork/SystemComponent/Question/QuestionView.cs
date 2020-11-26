using System;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.Drawer;
using _04ToDoList.Editor.FrameWork.Layout;

namespace _04ToDoList.Editor.FrameWork.SystemComponent.Question
{
	public class QuestionView<T> : VerticalLayout, IQuestion<T> where T : IQuestionContainer
	{
		private LabelView titleLabel;
		private LabelView contextLabel;
		private TextAreaView contextAreaView;
		private FlexibleSpaceView spaceView;
		private List<ButtonView> btnViews;
		private List<ButtonView> choiceViews;


		private Action onNext;
		private Action<string> onChoice;


		public QuestionQueue Queue { get; set; }
		public T Container { private get; set; }


		public QuestionView(Action nextAct = null)
		{
			titleLabel = new LabelView(string.Empty).FontSize(25).TextMiddleCenter().AddTo(this);
			new SpaceView(10f).AddTo(this);
			contextLabel = new LabelView(string.Empty).FontSize(20).TextMiddleCenter().AddTo(this);
			contextAreaView = new TextAreaView(string.Empty).ExpandHeight(true).FontSize(15).AddTo(this);
			spaceView = new FlexibleSpaceView().AddTo(this);
			//new SpaceView(15f).AddTo(this);
			btnViews = new List<ButtonView>();
			choiceViews = new List<ButtonView>();
			AddNextAction(nextAct);

			contextLabel.Hide();
			contextAreaView.Hide();
		}


		public QuestionView(string titleText, string contextText, Action onYes, Action onNo, Action nextAct = null) :
			this(nextAct)
		{
			SetTitle(titleText);
			SetContext(contextText);
			var horizontalLayout = new HorizontalLayout().AddTo(this);
			//0 yes  1 no
			btnViews.Add(new ButtonView("是", onYes, true).AddTo(horizontalLayout));
			btnViews.Add(new ButtonView("否", onNo, true).AddTo(horizontalLayout));
		}


		public QuestionView<T> SetTitle(string text)
		{
			titleLabel.SetText(text);
			return this;
		}

		public QuestionView<T> SetContext(string text)
		{
			contextLabel.SetText(text);
			contextLabel.Show();
			return this;
		}

		public QuestionView<T> SetContextTextArea(string text, Action<string> act = null)
		{
			spaceView.Hide();
			contextAreaView.Content.Val = text;
			contextAreaView.Content.SetValueChanged(act);
			contextAreaView.Show();
			return this;
		}

		public QuestionView<T> NewBtn(string text, Action act = null)
		{
			var btn = new ButtonView(text, act, true).FontSize(25).TextMiddleCenter().AddTo(this);
			btnViews.Add(btn);
			if (onNext != null)
			{
				btn.OnClickEvent += onNext;
			}

			return this;
		}

		public QuestionView<T> NewChoice(string text, string dest, Action act = null)
		{
			var btn = new ButtonView(text, () =>
				{
					act?.Invoke();
					onChoice?.Invoke(dest);
				}, true).FontSize(25).TextMiddleCenter()
				.AddTo(this);
			choiceViews.Add(btn);
			// if (onNext != null)
			// {
			//     btn.OnClickEvent += onNext;
			// }

			return this;
		}

		public QuestionView<T> RepeatSelfMenu(string name, Action act)
		{
			new ButtonView(name, () =>
			{
				Hide();
				act?.Invoke();
				contextAreaView.Content.Val = string.Empty;
				EnqueueCmd(Show);
			}, true).FontSize(25).TextMiddleCenter().AddTo(this);
			return this;
		}

		public QuestionView<T> SetBtnText(int index, string text, bool isChoice = false)
		{
			var views = isChoice ? choiceViews : btnViews;
			views[index].Text = text;
			return this;
		}

		public QuestionView<T> SetBtnAct(int index, Action act, bool isChoice = false)
		{
			var views = isChoice ? choiceViews : btnViews;
			var btn = views[index];
			btn.OnClickEvent = act;
			if (onNext != null)
			{
				btn.OnClickEvent += onNext;
			}

			return this;
		}

		public QuestionView<T> SetBtn(int index, string text, Action act, bool isChoice = false)
		{
			var views = isChoice ? choiceViews : btnViews;
			var btn = views[index];
			btn.Text = text;
			btn.OnClickEvent = act;
			if (onNext != null)
			{
				btn.OnClickEvent += onNext;
			}

			return this;
		}


		public void Add(QuestionQueue queue)
		{
			queue.Add(this);
		}

		public QuestionView<T> AddNextAction(Action nextAct)
		{
			if (nextAct != null)
			{
				for (int i = 0; i < btnViews.Count; ++i)
				{
					var btn = btnViews[i];
					if (btn.OnClickEvent != null)
					{
						btn.OnClickEvent -= onNext;
					}
				}
			}

			onNext = nextAct;
			if (nextAct != null)
			{
				for (int i = 0; i < btnViews.Count; ++i)
				{
					var btn = btnViews[i];
					if (btn.OnClickEvent != null)
					{
						btn.OnClickEvent += onNext;
					}
				}
			}

			return this;
		}


		public T EndQuestion()
		{
			return Container;
		}

		void IQuestion.OnProcess(Action action)
		{
			AddNextAction(action);
		}

		void IQuestion.OnChoice(Action<string> choice)
		{
			onChoice = choice;
		}
	}
}