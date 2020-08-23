using _04ToDoList.Editor.FrameWork.Layout;
using _04ToDoList.Editor.FrameWork.ViewController;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.ViewGUI
{
    public class ToDoListPage : VerticalLayout
    {
        public AbsViewController ctrl { get; protected set; }

        public int eventKey { get; protected set; }

        public ToDoListPage(AbsViewController _ctrl, string style = null) : base(style)
        {
            eventKey = GetHashCode();
            ctrl = _ctrl;
            RegisterEvent(ctrl.eventKey, ChangePage);
        }

        public void ChangePage(object currentPage)
        {
            if ((int) currentPage == eventKey)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            UnRegisterAll();
        }
    }
}