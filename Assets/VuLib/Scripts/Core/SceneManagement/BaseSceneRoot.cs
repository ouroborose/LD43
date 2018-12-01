using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VuLib
{
    public class BaseSceneRoot : MonoBehaviour
    {
        protected bool _initialized = false;

        protected void Awake()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (currentSceneIndex != 0)
            {
                BaseSceneManager._testSceneIndex = currentSceneIndex;
                SceneManager.LoadScene(0);
                return;
            }

            TriggerInit();
        }

        public virtual void TriggerInit()
        {
            if(_initialized)
            {
                return;
            }

            _initialized = true;
            Init();
        }

        protected virtual void Init()
        {

        }
    }
}

