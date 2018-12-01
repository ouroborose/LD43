using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VuLib
{
    public class BaseMain<T> : Singleton<T> where T:BaseMain<T>
    {
        protected bool _gameStarted = false;

        protected List<BaseBehaviour<T>> _allBehaviours = new List<BaseBehaviour<T>>();

        public virtual void Start()
        {
            StartGame();
        }

        public virtual void StartGame()
        {
            _gameStarted = true;
        }


        public virtual void EndGame()
        {
            _gameStarted = false;
        }

        public virtual void Update()
        {
            if(!_gameStarted)
            {
                return;
            }

            for(int i = 0; i < _allBehaviours.Count; ++i)
            {
                _allBehaviours[i].TriggerControlledUpdate();
            }
        }

        public virtual void FixedUpdate()
        {
            if (!_gameStarted)
            {
                return;
            }

            for (int i = 0; i < _allBehaviours.Count; ++i)
            {
                _allBehaviours[i].TriggerControlledFixedUpdate();
            }
        }

        public virtual void LateUpdate()
        {
            if (!_gameStarted)
            {
                return;
            }

            for (int i = 0; i < _allBehaviours.Count; ++i)
            {
                _allBehaviours[i].TriggerControlledLateUpdate();
            }
        }

        public virtual void RegisterBehaviour(BaseBehaviour<T> behaviour)
        {
            _allBehaviours.Add(behaviour);
        }

        public virtual void UnregisterBehaviour(BaseBehaviour<T> behaviour)
        {
            _allBehaviours.Remove(behaviour);
        }
    }
}

