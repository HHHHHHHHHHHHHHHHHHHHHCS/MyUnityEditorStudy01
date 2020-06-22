using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
    public class LabelView : View
    {
        public string content { get; }

        public LabelView(string _content)
        {
            content = _content;
        }

        protected override void OnGUI()
        {
            GUILayout.Label(content, style, guiLayoutOptions);
        }
    }
}