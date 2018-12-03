using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePowerUp : BaseObject {

    public bool _collected = false;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (_collected)
        {
            return;
        }

        if(collision.attachedRigidbody == null)
        {
            return;
        }

        BasePerson person = collision.attachedRigidbody.GetComponent<BasePerson>();
        if (person != null)
        {
            HandleCollection(person);
        }
    }

    public virtual void HandleCollection(BasePerson person)
    {
        _collected = true;
    }
}
