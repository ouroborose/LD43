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

        if(_sprite != null)
        {
            _originalColor = _sprite.color;
        }
        TriggerRevive();
    }

    protected override void ControlledLateUpdate()
    {
        base.ControlledLateUpdate();

        UpdateSortingOrder();
    }

    protected virtual void UpdateSortingOrder()
    {
        if(_sprite != null)
        {
            _sprite.sortingOrder = Mathf.RoundToInt(transform.position.y) * -1;
        }
    }

    public void ResetColor()
    {
        _sprite.color = _originalColor;
    }

    public void SetColor(Color c)
    {
        _sprite.color = c;
    }

    protected override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
    }
}
