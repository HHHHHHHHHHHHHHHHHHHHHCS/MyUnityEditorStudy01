using System;
using System.Collections.Generic;
using UnityEngine;

namespace _04ToDoList.Util
{
    public class EventDispatcher
    {
        private static Dictionary<int, Action> registeredEvents = new Dictionary<int, Action>();

        public static void Register<T>(Action _onEvent) where T : IConvertible
        {
            //delegate 只能这样写
            int intKey = default(T).ToInt32(null);
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

        public static void Register<T>(T type, Action _onEvent) where T : IConvertible
        {
            int intKey = type.ToInt32(null);
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

        public static void Remove<T>(Action _onEvent) where T : IConvertible
        {
            int intKey = default(T).ToInt32(null);
            if (registeredEvents.ContainsKey(intKey))
            {
                registeredEvents[intKey] -= _onEvent;
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
    }