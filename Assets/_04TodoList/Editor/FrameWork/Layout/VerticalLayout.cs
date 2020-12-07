using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Layout
{
    public class VerticalLayout : Layout
    {
        public VerticalLayout(GUIStyle style = null)
            : base(style)
        {
        }

        protected override void OnGUIBegin()
        {
            if (Style == null)
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