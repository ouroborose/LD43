using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace VuLib
{
    public class BaseSceneManager : Singleton<BaseSceneManager>
    {
        public static void ResetGame()
        {
            SceneManager.LoadScene(0);
        }

        public static int _testSceneIndex = -1;

        protected System.Action<BaseSceneRoot> _onSceneLoaded;
        protected BaseSceneRoot _activeSceneRoot;

        protected override void Awake()
        {
            base.Awake();
            
            SceneManager.sceneLoaded += OnSceneLoaded;
            if(_testSceneIndex > 0)
            {
                LoadSceneAdditive(_testSceneIndex);
                _testSceneIndex = -1;
            }
        }

        protected override void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            base.OnDestroy();
        }

        protected virtual void OnSceneLoaded(Scene loadedScene, LoadSceneMode mode)
        {
            if(_activeSceneRoot != null)
            {
                Destroy(_activeSceneRoot.gameObject);
                _activeSceneRoot = null;
            }

            GameObject[] roots = loadedScene.GetRootGameObjects();
            BaseSceneRoot sceneRoot = null;
            for(int i = 0; i < roots.Length; ++i)
            {
                sceneRoot = roots[i].GetComponent<BaseSceneRoot>();
                if(sceneRoot != null)
                {
                    sceneRoot.name += string.Format(" < {0} >", loadedScene.name);
                    _activeSceneRoot = sceneRoot;
                    if(_onSceneLoaded != null)
                    {
                        _onSceneLoaded(_activeSceneRoot);
                    }
                    break;
                }
            }

            _onSceneLoaded = null;
            SceneManager.MergeScenes(loadedScene, SceneManager.GetActiveScene());
        }

        public virtual void LoadSceneAdditive(int index, System.Action<BaseSceneRoot> onSceneLoaded = null)
        {
            _onSceneLoaded = onSceneLoaded;
            SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        }

        public virtual void LoadSceneAdditive(string sceneName, System.Action<BaseSceneRoot> onSceneLoaded = null)
        {
            _onSceneLoaded = onSceneLoaded;
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

    }
}

