using System;
using UnityEngine;

namespace _04TodoList.FrameWork.DataBinding
{
    [System.Serializable]
    public class Property<T>
    {
        public T _val = default(T);

        public event Action onValueChangedEvent;

        public T Val
        {
            get => _val;
            set
            {
                if (!_val.Equals(value))
                {
                    _val = value;
                    onValueChangedEvent?.Invoke();
                }
            }
        }

        //给序列化用
        public Property()
        {
        }

        public Property(T v)
        {
            _val = v;
        }

        public Property<T> Bind(Action<T> setter)
        {
            RegisterValueChanged(() => { setter?.Invoke(_val); });
            return this;
        }


        public void SetValueChanged(Action act)
        {
            onValueChangedEvent = act;
        }

        public void RegisterValueChanged(Action act)
        {
            onValueChangedEvent += act;
        }

        public void ClearValueChanged()
        {
            onValueChangedEvent = null;
        }

        public static implicit operator T(Property<T> p)
        {
            return p.Val;
        }
    }
}