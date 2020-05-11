using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.DataBinding
{
    public class ToDoModelUpgradeActionV1 : UpdateAction<Deprecated2.ToDoListCls, ToDoListCls>
    {
        protected override ToDoListCls OldConvertNewModel(Deprecated2.ToDoListCls oldModel)
        {
            ToDoListCls newList = new ToDoListCls();
            if (oldModel != null && oldModel.modelData.Count > 0)
            {
                foreach (var item in oldModel.modelData)
                {
                    ToDoData newData = new ToDoData(item.content, item.finished);
                    newList.todoList.Add(newData);
                }
            }

            return newList;
        }
    }
}