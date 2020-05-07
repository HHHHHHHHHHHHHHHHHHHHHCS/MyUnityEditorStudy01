using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _04ToDoList.Editor.FrameWork.DataBinding
{
    public abstract class UpdateAction<TOldModel, TNewModel>
        where TOldModel : class, new()
        where TNewModel : class, new()
    {
        public TNewModel Result { get; protected set; }


        public TNewModel Execute(TOldModel oldModel)
        {
            Result = OldConvertNewModel(oldModel);
            return Result;
        }

        protected abstract TNewModel OldConvertNewModel(TOldModel oldModel);
    }
}