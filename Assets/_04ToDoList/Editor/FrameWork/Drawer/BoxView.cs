using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
    public class BoxView : View
    {
        public string Text { get; set; }

        public BoxView(string text)
        {
            Text = text;
            guiStyle = GUI.skin.box;
        }

        protected override void OnGUI()
        {
            GUILayout.Box(Text, guiStyle);
        }
    }
}