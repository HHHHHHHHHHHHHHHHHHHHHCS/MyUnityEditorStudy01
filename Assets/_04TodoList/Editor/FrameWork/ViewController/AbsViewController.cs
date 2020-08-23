using System.Collections.Generic;
using _04ToDoList.Editor.FrameWork.Drawer.Interface;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewController
{
    public abstract class AbsViewController
    {
        public int eventKey { get; protected set; }

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
            foreach (var item in views)
            {
                item.Refresh();
            }

            OnUpdate();
            views.ForEach(x => x.DrawGUI());
        }
    }
}