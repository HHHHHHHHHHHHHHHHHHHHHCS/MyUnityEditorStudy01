using System;
using System.Collections.Generic;
using System.Linq;
using _04ToDoList.Editor.FrameWork.Utils;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;


namespace _04ToDoList.Editor.FrameWork.DataBinding.Deprecated0
{
    [Obsolete]
    [System.Serializable]
    public class ToDoListCls : ToDoListBaseCls
    {
        public new const int version = 0;

        public List<string> todoList = new List<string>();
    }
}

namespace _04ToDoList.Editor.FrameWork.DataBinding.Deprecated1
{
    [System.Serializable]
    public class ToDoData
    {
        public string content;

        public bool finished;

        [NonSerialized] public bool finishedChanged;

        public bool finishedValue
        {
            get => finished;
            set
            {
                if (finished != value)
                {
                    finished = value;
                    finishedChanged = true;
                }
            }
        }

        public ToDoData(string _content, bool _finished)
        {
            this.content = _content;
            this.finished = _finished;
            this.finishedChanged = false;
        }
    }

    [Obsolete]
    [System.Serializable]
    public class ToDoListCls : ToDoListBaseCls
    {
        public new const int version = 1;

        public List<ToDoData> todoList = new List<ToDoData>();
    }
}

namespace _04ToDoList.Editor.FrameWork.DataBinding.Deprecated2
{
    [System.Serializable]
    public class ToDoData
    {
        public string content;

        public Property<bool> finished;


        public ToDoData()
        {
            this.content = string.Empty;
            this.finished = new Property<bool>(false);
        }

        public ToDoData(string _content, bool _finished)
        {
            this.content = _content;
            this.finished = new Property<bool>(_finished);
        }

        public ToDoData(string _content, bool _finished, Action<bool> _act)
        {
            this.content = _content;
            this.finished = new Property<bool>(_finished);
            this.finished.SetValueChanged(_act);
        }
    }

    [Obsolete]
    [System.Serializable]
    public class ToDoListCls : ToDoListBaseCls
    {
        public new const int version = 2;

        public List<ToDoData> modelData = new List<ToDoData>();
    }
}