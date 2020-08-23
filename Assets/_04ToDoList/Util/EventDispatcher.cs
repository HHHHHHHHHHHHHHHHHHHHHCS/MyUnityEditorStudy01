using System;
using System.Collections.Generic;
using UnityEngine;

namespace _04ToDoList.Util
{
    public class EventRecord
    {
        public int key;

        public Action<object> onAction;
    }

    public class EventDispatcher
    {
        private static readonly Dictionary<int, Action<object>>
            registeredEvents = new Dictionary<int, Action<object>>();

        public static void Register(int key, Action<object> _onEvent)
        {
            //delegate 只能这样写  因为可能是值类型
            int intKey = key;
            if (!registeredEvents.ContainsKey(intKey))
            {
                Action<object> act = _onEvent; //拷贝一份
                registeredEvents.Add(intKey, act);
            }
            else
            {
                registeredEvents[intKey] += _onEvent;
            }
        }

        public static void Remove(int key, Action<object> _onEvent)
        {
            if (registeredEvents.ContainsKey(key))
            {
                if (registeredEvents[key] != null)
                {
                    registeredEvents[key] -= _onEvent;
                }
            }
        }

        public static void RemoveAll<T>(T key) where T : IConvertible
        {
            int intKey = key.ToInt32(null);
            if (registeredEvents.TryGetValue(intKey, out var acts))
            {
                //也可以这样获取全部的方法
                //registeredEvents[intKey].GetInvocationList()
                registeredEvents[intKey] -= acts;
            }
        }

        public static void Send(int key, object obj = null)
        {
            if (registeredEvents.TryGetValue(key, out var acts))
            {
                acts?.Invoke(obj);
            }
        }
    }
}