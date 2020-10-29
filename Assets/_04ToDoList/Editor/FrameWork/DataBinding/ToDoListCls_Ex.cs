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


    public static void AddToDoItem(this ToDoListCls cls, string content, bool finished, TodoCategory category)
    {
        var data = new ToDoData(content, finished, cls.Save, category);
        cls.AddToDoItem(data);
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

    public static void AddToDoCategory(this ToDoListCls cls, string name, string color)
    {
        cls.AddToDoCategory(new TodoCategory(name, color));
    }

    public static void AddToDoCategory(this ToDoListCls cls, TodoCategory category)
    {
        cls.categoryList.Add(category);
        cls.Save();
    }

    public static TodoCategory ToDoCategoryAt(this ToDoListCls cls, int index)
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

    public static int ToDoCategoryIndexOf(this ToDoListCls cls, TodoCategory category)
    {
        if (category == null)
            return -1;
        return cls.categoryList.IndexOf(category);
    }

    public static void RemoveToDoCategory(this ToDoListCls cls, TodoCategory category)
    {
        cls.categoryList.Remove(category);
        cls.Save();
    }

    public static void AddToDoNote(this ToDoListCls cls, string note)
    {
        cls.AddToDoNote(new ToDoNote(note));
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

    public static void ConvertToDoNote(this ToDoListCls cls, ToDoNote note, bool isHide)
    {
        cls.noteList.Remove(note);
        //AddToDoItem  里面有save
        var data = new ToDoData().Init(content: note.content, finished: new Property<bool>(false), isHide: isHide);
        cls.AddToDoItem(data);
    }
}