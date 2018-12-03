using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : BaseObstacle {
    public Collider2D _collider;
    public Sprite[] _frames;
    protected int _deathCount = 0;

    protected override void UpdateSortingOrder()
    {
        base.UpdateSortingOrder();
        _sprite.sortingOrder = BaseEnvironmentTile.ENVIRONMENT_SORT_ORDER + 1;
    }

    protected override void Kill(BasePerson person)
    {
        if(_deathCount >= _frames.Length - 1)
        {
            return;
        }

        base.Kill(person);
        _deathCount++;
        _sprite.sprite = _frames[_deathCount];
        if(_deathCount >= _frames.Length-1)
        {
            _collider.enabled = false;
        }
    }
}
