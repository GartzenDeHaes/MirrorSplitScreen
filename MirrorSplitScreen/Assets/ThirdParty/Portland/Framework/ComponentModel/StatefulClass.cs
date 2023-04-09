using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Portland.ComponentModel
{
    public sealed class StatefulClass<T> where T : struct, IConvertible
    {
        public T State { get; private set; }

        private const BindingFlags FLAGS = BindingFlags.NonPublic | BindingFlags.Instance;

        private IDictionary<T, MethodInfo> _enter = new Dictionary<T, MethodInfo>();
        private IDictionary<T, MethodInfo> _update = new Dictionary<T, MethodInfo>();
        private IDictionary<T, MethodInfo> _exit = new Dictionary<T, MethodInfo>();

        private object[] _stateArgs = new object[1];

        // this constructor is for inheritance, but making the class sealed
        //public StatefulClass(T init)
        //{
        //    Init(init, this);
        //}

        public StatefulClass(T init, object me)
        {
            Init(init, me);
        }

        private void Init(T init, object me)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumeration");
            }

            // Cache state and transition functions
            foreach (T value in typeof(T).GetEnumValues())
            {
                var s = me.GetType().GetMethod(value.ToString() + "_Enter", FLAGS);
                if (s != null)
                {
                    _enter.Add(value, s);
                }

                s = me.GetType().GetMethod(value.ToString() + "_Update", FLAGS);
                if (s != null)
                {
                    _update.Add(value, s);
                }

                var t = GetType().GetMethod(value.ToString() + "_Exit", FLAGS);
                if (t != null)
                {
                    _exit.Add(value, t);
                }
            }

            State = init;
        }

        public void ChangeState(T next)
        {
            if (_exit.TryGetValue(State, out MethodInfo method))
            {
                _stateArgs[0] = next;
                method.Invoke(this, _stateArgs);
            }

            if (_enter.TryGetValue(next, out method))
            {
                _stateArgs[0] = State;
                method.Invoke(this, _stateArgs);
            }

            State = next;
        }

        public void Update()
        {
            if (_update.TryGetValue(State, out MethodInfo method))
            {
                method.Invoke(this, null);
            }
        }
    }
}
