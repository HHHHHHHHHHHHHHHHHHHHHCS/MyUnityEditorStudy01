using System.Collections.Generic;
using _04TodoList.Editor.FrameWork.Drawer.Interface;
using UnityEngine;

namespace _04TodoList.Editor.FrameWork.ViewController
{
    public abstract class AbsViewController
    {
        protected List<IView> views;

        protected AbsViewController()
        {
            views = new List<IView>();
        }

        public virtual void Draw()
        {
            views.ForEach(x => x.DrawGUI());
        }
    }
}