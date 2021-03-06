﻿using System;
using System.Collections.Generic;
using System.Linq;
using _04ToDoList.Editor.FrameWork.ViewGUI;
using Newtonsoft.Json;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.DataBinding
{
	[Serializable]
	public class ToDoVersion : IComparable<ToDoVersion>
	{
		public int Major
		{
			get => major;
			set => major = value > 0 ? value : 0;
		}

		public int Middle
		{
			get => middle;
			set => middle = value > 0 ? value : 0;
		}

		public int Small
		{
			get => small;
			set => small = value > 0 ? value : 0;
		}

		private int major;
		private int middle;
		private int small;

		public ToDoVersion(int _major, int _middle, int _small)
		{
			SetVersion(_major, _middle, _small);
		}

		public void SetVersion(int _major, int _middle, int _small)
		{
			major = _major;
			middle = _middle;
			small = _small;
		}

		public void SetVersion(ToDoVersion _version)
		{
			major = _version.major;
			middle = _version.middle;
			small = _version.small;
		}

		public int CompareTo(ToDoVersion other)
		{
			if (this.major > other.major)
			{
				return 1;
			}
			else if (this.major < other.major)
			{
				return -1;
			}

			if (this.middle > other.middle)
			{
				return 1;
			}
			else if (this.middle < other.middle)
			{
				return -1;
			}


			if (this.small > other.small)
			{
				return 1;
			}
			else if (this.small < other.small)
			{
				return -1;
			}

			return 0;
		}

		public override string ToString()
		{
			return $"v{major}.{middle}.{small}";
		}
	}

	[Serializable]
	public class ToDoProductVersion
	{
		public string id = Guid.NewGuid().ToString();

		public string name;

		public ToDoVersion version;

		public List<ToDoNote> notes = new List<ToDoNote>();

		public List<string> todoIDs = new List<string>();


		public ToDoData.ToDoState GetStates()
		{
			if (todos.Count == 0)
			{
				return ToDoData.ToDoState.NoStart;
			}

			foreach (var item in todos)
			{
				if (item.state.Val == ToDoData.ToDoState.Started || item.state.Val == ToDoData.ToDoState.NoStart)
				{
					return ToDoData.ToDoState.Started;
				}
			}

			return ToDoData.ToDoState.Done;
		}

		[System.NonSerialized, JsonIgnore] private List<ToDoData> _todos;

		[JsonIgnore]
		public List<ToDoData> todos
		{
			get
			{
				if (_todos == null)
				{
					_todos = new List<ToDoData>(todoIDs.Count);
					foreach (var id in todoIDs)
					{
						_todos.Add(ToDoDataManager.GetToDoItemByID(id));
					}
				}

				return _todos;
			}
		}

		public void ToDoOrderBy<T>(Func<ToDoData, T> compare)
		{
			_todos = todos.OrderBy(compare).ToList();
		}
	}

	[Serializable]
	public class ToDoFeature
	{
		public string name;

		public string description;

		public List<ToDoFeature> childFeatures = new List<ToDoFeature>();

		// public List<ToDoNote> notes = new List<ToDoNote>();

		public ToDoFeature()
		{
		}

		public ToDoFeature(string _name, string _description)
		{
			this.name = _name;
			this.description = _description;
		}
	}

	[Serializable]
	public class ToDoProduct
	{
		public string name;

		public string description;

		public List<ToDoFeature> features = new List<ToDoFeature>();

		public List<ToDoProductVersion> versions = new List<ToDoProductVersion>();

		public ToDoProduct(string _name, string _description)
		{
			this.name = _name;
			this.description = _description;
		}
	}
}