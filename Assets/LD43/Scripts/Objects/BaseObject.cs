using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuLib;

public class BaseObject : BaseBehaviour<Main> {

    public SpriteRenderer _sprite;
    protected Color _originalColor;


    protected override void Init()
    {
        base.Init();
        if (_sprite == null)
        {
            _sprite = GetComponentInChildren<SpriteRenderer>();
        }

        _originalColor = _sprite.color;
        TriggerRevive();
    }

    protected override void ControlledLateUpdate()
    {
        base.ControlledLateUpdate();

        UpdateSortingOrder();
    }

    protected virtual void UpdateSortingOrder()
    {
        _sprite.sortingOrder = Mathf.RoundToInt(transform.position.y) * -1;
    }

    public void ResetColor()
    {
        _sprite.color = _originalColor;
    }

    public void SetColor(Color c)
    {
        _sprite.color = c;
    }
}
