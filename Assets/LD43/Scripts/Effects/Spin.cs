using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    public float _spinSpeed;
    
    protected void Awake()
    {
        transform.localEulerAngles = new Vector3(0, 0, Random.Range(0,360.0f));
    }

    protected void Update()
    {
        transform.Rotate(0, 0, _spinSpeed * Time.deltaTime, Space.World);
    }
}
