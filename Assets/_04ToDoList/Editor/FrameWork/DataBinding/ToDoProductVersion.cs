using System;
using System.Collections.Generic;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.DataBinding
{
	public class ToDoVersion
	{
		public readonly int major;
		public readonly int middle;
		public readonly int small;

		public ToDoVersion(int _major, int _middle, int _small)
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

	public class ToDoProductVersion
	{
		public string Id = Guid.NewGuid().ToString();

		public string name;

		public ToDoVersion version;

		public List<ToDoNote> notes = new List<ToDoNote>();

		public List<ToDoData> todos = new List<ToDoData>();
	}

	public class Feature
	{
		public string name;

		public List<ToDoNote> notes = new List<ToDoNote>();
	}

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