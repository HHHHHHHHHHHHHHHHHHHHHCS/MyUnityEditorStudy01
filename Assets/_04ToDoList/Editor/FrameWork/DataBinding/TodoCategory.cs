﻿using System;

namespace _04ToDoList.Editor.FrameWork.DataBinding
{
    [System.Serializable]
    public class ToDoCategory
    {
        public string id = Guid.NewGuid().ToString();

        public string name;

        //因为正常的Color 没有序列化  所以用了string
        public string color;

        public ToDoCategory()
        {
        }

        public ToDoCategory(string name, string color)
        {
            this.name = name;
            this.color = color;
        }
    }
}