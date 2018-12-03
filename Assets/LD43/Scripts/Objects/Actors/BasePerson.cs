using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BasePerson : BaseActor {
    public static readonly int WALK_OFFSET_ID = Animator.StringToHash("WalkOffset");
    public static readonly int WALK_MIRRORED_ID = Animator.StringToHash("WalkMirrored");

    public SpriteRenderer _shadow;
    public SelectionIndicator _selectionIndicator;
    public Animator _animator;

    protected override void Init()
    {
        base.Init();
        _animator.SetFloat(WALK_OFFSET_ID, Random.value);
        _animator.SetBool(WALK_MIRRORED_ID, Random.value > 0.5f);
    }

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
        _shadow.sortingOrder = BaseEnvironmentTile.ENVIRONMENT_SORT_ORDER + 1;
    }
}
