using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuLib
{
    public static class ComponentUtils
    {
        public static List<T> CollectComponents<T>(GameObject searchRoot) where T : Component
        {
            List<T> components = new List<T>();
            components.AddRange(searchRoot.GetComponentsInChildren<T>(true));
            return components;
        }
    }
}

