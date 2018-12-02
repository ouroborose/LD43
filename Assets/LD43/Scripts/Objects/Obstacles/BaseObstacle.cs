using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : BaseObject {

    public bool _kills = false;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(_kills)
        {
            BasePerson person = collision.attachedRigidbody.GetComponent<BasePerson>();
            if(person != null)
            {
                person.TriggerDeath();
            }
        }
    }
}
