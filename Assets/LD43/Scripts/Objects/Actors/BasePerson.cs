using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BasePerson : BaseActor {
    public SelectionIndicator _selectionIndicator;

    public void Select()
    {
        _selectionIndicator.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        _selectionIndicator.gameObject.SetActive(false);
        ResetColor();
    }

    protected override void UpdateSortingOrder()
    {
        base.UpdateSortingOrder();
        _selectionIndicator._line.sortingOrder = _sprite.sortingOrder - 1;
    }
    

    protected override void Die()
    {
        transform.parent = EnvironmentManager.Instance._scrollParent;
        Destroy(_rigidbody);
        transform.DOScale(0, 0.5f).OnComplete(base.Die);
    }
}
