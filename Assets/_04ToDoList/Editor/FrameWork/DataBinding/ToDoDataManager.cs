﻿using System.Linq;

namespace _04ToDoList.Editor.FrameWork.DataBinding
{
	public static class ToDoDataManager
	{
		public static ToDoListCls Data => ToDoListCls.ModelData;

		public static void Save() => Data.Save();

		public static ToDoData CreateToDoItem(string content, bool finished = false, ToDoCategory category = null,
			string productVersionID = null) =>
			Data.CreateToDoItem(content, finished, category, productVersionID);

		public static ToDoData AddToDoItem(string content, bool finished = false, ToDoCategory category = null,
			string productVersionID = null) =>
			Data.AddToDoItem(content, finished, category, productVersionID);

		public static void AddToDoItem(ToDoData data) =>
			Data.AddToDoItem(data);

		public static void RemoveToDoItem(int index) =>
			Data.RemoveToDoItem(index);

		public static void RemoveToDoItem(ToDoData data) =>
			Data.RemoveToDoItem(data);

		public static ToDoData GetToDoItemByID(string id) => Data.todoList.First(x => x.id == id);

		public static ToDoCategory AddToDoCategory(string name, string color) => Data.AddToDoCategory(name, color);

		public static void AddToDoCategory(ToDoCategory category) => Data.AddToDoCategory(category);

		public static ToDoCategory ToDoCategoryAt(int index) => Data.ToDoCategoryAt(index);

		public static int ToDoCategoryIndexOf(ToDoCategory category) => Data.ToDoCategoryIndexOf(category);

		public static void RemoveToDoCategory(ToDoCategory category) => Data.RemoveToDoCategory(category);

		public static ToDoNote AddToDoNote(string note) => Data.AddToDoNote(note);

		public static void AddToDoNote(ToDoNote note) => Data.AddToDoNote(note);

		public static void RemoveToDoNote(ToDoNote note) => Data.RemoveToDoNote(note);

		public static ToDoData ConvertToDoNote(ToDoNote note, bool isHide) => Data.ConvertToDoNote(note, isHide);

		public static void AddProduct(string name, string desc) => Data.AddProduct(name, desc);

		public static void AddProductToDoItem(ToDoProductVersion productVersion, ToDoData data) =>
			Data.AddProductToDoItem(productVersion, data);

		public static void RemoveProductToDoItem(ToDoProductVersion productVersion, ToDoData data) =>
			Data.RemoveProductToDoItem(productVersion, data);
	}
}