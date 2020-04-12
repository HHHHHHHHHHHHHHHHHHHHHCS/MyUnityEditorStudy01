using _04TodoList.Editor.FrameWork.DataBinding;
using UnityEngine;

namespace _04TodoList.Editor.FrameWork.Drawer
{
    public class ToggleView : View
    {
        public string text { get; set; }
        public Property<bool> IsToggle { get; private set; }

        public ToggleView(string _text = null, bool isToggle = false)
        {
            this.text = _text;
            IsToggle = new Property<bool>(isToggle);
        }

        protected override void OnGUI()
        {
            IsToggle.Val = GUILayout.Toggle(IsToggle.Val, text);
        }
    }
}