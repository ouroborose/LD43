using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuLib
{
    public class BaseBehaviour<T> : MonoBehaviour where T:BaseMain<T>
    {
        protected bool _initialized = false;
        public bool _isAlive { get; protected set; }
        protected bool _isDestroyed = false;

        protected void Awake()
        {
            TriggerInit();
        }

        public void TriggerInit()
        {
            if(_initialized)
            {
                return;
            }

            Init();
        }

        protected virtual void Init()
        {
            BaseMain<T>.Instance.RegisterBehaviour(this);

            _initialized = true;
            // init
        }

        protected void OnDestroy()
        {
            TriggerDestroy();
        }

        protected void TriggerDestroy()
        {
            if(_isDestroyed)
            {
                return;
            }

            Destroy();
        }

        protected virtual void Destroy()
        {
            BaseMain<T>.Instance?.UnregisterBehaviour(this);

            _isDestroyed = true;
            // destroy
        }
        

        public void TriggerRevive()
        {
            if(_isAlive)
            {
                return;
            }

            Revive();
        }

        protected virtual void Revive()
        {
            _isAlive = true;
        }

        public void TriggerDeath()
        {
            if(!_isAlive)
            {
                return;
            }

            Die();
        }

        protected virtual void Die()
        {
            _isAlive = false;
        }

        public void TriggerControlledUpdate()
        {
            if(!_isAlive)
            {
                return;
            }

            ControlledUpdate();
        }

        protected virtual void ControlledUpdate()
        {
            // update
        }

        public void TriggerControlledFixedUpdate()
        {
            if (!_isAlive)
            {
                return;
            }

            ControlledFixedUpdate();
        }

        public virtual void ControlledFixedUpdate()
        {
            // fixed update
        }

        public void TriggerControlledLateUpdate()
        {
            if (!_isAlive)
            {
                return;
            }

            ControlledLateUpdate();
        }

        protected virtual void ControlledLateUpdate()
        {
            // late update
        }

    }
}

