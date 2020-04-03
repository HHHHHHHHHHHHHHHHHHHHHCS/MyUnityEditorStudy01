using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _04TodoList.FrameWork.Drawer
{
    public class SpaceView : IView
    {
        public int Pixel { get; set; } = 10;

        public SpaceView(int pixel = 10)
        {
            Pixel = pixel;
        }

        public void OnGUI()
        {
            GUILayout.Space(10);
        }
    }

}


