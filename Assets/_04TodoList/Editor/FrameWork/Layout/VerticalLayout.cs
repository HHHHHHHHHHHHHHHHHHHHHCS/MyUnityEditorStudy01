using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Layout
{
    public class VerticalLayout : Layout
    {
        public VerticalLayout(string style = null)
            : base(style)
        {
        }

        protected override void OnGUIBegin()
        {
            if (string.IsNullOrEmpty(Style))
            {
                GUILayout.BeginVertical();
            }
            else
            {
                GUILayout.BeginVertical(Style);
            }
        }

        protected override void OnGUIEnd()
        {
            GUILayout.EndVertical();
        }
    }
}