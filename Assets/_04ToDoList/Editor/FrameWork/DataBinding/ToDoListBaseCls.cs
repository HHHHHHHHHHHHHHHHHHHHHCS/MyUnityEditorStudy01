namespace _04ToDoList.Editor.FrameWork.DataBinding
{
    public class ToDoListBaseCls : IToDoModel
    {
        [System.NonSerialized] public const string todosKey = "ToDoList_TODOS";
        [System.NonSerialized] public const string todosVersionKey = "ToDoList_TODOS_Version";

        [System.NonSerialized] public const int version = -1;
    }
}