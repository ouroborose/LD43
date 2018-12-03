using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VuLib;

public class UIManager : Singleton<UIManager> {

    public BaseUI _titleScreen;
    public BaseUI _winScreen;
    public BaseUI _loseScreen;
    public BaseUI _hudScreen;

    protected override void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            SceneManager.LoadScene(0);
            return;
        }
        base.Awake();

        EventManager.OnStartGame.Register(OnGameStart);
        EventManager.OnFinale.Register(OnFinale);
        EventManager.OnLoseGame.Register(OnLoseGame);
        EventManager.OnWinGame.Register(OnWinGame);
    }

    private void OnGameStart()
    {
        _hudScreen.Show();
    }

    private void OnFinale()
    {
        _hudScreen.Hide();
    }

    private void OnWinGame()
    {
        _hudScreen.Hide();
        _winScreen.Show();
    }

    private void OnLoseGame()
    {
        _hudScreen.Hide();
        _loseScreen.Show();
    }

    protected override void OnDestroy()
    {
        if(Instance == this)
        {
            EventManager.OnStartGame.Unregister(OnGameStart);
            EventManager.OnFinale.Unregister(OnFinale);
            EventManager.OnLoseGame.Unregister(OnLoseGame);
            EventManager.OnWinGame.Unregister(OnWinGame);
        }
        base.OnDestroy();
    }
}
