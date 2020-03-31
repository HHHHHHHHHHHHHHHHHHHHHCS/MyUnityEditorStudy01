using _04TodoList.Util;
using NUnit.Framework;
using UnityEngine;
using Newtonsoft.Json;

namespace Tests
{
    public class Playground
    {
        [Test]
        public void PlaygroundSimplePasses()
        {
            var p = new Property<int>(10);
            var ser = JsonConvert.SerializeObject(p);
            Debug.Log(ser);
            var deser = JsonConvert.DeserializeObject<Property<int>>(ser);
            Assert.IsTrue(p.Val == 10);
        }
    }
}