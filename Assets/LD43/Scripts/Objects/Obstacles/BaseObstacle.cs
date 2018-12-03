using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : BaseObject {

    public bool _kills = false;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody == null)
        {
            return;
        }

        if (_kills)
        {
            BasePerson person = collision.attachedRigidbody.GetComponent<BasePerson>();
            if(person != null)
            {
                Main.Instance.RemovePerson(person);
                Kill(person);
            }
        }
    }

    protected virtual void Kill(BasePerson person)
    {
        person.TriggerDeath();
    }
}
