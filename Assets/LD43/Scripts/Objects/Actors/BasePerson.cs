using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
