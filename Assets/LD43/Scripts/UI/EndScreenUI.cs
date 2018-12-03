using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenUI : BaseUI
{
    public BaseUI _fader;

    public void Start()
    {
        Hide(true);
    }

    public void RestartGame()
    {
        Hide();
        AudioManager.Instance.PlayDefaultClickSound();
        UIManager.Instance.StartCoroutine(HandleRestart());
    }

    protected IEnumerator HandleRestart()
    {
        _fader.Show();
        yield return new WaitForSeconds(_fader._transitionInDelay + _fader._transitionInTime + 0.1f);
        SceneManager.LoadScene(0);
    }
}
