using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hut : BasePowerUp
{
    public int _numPeopleToSpawn = 3;

    public override void HandleCollection(BasePerson collector)
    {
        base.HandleCollection(collector);
        
        for(int i = 0; i < _numPeopleToSpawn; ++i)
        {
            SpawnPersonAround(transform.position);
        }

        transform.DOScale(0.0f, 0.5f).SetEase(Ease.InBack);
    }

    public void SpawnPersonAround(Vector3 pos)
    {
        Vector3 dir = Random.insideUnitCircle.normalized;
        pos += dir * 16f;
        BasePerson person = Main.Instance.SpawnPerson(pos);
        
        person.transform.localScale = Vector3.zero;
        person.transform.DOScale(1, 1.0f).SetEase(Ease.OutBack).SetDelay(0.25f);
        person._rigidbody.velocity = dir * person._maxSpeed; 
    }
}
