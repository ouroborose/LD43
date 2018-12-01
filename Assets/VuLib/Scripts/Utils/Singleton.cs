using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuLib
{
    public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        public static T Instance { get; protected set; }

        protected virtual void Awake()
        {
            if(Instance != null)
            {
                Debug.LogErrorFormat("Multiple Instance of {0}", typeof(T).Name);
                return;
            }

            Instance = this as T;
        }

        protected virtual void OnDestroy()
        {
            if(Instance == this)
            {
                Instance = null;
            }
        }
    }
}


