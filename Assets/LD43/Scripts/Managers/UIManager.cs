using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VuLib;

public class UIManager : Singleton<UIManager> {

    public BaseUI _titleScreen;
    public BaseUI _winScreen;
    public BaseUI _loseScreen;

    protected override void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            SceneManager.LoadScene(0);
            return;
        }
        base.Awake();

        EventManager.OnLoseGame.Register(OnLoseGame);
        EventManager.OnWinGame.Register(OnWinGame);
    }

    private void OnWinGame()
    {
        _winScreen.Show();
    }

    private void OnLoseGame()
    {
        _loseScreen.Show();
    }

    protected override void OnDestroy()
    {
        if(Instance == this)
        {
            EventManager.OnLoseGame.Unregister(OnLoseGame);
            EventManager.OnWinGame.Unregister(OnWinGame);
        }
        base.OnDestroy();
    }
}
