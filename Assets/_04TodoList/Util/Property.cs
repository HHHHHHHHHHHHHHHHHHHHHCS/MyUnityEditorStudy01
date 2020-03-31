using System;

namespace _04TodoList.Util
{
    [System.Serializable]
    public class Property<T>
    {
        private T val = default(T);

        public event Action onValueChangedEvent;

        public T Val
        {
            get => val;
            set
            {
                if (!val.Equals(value))
                {
                    val = value;
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
            val = v;
        }

        public void RegisterValueChanged(Action act)
        {
            onValueChangedEvent += act;
        }

        public static implicit operator T(Property<T> p)
        {
            return p.Val;
        }
    }
}