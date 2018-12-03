using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenUI : BaseUI
{
    public float _startDelay = 1.0f;

    public BaseUI _fader;

    public void Start()
    {
        _fader.Show(true);
        StartCoroutine(HandleStartTransition());
    }

    protected IEnumerator HandleStartTransition()
    {
        yield return new WaitForSeconds(_startDelay);
        _fader.Hide();
    }

    public void StartGame()
    {
        Hide();
        AudioManager.Instance.PlayDefaultClickSound();

        EventManager.OnStartGame.Dispatch();
    }
}
