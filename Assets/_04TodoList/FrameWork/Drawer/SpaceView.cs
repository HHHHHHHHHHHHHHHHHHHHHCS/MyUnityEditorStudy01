using System.Collections;
using System.Collections.Generic;
using _04TodoList.FrameWork.Drawer.Interface;
using UnityEngine;

namespace _04TodoList.FrameWork.Drawer
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