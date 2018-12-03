using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScreenUI : BaseUI {
    public void Start()
    {
        Hide(true);
    }

    public void StartGame()
    {
        Hide();
        AudioManager.Instance.PlayDefaultClickSound();
        UIManager.Instance.StartCoroutine(HandleStart());
    }

    protected IEnumerator HandleStart()
    {
        yield return new WaitForSeconds(_transitionOutDelay + _transitionOutTime + 0.1f);
        EventManager.OnStartGame.Dispatch();
    }
}
