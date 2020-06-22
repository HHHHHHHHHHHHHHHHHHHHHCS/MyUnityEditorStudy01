using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.Drawer
{
    public class SpaceView : View
    {
        public float Pixel { get; set; } = 10;

        public SpaceView(float pixel = 10)
        {
            Pixel = pixel;
        }

        protected override void OnGUI()
        {
            GUILayout.Space(Pixel);
        }
    }
}