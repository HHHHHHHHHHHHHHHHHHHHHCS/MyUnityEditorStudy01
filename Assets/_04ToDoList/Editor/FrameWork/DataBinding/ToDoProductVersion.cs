using System;
using System.Collections.Generic;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.DataBinding
{
	[Serializable]
	public class ToDoVersion
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

		public override string ToString()
		{
			return $"v{major}.{middle}.{small}";
		}
	}

	[Serializable]
	public class ToDoProductVersion
	{
		public string Id = Guid.NewGuid().ToString();

		public string name;

		public ToDoVersion version;

		public List<ToDoNote> notes = new List<ToDoNote>();

		public List<ToDoData> todos = new List<ToDoData>();
	}

	[Serializable]
	public class Feature
	{
		public string name;

		public List<ToDoNote> notes = new List<ToDoNote>();
	}


	[Serializable]
	public class Product
	{
		public string name;

		public string description;

		public List<Feature> features = new List<Feature>();

		public List<ToDoProductVersion> versions = new List<ToDoProductVersion>();

		public Product(string _name, string _description)
		{
			this.name = _name;
			this.description = _description;
		}
	}
}