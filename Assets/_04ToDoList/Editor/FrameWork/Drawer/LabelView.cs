using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
    public class LabelView : View
    {
        public string content { get; private set; }

        public LabelView(string _content)
        {
            content = _content;
            guiStyle = new GUIStyle(GUI.skin.label);
        }

        public void SetText(string _content)
        {
            content = _content;
        }

        protected override void OnGUI()
        {
            GUILayout.Label(content, guiStyle, guiLayoutOptions);
        }
    }
}