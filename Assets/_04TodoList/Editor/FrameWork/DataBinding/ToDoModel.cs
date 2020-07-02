using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.DataBinding
{
    public class ToDoListBaseCls
    {
        [System.NonSerialized] public const string todosKey = "ToDoList_TODOS";
        [System.NonSerialized] public const string todosVersionKey = "ToDoList_TODOS_Version";

        [System.NonSerialized] public const int version = -1;
    }

    [System.Serializable]
    public class ToDoListCls : ToDoListBaseCls
    {
        public new const int version = 3;

        [System.NonSerialized] private static ToDoListCls _modelData;
        public static ToDoListCls ModelData => _modelData ?? (_modelData = Load());

        public static ToDoListCls Load()
        {
            var version = EditorPrefs.GetInt(todosVersionKey, -1);

            if (version == -1)
            {
                return new ToDoListCls();
            }

            var todoContext = EditorPrefs.GetString(todosKey, string.Empty);

            try
            {
                if (string.IsNullOrEmpty(todoContext))
                {
                    return new ToDoListCls();
                }
                else
                {
                    try
                    {
                        if (version == 0)
                        {
                            ToDoListCls cls;
                            var deprecated = JsonConvert.DeserializeObject<Deprecated0.ToDoListCls>(todoContext);
                            Debug.Log("数据版本不一致,进行升级!");
                            if (deprecated != null && deprecated.todoList.Count > 0)
                            {
                                var todoList = deprecated.todoList.Select(todo => new ToDoData(todo, false)).ToList();
                                cls = new ToDoListCls {todoList = todoList};
                            }
                            else
                            {
                                cls = new ToDoListCls();
                            }

                            cls.Save();
                            return cls;
                        }
                        else if (version == 1)
                        {
                            ToDoListCls cls;
                            var deprecated = JsonConvert.DeserializeObject<Deprecated1.ToDoListCls>(todoContext);
                            Debug.Log("数据版本不一致,进行升级!");
                            if (deprecated != null && deprecated.todoList.Count > 0)
                            {
                                cls = new ToDoListCls
                                {
                                    todoList = deprecated.todoList
                                        .Select(todo => new ToDoData(todo.content, todo.finished))
                                        .ToList()
                                };
                                cls.Save();
                            }
                            else
                            {
                                cls = new ToDoListCls();
                            }

                            cls.Save();
                            return cls;
                        }
                        else if (version == 2)
                        {
                            ToDoListCls cls;
                            var deprecated = JsonConvert.DeserializeObject<Deprecated2.ToDoListCls>(todoContext);
                            Debug.Log("数据版本不一致,进行升级!");
                            var helper = new ToDoModelUpgradeActionV1();
                            cls = helper.Execute(deprecated);
                            cls.Save();
                            return cls;
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }

                    return JsonConvert.DeserializeObject<ToDoListCls>(todoContext);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.StackTrace);
            }

            return new ToDoListCls();
        }

        public static void Save(ToDoListCls cls)
        {
            if (cls == null)
            {
                Debug.LogError("ToDoListCls is null!");
                return;
            }

            EditorPrefs.SetInt(todosVersionKey, version);
            EditorPrefs.SetString(todosKey, JsonConvert.SerializeObject(cls));
        }

        public List<ToDoData> todoList = new List<ToDoData>();

        public ToDoData this[int index]
        {
            get
            {
                if (index >= 0 && index < todoList.Count)
                {
                    return todoList[index];
                }

                Debug.LogError("Index out of array!");
                return null;
            }

            set
            {
                if (index >= 0 && index < todoList.Count)
                {
                    todoList[index] = value;
                }

                Debug.LogError("Index out of array!");
            }
        }
    }

    public static class ToDoListCls_Ex
    {
        public static void Save(this ToDoListCls cls)
        {
            ToDoListCls.Save(cls);
        }

        public static void Add(this ToDoListCls cls, string content, bool finished)
        {
            var data = new ToDoData(content, finished, cls.Save);
            cls.todoList.Add(data);
            cls.Save();
        }
    }

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

        public enum Priority
        {
            A,
            B,
            C,
            D,
            None,
        }

        public string content;
        public Property<bool> finished;

        public DateTime createTime;
        public DateTime finishTime;
        public DateTime startTime;

        public Property<ToDoState> state;

        public TimeSpan UsedTime => finishTime - startTime;

        public string UsedTimeText => UsedTimeToString(UsedTime);

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

        public void Init(string content = null, Action saveAct = null
            , Property<bool> finished = null, Action<bool> finishedChangeAct = null
            , DateTime? createTime = null, DateTime? finishTime = null, DateTime? startTime = null
            , Property<ToDoState> state = null, Action<ToDoState> stateChangeAct = null)
        {
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
            if (stateChangeAct != null)
            {
                this.state.RegisterValueChanged(stateChangeAct);
            }


            if (saveAct != null)
            {
                this.finished.RegisterValueChanged(_ => saveAct());
                this.state.RegisterValueChanged(_ => saveAct());
            }
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

        public override string ToString()
        {
            return
                $"{nameof(content)}: {content}, {nameof(finished)}: {finished}, {nameof(createTime)}: {createTime}, {nameof(finishTime)}: {finishTime}, {nameof(startTime)}: {startTime}, {nameof(state)}: {state}";
        }
    }
}

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