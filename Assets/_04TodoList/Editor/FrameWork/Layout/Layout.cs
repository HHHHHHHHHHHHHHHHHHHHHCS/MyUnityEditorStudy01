using System.Collections.Generic;
using _04TodoList.Editor.FrameWork.Drawer.Interface;

namespace _04TodoList.Editor.FrameWork.Layout
{
    public abstract class Layout : IView
    {
        public bool Visible { get; set; } = true;

        public string Style { get; set; }

        protected readonly LinkedList<IView> children = new LinkedList<IView>();

        protected Layout(string style = null)
        {
            Style = style;
        }

        public void Show()
        {
            Visible = true;
        }

        public void Hide()
        {
            Visible = false;
        }

        public void Add(IView view)
        {
            children.AddLast(view);
        }

        public void Remove(IView view)
        {
            children.Remove(view);
        }

        public void Clear()
        {
            children.Clear();
        }

        public void DrawGUI()
        {
            if (Visible)
            {
                OnGUIBegin();
                OnGUI();
                OnGUIEnd();
            }
        }

        protected abstract void OnGUIBegin();

        protected abstract void OnGUI();

        protected abstract void OnGUIEnd();
    }
}