using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
    public class FlexibleSpaceView:View
    {
        protected override void OnGUI()
        {
            GUILayout.FlexibleSpace();
        }
    }
}