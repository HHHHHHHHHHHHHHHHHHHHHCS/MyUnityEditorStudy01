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
            EventDispatcher.Register((int) TestEvents.A, (obj) => { Debug.Log(1); });

            EventDispatcher.Send((int) TestEvents.A, this);

            EventDispatcher.RemoveAll((int) TestEvents.A);

            EventDispatcher.Send((int) TestEvents.A, this);
        }
    }
}