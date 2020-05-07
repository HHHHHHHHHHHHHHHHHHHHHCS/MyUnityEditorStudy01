using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.DataBinding
{
    public class ToDoModelUpgradeActionV1 : UpdateAction<Deprecated2.ToDoListCls, ToDoListCls>
    {
        protected override ToDoListCls OldConvertNewModel(Deprecated2.ToDoListCls oldModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
