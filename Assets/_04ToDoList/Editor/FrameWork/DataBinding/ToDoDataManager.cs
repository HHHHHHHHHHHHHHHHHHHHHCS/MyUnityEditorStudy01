using _04ToDoList.Editor.FrameWork.DataBinding;


public static class ToDoDataManager
{
    public static ToDoListCls Data => ToDoListCls.ModelData;

    public static void Save() => Data.Save();

    public static void AddToDoItem(string content, bool finished = false, TodoCategory category = null) =>
        Data.AddToDoItem(content, finished, category);

    public static void AddToDoItem(ToDoData data) =>
        Data.AddToDoItem(data);

    public static void RemoveToDoItem(int index) =>
        Data.RemoveToDoItem(index);

    public static void RemoveToDoItem(ToDoData data) =>
        Data.RemoveToDoItem(data);

    public static void AddToDoCategory(string name, string color) => Data.AddToDoCategory(name, color);

    public static void AddToDoCategory(TodoCategory category) => Data.AddToDoCategory(category);

    public static TodoCategory ToDoCategoryAt(int index) => Data.ToDoCategoryAt(index);

    public static int ToDoCategoryIndexOf(TodoCategory category) => Data.ToDoCategoryIndexOf(category);

    public static void RemoveToDoCategory(TodoCategory category) => Data.RemoveToDoCategory(category);

    public static void AddToDoNote(string note) => Data.AddToDoNote(note);

    public static void AddToDoNote(ToDoNote note) => Data.AddToDoNote(note);

    public static void RemoveToDoNote(ToDoNote note) => Data.RemoveToDoNote(note);

    public static void ConvertToDoNote(ToDoNote note, bool isHide) => Data.ConvertToDoNote(note, isHide);
}