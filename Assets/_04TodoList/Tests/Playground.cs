using _04ToDoList.Editor.FrameWork.DataBinding;
using NUnit.Framework;
using UnityEngine;
using Newtonsoft.Json;

namespace Tests
{
    public class Playground
    {
        [Test]
        public void TestJson()
        {
            var p = new Property<int>(10);
            var ser = JsonConvert.SerializeObject(p);
            Debug.Log(ser);
            var deser = JsonConvert.DeserializeObject<Property<int>>(ser);
            Assert.IsTrue(p.Val == 10);
        }

        [Test]
        public void LoadData()
        {
            var todoList = ToDoListCls.Load();
            Debug.Log("Data:");
            //Formatting.Indented 输出更加格式化
            Debug.Log(JsonConvert.SerializeObject(todoList, Formatting.Indented));
        }
    }
}