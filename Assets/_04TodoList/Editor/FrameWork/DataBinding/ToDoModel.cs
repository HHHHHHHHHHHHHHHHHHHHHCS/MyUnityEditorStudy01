using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace _04TodoList.Editor.FrameWork.DataBinding
{
    public class TodoListBaseCls
    {
        [System.NonSerialized] public const string todosKey = "TodoList_TODOS";
        [System.NonSerialized] public const string todosVersionKey = "TodoList_TODOS_Version";

        [System.NonSerialized] public const int version = -1;
    }

    [System.Serializable]
    public class TodoListCls : TodoListBaseCls
    {
        [System.NonSerialized] public new const int version = 2;

        public static TodoListCls Load()
        {
            var version = EditorPrefs.GetInt(todosVersionKey, -1);

            if (version == -1)
            {
                return new TodoListCls();
            }

            var todoContext = EditorPrefs.GetString(todosKey, string.Empty);

            try
            {
                if (string.IsNullOrEmpty(todoContext))
                {
                    return new TodoListCls();
                }
                else
                {
                    try
                    {
                        if (version == 0)
                        {
                            var deprecated = JsonConvert.DeserializeObject<Deprecated0.TodoListCls>(todoContext);
                            Debug.Log("数据版本不一致,进行升级!");
                            if (deprecated != null && deprecated.todoList.Count > 0)
                            {
                                var todoList = deprecated.todoList.Select(todo => new TODOData(todo, false)).ToList();
                                var cls = new TodoListCls();
                                cls.todoList = todoList;
                                cls.Save();
                                return cls;
                            }
                            else
                            {
                                return new TodoListCls();
                            }
                        }
                        else if (version == 1)
                        {
                            var deprecated = JsonConvert.DeserializeObject<Deprecated1.TodoListCls>(todoContext);
                            Debug.Log("数据版本不一致,进行升级!");
                            if (deprecated != null && deprecated.todoList.Count > 0)
                            {
                                var cls = new TodoListCls
                                {
                                    todoList = deprecated.todoList
                                        .Select(todo => new TODOData(todo.content, todo.finished))
                                        .ToList()
                                };
                                cls.Save();
                                return cls;
                            }
                            else
                            {
                                return new TodoListCls();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }

                    return JsonConvert.DeserializeObject<TodoListCls>(todoContext);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.StackTrace);
            }

            return new TodoListCls();
        }

        public static void Save(TodoListCls cls)
        {
            if (cls == null)
            {
                Debug.LogError("TodoListCls is null!");
                return;
            }

            EditorPrefs.SetInt(todosVersionKey, version);
            EditorPrefs.SetString(todosKey, JsonConvert.SerializeObject(cls));
        }

        public List<TODOData> todoList = new List<TODOData>();
    }

    public static class TodoListCls_Ex
    {
        public static void Save(this TodoListCls cls)
        {
            TodoListCls.Save(cls);
        }

        public static void Add(this TodoListCls cls, string content, bool finished)
        {
            var data = new TODOData(content, finished, cls.Save);
            cls.todoList.Add(data);
            data.finished.RegisterValueChanged(cls.Save);
            cls.Save();
        }
    }

    [System.Serializable]
    public class TODOData
    {
        public string content;

        public Property<bool> finished;


        public TODOData()
        {
            this.content = string.Empty;
            this.finished = new Property<bool>(false);
        }

        public TODOData(string _content, bool _finished)
        {
            this.content = _content;
            this.finished = new Property<bool>(_finished);
        }

        public TODOData(string _content, bool _finished, Action _act)
        {
            this.content = _content;
            this.finished = new Property<bool>(_finished);
            this.finished.SetValueChanged(_act);
        }
    }
}

namespace _04TodoList.Editor.FrameWork.DataBinding.Deprecated0
{
    [Obsolete]
    [System.Serializable]
    public class TodoListCls : TodoListBaseCls
    {
        [System.NonSerialized] public new const int version = 0;

        public List<string> todoList = new List<string>();
    }
}

namespace _04TodoList.Editor.FrameWork.DataBinding.Deprecated1
{
    [System.Serializable]
    public class TODOData
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

        public TODOData(string _content, bool _finished)
        {
            this.content = _content;
            this.finished = _finished;
            this.finishedChanged = false;
        }
    }

    [Obsolete]
    [System.Serializable]
    public class TodoListCls : TodoListBaseCls
    {
        [System.NonSerialized] public new const int version = 1;

        public List<TODOData> todoList = new List<TODOData>();
    }
}