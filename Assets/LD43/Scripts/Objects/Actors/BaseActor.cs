using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActor : BaseObject {


    public Rigidbody2D _rigidbody;
    public float _maxSpeed = 10.0f;
    public float _movementForce = 5.0f;
    public float _stoppingForce = 1.0f;
    protected Vector3 _movementDir;


    protected override void Init()
    {
        base.Init();

        if(_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
    }

    protected override void ControlledUpdate()
    {
        base.ControlledUpdate();
        UpdateMovement();
    }

    public void MoveTowards(Vector3 pos)
    {
        _movementDir = pos - transform.position;
        _movementDir.Normalize();
    }

    public void Stop()
    {
        _movementDir = Vector3.zero;
    }

    public void UpdateMovement()
    {
        if(_rigidbody == null)
        {
            return;
        }

        Vector3 vel = _rigidbody.velocity;

        Debug.DrawRay(transform.position, vel);

        _rigidbody.velocity = Vector3.Lerp(vel, _movementDir * _maxSpeed, Time.deltaTime * ((_movementDir == Vector3.zero)?_stoppingForce:_movementForce));
    }
}
