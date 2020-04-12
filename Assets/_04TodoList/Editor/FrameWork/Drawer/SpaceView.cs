using UnityEngine;

namespace _04TodoList.Editor.FrameWork.Drawer
{
    public class SpaceView : View
    {
        public int Pixel { get; set; } = 10;

        public SpaceView(int pixel = 10)
        {
            Pixel = pixel;
        }

        protected override void OnGUI()
        {
            GUILayout.Space(10);
        }
    }
}