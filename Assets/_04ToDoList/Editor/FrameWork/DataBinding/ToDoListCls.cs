using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.DataBinding
{
	[System.Serializable]
	public partial class ToDoListCls : ToDoListBaseCls
	{
		public new const int version = 3;

		public List<ToDoData> todoList = new List<ToDoData>();

		public List<ToDoNote> noteList = new List<ToDoNote>();

		public List<ToDoCategory> categoryList = new List<ToDoCategory>();
	}

	public partial class ToDoListCls : ToDoListBaseCls
	{
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
}