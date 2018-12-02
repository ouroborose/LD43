using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobble : MonoBehaviour {

    public float _bobbleHeight = 0.1f;
    public float _bobbleSpeed = 10.0f;

    public float _bobbleOffset = 0.0f;

    protected void Awake()
    {
        _bobbleOffset = Random.value * Mathf.PI * 2;
    }

    protected void Update()
    {
        transform.localPosition = Mathf.Abs(Mathf.Sin(_bobbleOffset + Time.time * _bobbleSpeed)) * _bobbleHeight * Vector3.up;
    }
}
