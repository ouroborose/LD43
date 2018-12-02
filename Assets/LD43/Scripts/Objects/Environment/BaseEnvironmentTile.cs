using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnvironmentTile : BaseObject {

    public float _height { get { return _sprite.sprite.rect.height; } }
    

    protected override void UpdateSortingOrder()
    {
        //base.UpdateSortingOrder();
        _sprite.sortingOrder = -32768;
    }
}
