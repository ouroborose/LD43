using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreenUI : BaseUI
{
    public TextMeshProUGUI _text;
    public BaseUI _fader;

    protected string _textFormat;

    public void Start()
    {
        _textFormat = _text.text;
        Hide(true);
    }

    public override void Show(bool instant = false)
    {
        _text.text = string.Format(_textFormat, Main.Instance._sacrificeCount);
        base.Show(instant);
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
