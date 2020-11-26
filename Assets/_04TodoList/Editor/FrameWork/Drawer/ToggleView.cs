using _04ToDoList.Editor.FrameWork.DataBinding;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
    public class ToggleView : View
    {
        public string Text { get; set; }
        public Property<bool> IsToggle { get; }

        public ToggleView(string _text = null, bool isToggle = false)
        {
            Text = _text;
            IsToggle = new Property<bool>(isToggle);
        }

        protected override void OnGUI()
        {
            IsToggle.Val = GUILayout.Toggle(IsToggle.Val, Text);
        }
    }
}