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
            SetUpView();
        }

        protected abstract void SetUpView();

        protected virtual void OnUpdate()
        {
        }


        public virtual void Draw()
        {
            OnUpdate();
            views.ForEach(x => x.DrawGUI());
        }
    }
}