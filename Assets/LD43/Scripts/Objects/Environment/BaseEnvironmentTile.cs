using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnvironmentTile : BaseObject {

    public GameObject[] _obstaclePrefabs;
    public int _minObstacles = 0;
    public int _maxObstacles = 10;


    public float _height { get { return _sprite.sprite.rect.height; } }

    protected override void UpdateSortingOrder()
    {
        //base.UpdateSortingOrder();
        _sprite.sortingOrder = -32768;
    }

    protected override void Init()
    {
        base.Init();

        if(_obstaclePrefabs.Length > 0)
        {
            int numObstacles = Random.Range(_minObstacles, _maxObstacles);

            for(int i = 0; i < numObstacles; ++i)
            {
                SpawnRandomObstacle();
            }
        }
    }

    public void SpawnRandomObstacle()
    {
        GameObject obstacleObj = Instantiate(_obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Length)], transform);
        obstacleObj.transform.localPosition = new Vector3(Random.Range(-150, 150), Random.Range(0, _height), 0);
    }
}
