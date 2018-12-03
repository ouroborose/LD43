using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaseObstacle : BaseObject {

    public bool _kills = false;
    public bool _doShrinkingDeath = true;

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
        person.transform.parent = EnvironmentManager.Instance._scrollParent;
        person._colliderGroup.SetActive(false);
        Destroy(person._rigidbody);

        if (_doShrinkingDeath)
        {
            person.transform.DOScale(0, 1.0f).OnComplete(person.TriggerDeath);
        }
        else
        {
            person.TriggerDeath();
        }
    }
}
