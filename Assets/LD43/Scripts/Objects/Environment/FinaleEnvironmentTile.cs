using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinaleEnvironmentTile : BaseEnvironmentTile {
    public GameObject _blocker;
    public TextMeshPro _text;

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
        yield return new WaitForSeconds(1.0f);

        yield return StartCoroutine(ShowText("You have reached the end..."));
        yield return new WaitForSeconds(3.0f);
        yield return StartCoroutine(ShowText("It is time for the final sacrifice!"));
        yield return new WaitForSeconds(3.0f);
        _blocker.SetActive(false);
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
