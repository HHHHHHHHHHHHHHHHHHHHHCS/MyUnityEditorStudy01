﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace _04ToDoList.Util
{
    public class EventDispatcher
    {
        private static Dictionary<int, Action> registeredEvents = new Dictionary<int, Action>();

        public static void Register<T>(T key, Action _onEvent) where T : IConvertible
        {
            //delegate 只能这样写  因为可能是值类型
            int intKey = key.ToInt32(null);
            if (!registeredEvents.ContainsKey(intKey))
            {
                Action act = _onEvent;
                registeredEvents.Add(intKey, act);
            }
            else
            {
                registeredEvents[intKey] += _onEvent;
            }
        }

        public static void Remove<T>(T key, Action _onEvent) where T : IConvertible
        {
            int intKey = key.ToInt32(null);
            if (registeredEvents.ContainsKey(intKey))
            {
                registeredEvents[intKey] -= _onEvent;
            }
        }

        public static void RemoveAll<T>(T key) where T : IConvertible
        {
            int intKey = key.ToInt32(null);
            if (registeredEvents.TryGetValue(intKey, out var acts))
            {
                //也可以这样获取全部的方法
                //registeredEvents[intKey].GetInvocationList()
                registeredEvents[intKey]-= acts;
            }
        }

        public static void Send<T>(T key) where T : IConvertible
        {
            int intKey = key.ToInt32(null);
            if (registeredEvents.TryGetValue(intKey, out var acts))
            {
                acts?.Invoke();
            }
        }
    }
}