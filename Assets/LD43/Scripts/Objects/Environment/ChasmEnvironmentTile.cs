using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasmEnvironmentTile : BaseEnvironmentTile {
    public SpriteRenderer _chasm;
    protected override void Init()
    {
        base.Init();

        Vector3 pos = _chasm.transform.localPosition;
        pos.x = Random.Range(1.0f, 2.0f) * pos.x;
        _chasm.transform.localPosition = pos;
    }

    protected override void UpdateSortingOrder()
    {
        base.UpdateSortingOrder();
        _chasm.sortingOrder = _sprite.sortingOrder+1;
    }
}
