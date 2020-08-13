using _04ToDoList.Editor.FrameWork.DataBinding;
using _04ToDoList.Util;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class EventsTest
    {
        public enum TestEvents
        {
            A,
            B,
            C,
            D,
        }

        [Test]
        public void TestEvent01()
        {
            EventDispatcher.Register(TestEvents.A, () =>
            {
                Debug.Log(1);
            });

            EventDispatcher.Send(TestEvents.A);

            EventDispatcher.RemoveAll(TestEvents.A);

            EventDispatcher.Send(TestEvents.A);

        }
    }
}