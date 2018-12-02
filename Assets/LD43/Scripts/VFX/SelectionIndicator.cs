using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionIndicator : MonoBehaviour {
    public LineRenderer _line;

    public int _resolution = 50;
    public float _radius = 30.0f;
    public float _spinSpeed = 10.0f;

    public float _currentRadius = 0.0f;

    protected void Awake()
    {
    }

    protected void Update()
    {
        SetRadius(_radius);
        transform.Rotate(0, 0, _spinSpeed * Time.deltaTime);
    }

    public void SetRadius(float radius)
    {
        if(_radius == _currentRadius)
        {
            return;
        }

        _currentRadius = radius;
        _line.positionCount = _resolution;

        float step = Mathf.PI * 2/_resolution;

        for (int i = 0; i < _resolution; ++i)
        {
            float t = step * i;
            _line.SetPosition(i, new Vector3(Mathf.Cos(t) * _radius, Mathf.Sin(t) * _radius, -1));
        }
    }
}
