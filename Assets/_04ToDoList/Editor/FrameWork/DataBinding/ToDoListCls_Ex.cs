using System.Collections;
using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.DataBinding;
using UnityEngine;

public static class ToDoListCls_Ex
{
	public static void Save(this ToDoListCls cls)
	{
		ToDoListCls.Save(cls);
	}

	public static ToDoData CreateToDoItem(this ToDoListCls cls, string content, bool finished, ToDoCategory category,string productVersionID )
	{
		var data = new ToDoData(content, finished, cls.Save, category,productVersionID);
		return data;
	}

	public static ToDoData AddToDoItem(this ToDoListCls cls, string content, bool finished, ToDoCategory category,string productVersionID )
	{
		var data = cls.CreateToDoItem(content, finished, category,productVersionID);
		cls.AddToDoItem(data);
		return data;
	}

	public static void AddToDoItem(this ToDoListCls cls, ToDoData data)
	{
		cls.todoList.Add(data);
		cls.Save();
	}

	public static void RemoveToDoItem(this ToDoListCls cls, int index)
	{
		cls.todoList.RemoveAt(index);
		cls.Save();
	}

	public static void RemoveToDoItem(this ToDoListCls cls, ToDoData data)
	{
		cls.todoList.Remove(data);
		cls.Save();
	}

	public static ToDoCategory AddToDoCategory(this ToDoListCls cls, string name, string color)
	{
		var val = new ToDoCategory(name, color);
		cls.AddToDoCategory(val);
		return val;
	}

	public static void AddToDoCategory(this ToDoListCls cls, ToDoCategory category)
	{
		cls.categoryList.Add(category);
		cls.Save();
	}

	public static ToDoCategory ToDoCategoryAt(this ToDoListCls cls, int index)
	{
		if (index >= 0 && index < cls.categoryList.Count)
		{
			return cls.categoryList[index];
		}
		else
		{
			return null;
		}
	}

	public static int ToDoCategoryIndexOf(this ToDoListCls cls, ToDoCategory category)
	{
		if (category == null)
			return -1;
		return cls.categoryList.IndexOf(category);
	}

	public static void RemoveToDoCategory(this ToDoListCls cls, ToDoCategory category)
	{
		cls.categoryList.Remove(category);
		cls.Save();
	}

	public static ToDoNote AddToDoNote(this ToDoListCls cls, string note)
	{
		var val = new ToDoNote(note);
		cls.AddToDoNote(val);
		return val;
	}

	public static void AddToDoNote(this ToDoListCls cls, ToDoNote note)
	{
		cls.noteList.Add(note);
		cls.Save();
	}

	public static void RemoveToDoNote(this ToDoListCls cls, ToDoNote note)
	{
		cls.noteList.Remove(note);
		cls.Save();
	}

	public static ToDoData ConvertToDoNote(this ToDoListCls cls, ToDoNote note, bool isHide)
	{
		cls.noteList.Remove(note);
		//AddToDoItem  里面有save
		var data = new ToDoData().Init(content: note.content, finished: new Property<bool>(false), isHide: isHide);
		cls.AddToDoItem(data);
		return data;
	}

	public static ToDoProduct AddProduct(this ToDoListCls cls, string name, string desc)
	{
		var val = new ToDoProduct(name, desc);
		cls.productList.Add(val);
		cls.Save();
		return val;
	}

	public static void RemoveProduct(this ToDoListCls cls, ToDoProduct todoProduct)
	{
		cls.productList.Remove(todoProduct);
		cls.Save();
	}

	public static void AddProductToDoItem(this ToDoListCls cls, ToDoProductVersion productVersion, ToDoData data)
	{
		productVersion.todoIDs.Add(data.id);
		productVersion.todos.Add(data);
		cls.todoList.Add(data);
		cls.Save();
	}
	
	public  static void RemoveProductToDoItem(this ToDoListCls cls, ToDoProductVersion productVersion, ToDoData data)
	{
		productVersion.todoIDs.Remove(data.id);
		productVersion.todos.Remove(data);
		cls.todoList.Remove(data);
		cls.Save();
	}
}