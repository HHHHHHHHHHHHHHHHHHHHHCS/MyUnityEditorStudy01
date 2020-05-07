using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Layout
{
    public class VerticalLayout : Editor.FrameWork.Layout.Layout
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

        protected override void OnGUI()
        {
            using (var ptr = children.GetEnumerator())
            {
                while (ptr.MoveNext())
                {
                    ptr.Current?.DrawGUI();
                }
            }
        }

        protected override void OnGUIEnd()
        {
            GUILayout.EndVertical();
        }
    }
}