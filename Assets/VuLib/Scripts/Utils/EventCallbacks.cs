using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuLib
{
    public class EventCallback
    {
        protected System.Action _callbacks;

        public void Register(System.Action callback)
        {
            _callbacks += callback;
        }

        public void Unregister(System.Action callback)
        {
            _callbacks -= callback;
        }

        public void Dispatch()
        {
            if(_callbacks != null)
            {
                _callbacks.Invoke();
            }
        }
    }

    public class EventCallback<T>
    {
        protected System.Action<T> _callbacks;

        public void Register(System.Action<T> callback)
        {
            _callbacks += callback;
        }

        public void Unregister(System.Action<T> callback)
        {
            _callbacks -= callback;
        }

        public void Dispatch(T arg)
        {
            if (_callbacks != null)
            {
                _callbacks.Invoke(arg);
            }
        }
    }

    public class EventCallback<T0, T1>
    {
        protected System.Action<T0, T1> _callbacks;

        public void Register(System.Action<T0, T1> callback)
        {
            _callbacks += callback;
        }

        public void Unregister(System.Action<T0, T1> callback)
        {
            _callbacks -= callback;
        }

        public void Dispatch(T0 arg0, T1 arg1)
        {
            if (_callbacks != null)
            {
                _callbacks.Invoke(arg0, arg1);
            }
        }
    }

    public class EventCallback<T0, T1, T2>
    {
        protected System.Action<T0, T1, T2> _callbacks;

        public void Register(System.Action<T0, T1, T2> callback)
        {
            _callbacks += callback;
        }

        public void Unregister(System.Action<T0, T1, T2> callback)
        {
            _callbacks -= callback;
        }

        public void Dispatch(T0 arg0, T1 arg1, T2 arg2)
        {
            if (_callbacks != null)
            {
                _callbacks.Invoke(arg0, arg1, arg2);
            }
        }
    }

    public class EventCallback<T0, T1, T2, T3>
    {
        protected System.Action<T0, T1, T2, T3> _callbacks;

        public void Register(System.Action<T0, T1, T2, T3> callback)
        {
            _callbacks += callback;
        }

        public void Unregister(System.Action<T0, T1, T2, T3> callback)
        {
            _callbacks -= callback;
        }

        public void Dispatch(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            if (_callbacks != null)
            {
                _callbacks.Invoke(arg0, arg1, arg2, arg3);
            }
        }
    }
}

