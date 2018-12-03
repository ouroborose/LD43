using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuLib;
using DG.Tweening;

public class BaseUI : MonoBehaviour, IShowHide
{
    public bool _transitionInDown = true;
    public float _transitionInTime = 0.66f;
    public float _transitionInDelay = 0.0f;
    public Ease _transitionInEase = Ease.InOutSine;

    public bool _transitionOutDown = false;
    public float _transitionOutTime = 0.66f;
    public float _transitionOutDelay = 0.0f;
    public Ease _transitionOutEase = Ease.InOutSine;

    public float _originalY;

    protected bool _initialized = false;

    protected virtual void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        if(_initialized)
        {
            return;
        }
        _initialized = true;
        Vector3 pos = transform.localPosition;
        _originalY = pos.y;
    }

    public virtual void Hide(bool instant = false)
    {
        float y = _transitionOutDown ? -Screen.height : Screen.height;
        y *= 2;
        if (instant)
        {
            gameObject.SetActive(false);
            Vector3 pos = transform.localPosition;
            pos.y = y;
            transform.localPosition = pos;
        }

        transform.DOLocalMoveY(y, _transitionOutTime).SetEase(_transitionOutEase).SetDelay(_transitionOutDelay).OnComplete(() => gameObject.SetActive(false));
    }

    public virtual void Show(bool instant = false)
    {
        gameObject.SetActive(true);
        if(instant)
        {
            Vector3 pos = transform.localPosition;
            pos.y = _originalY;
            transform.localPosition = pos;
        }

        transform.DOLocalMoveY(_originalY, _transitionInTime).SetEase(_transitionInEase).SetDelay(_transitionInDelay);
    }

    public void PlayDefaultClickSound()
    {
        AudioManager.Instance.PlayDefaultClickSound();
    }
}
