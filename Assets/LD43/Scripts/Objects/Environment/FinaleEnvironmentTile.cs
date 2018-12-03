using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinaleEnvironmentTile : BaseEnvironmentTile {
    public GameObject _blocker;
    public BaseObject _leftBlocker;
    public BaseObject _rightBlocker;

    public float _gateOpeningTime = 3.0f;

    public TextMeshPro _text;

    public AudioClip _winTheme;

    protected override void Init()
    {
        base.Init();

        _text.alpha = 0.0f;

        EventManager.OnFinale.Register(OnFinale);
        EventManager.OnWinGame.Register(OnWinGame);
    }

    protected override void Destroy()
    {
        EventManager.OnFinale.Unregister(OnFinale);
        EventManager.OnWinGame.Unregister(OnWinGame);
        base.Destroy();
    }

    protected void OnWinGame()
    {
        _text.alpha = 0.0f;
    }

    protected void OnFinale()
    {
        StartCoroutine(HandleFinale());
    }

    protected IEnumerator HandleFinale()
    {
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayOneShot(_winTheme);
        yield return new WaitForSeconds(1.0f);

        yield return StartCoroutine(ShowText("You have reached the end..."));
        yield return new WaitForSeconds(3.0f);
        yield return StartCoroutine(ShowText("It is time for the final sacrifice!"));
        yield return new WaitForSeconds(0.5f);
        _blocker.SetActive(false);

        float timer = 0.0f;
        float gateMoveDist = 100;
        Vector3 leftStart = _leftBlocker.transform.localPosition;
        Vector3 rightStart = _rightBlocker.transform.localPosition;
        while(timer < _gateOpeningTime)
        {
            yield return new WaitForEndOfFrame();

            timer += Time.deltaTime;
            float t = timer / _gateOpeningTime;
            _leftBlocker.transform.localPosition = leftStart + gateMoveDist * t * Vector3.left;
            _leftBlocker._sprite.transform.localPosition = Random.insideUnitCircle * 2.5f;
            _rightBlocker.transform.localPosition = rightStart + gateMoveDist * t * Vector3.right;
            _rightBlocker._sprite.transform.localPosition = Random.insideUnitCircle * 2.5f;
        }

        _leftBlocker._sprite.transform.localPosition = Vector3.zero;
        _rightBlocker._sprite.transform.localPosition = Vector3.zero;
    }

    protected IEnumerator ShowText(string text)
    {
        _text.alpha = 0.0f;
        _text.text = text;

        float timer = 0.0f;
        while(timer < 1.0f)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            _text.alpha = timer / 1.0f;
        }
    }
}
