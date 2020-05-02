using System;
using System.Collections;
using System.Collections.Generic;

namespace _04TodoList.Editor.FrameWork.DataBinding
{
    [System.Serializable]
    public class Property<T>
    {
        public T _val = default(T);

        public event Action<T> onValueChangedEvent;

        public T Val
        {
            get => _val;
            set
            {
//                if (_val == null && value == null)
//                {
//                }
//                else if (_val == null && value != null)
//                {
//                    _val = value;
//                    onValueChangedEvent?.Invoke(_val);
//                }
//                else if (!_val.Equals(value))
//                {
//                    _val = value;
//                    onValueChangedEvent?.Invoke(_val);
//                }

                if (Comparer<T>.Default.Compare(_val, value) == 0)
                    return;

                _val = value;
                onValueChangedEvent?.Invoke(_val);
            }
        }

        //给序列化用
        public Property()
        {
        }

        public Property(T v = default(T), Action<T> act = null)
        {
            _val = v;
            RegisterValueChanged(act);
        }

        public Property<T> Bind(Action<T> setter)
        {
            RegisterValueChanged(setter);
            return this;
        }


        public void SetValueChanged(Action<T> act)
        {
            onValueChangedEvent = act;
        }

        public void RegisterValueChanged(Action<T> act)
        {
            onValueChangedEvent += act;
        }

        public void RemoveValueChanged(Action<T> act)
        {
            onValueChangedEvent -= act;
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