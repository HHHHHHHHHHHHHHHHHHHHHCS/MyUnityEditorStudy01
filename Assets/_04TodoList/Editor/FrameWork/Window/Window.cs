using UnityEditor;

namespace _04TodoList.Editor.FrameWork.Window
{
    public abstract class Window : EditorWindow
    {
        protected abstract void OnGUI();
    }
}