using System;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.DataBinding
{
    public class ToDoVersion
    {
        public readonly int major;
        public readonly int middle;
        public readonly int small;

        public ToDoVersion(int _major, int _middle, int _small)
        {
            major = _major;
            middle = _middle;
            small = _small;
        }

        public override string ToString()
        {
            return $"v{major}.{middle}.{small}";
        }
    }
    
    public class ToDoProductVersion
    {
        public string Id = Guid.NewGuid().ToString();

        public string name;

        public ToDoVersion version;
    }
}
