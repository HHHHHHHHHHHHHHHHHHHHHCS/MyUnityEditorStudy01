using System;

namespace _04ToDoList.Editor.FrameWork.DataBinding
{
	[System.Serializable]
	public class ToDoData
	{
		[Serializable]
		public enum ToDoState
		{
			NoStart,
			Started,
			Done,
		}

		[Serializable]
		public enum ToDoPriority
		{
			A = 0,
			B,
			C,
			D,
			None,
		}

		public string id = Guid.NewGuid().ToString();


		public string content;
		public Property<bool> finished;

		public DateTime createTime;
		public DateTime finishTime;
		public DateTime startTime;

		public Property<ToDoState> state;

		public Property<ToDoPriority> priority;

		public ToDoCategory category;

		public bool isHide;

		public string productVersionID;

		public TimeSpan UsedTime => finishTime - startTime;

		public string UsedTimeText => UsedTimeToString(UsedTime);


		public ToDoData Init(string content = null, Action saveAct = null
			, Property<bool> finished = null, Action<bool> finishedChangeAct = null
			, DateTime? createTime = null, DateTime? finishTime = null, DateTime? startTime = null
			, Property<ToDoState> state = null, Action<ToDoState> stateChangeAct = null
			, Property<ToDoPriority> priority = null, ToDoCategory category = null
			, bool? isHide = null, string productVersionID = null)
		{
			//this.id = Guid.NewGuid().ToString();
			this.content = content ?? string.Empty;
			this.finished = finished ?? new Property<bool>(false);
			if (finishedChangeAct != null)
			{
				this.finished.RegisterValueChanged(finishedChangeAct);
			}

			this.createTime = createTime ?? DateTime.Now;
			this.finishTime = finishTime ?? DateTime.Now;
			this.startTime = startTime ?? DateTime.Now;
			this.state = state ?? new Property<ToDoState>(ToDoState.NoStart);
			this.priority = priority ?? new Property<ToDoPriority>(ToDoPriority.None);
			this.category = category; //?? new TodoCategory();
			this.isHide = isHide ?? false;
			this.productVersionID = productVersionID;

			if (stateChangeAct != null)
			{
				this.state.RegisterValueChanged(stateChangeAct);
			}


			if (saveAct != null)
			{
				this.finished.RegisterValueChanged(_ => saveAct());
				this.state.RegisterValueChanged(_ => saveAct());
				this.priority.RegisterValueChanged(_ => saveAct());
				this.priority.RegisterValueChanged(_ => saveAct());
			}

			return this;
		}

		public ToDoData()
		{
			Init();
		}


		public ToDoData(string _content, bool _finished)
		{
			Init(content: _content, finished: new Property<bool>(_finished));
		}

		public ToDoData(string _content, bool _finished, Action _saveAct)
		{
			Init(content: _content, saveAct: _saveAct, finished: new Property<bool>(_finished));
		}

		public ToDoData(string _content, bool _finished, Action _saveAct, ToDoCategory _category,
			string _productVersionID)
		{
			Init(content: _content, saveAct: _saveAct, finished: new Property<bool>(_finished), category: _category,
				productVersionID: _productVersionID);
		}

		public override string ToString()
		{
			return
				$"{nameof(id)}:{id} , {nameof(content)}: {content}, {nameof(finished)}: {finished}, {nameof(createTime)}: {createTime}, {nameof(finishTime)}: {finishTime}, {nameof(startTime)}: {startTime}, {nameof(state)}: {state}";
		}

		public static string UsedTimeToString(TimeSpan usedTime)
		{
			if (usedTime.TotalSeconds < 60)
			{
				return $"花费 {usedTime.Seconds} 秒";
			}
			else if (usedTime.TotalMinutes < 60)
			{
				return $"花费 {usedTime.Minutes} 分钟";
			}
			else if (usedTime.TotalHours < 24)
			{
				return $"花费 {usedTime.Hours} 小时";
			}
			else if (usedTime.TotalDays < 7)
			{
				return $"花费 {usedTime.Days} 天";
			}

			return $"花费 {usedTime.TotalDays / 7} 周";
		}
	}
}