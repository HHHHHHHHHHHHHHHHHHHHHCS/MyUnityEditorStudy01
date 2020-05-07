using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Layout
{
    public class HorizontalLayout : Layout
    {
        public HorizontalLayout(string style = null)
            : base(style)
        {
        }

        protected override void OnGUIBegin()
        {
            if (string.IsNullOrEmpty(Style))
            {
                GUILayout.BeginHorizontal();
            }
            else
            {
                GUILayout.BeginHorizontal(Style);
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
            GUILayout.EndHorizontal();
        }
    }
}