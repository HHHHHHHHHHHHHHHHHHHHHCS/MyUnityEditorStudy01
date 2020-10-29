using System;

namespace _04ToDoList.Editor.FrameWork.DataBinding
{
    [System.Serializable]
    public class ToDoNote
    {
        public string id = Guid.NewGuid().ToString();

        public string title;

        public string content;

        public ToDoNote()
        {
        }

        public ToDoNote(string _content)
        {
            content = _content;
        }
    }
}