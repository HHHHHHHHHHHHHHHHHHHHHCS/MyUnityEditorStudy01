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
            if (!registeredEvents.ContainsKey(key))
            {
                Action<object> act = _onEvent; //拷贝一份
                registeredEvents.Add(key, act);
            }
            else
            {
                registeredEvents[key] += _onEvent;
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

        public static void RemoveAll(int key) 
        {
            registeredEvents.Remove(key);
        }

        public static void Clear()
        {
            registeredEvents.Clear();
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