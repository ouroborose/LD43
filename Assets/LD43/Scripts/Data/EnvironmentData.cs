using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnvironmentData", menuName = "LD43/Data/EnvironmentData")]
public class EnvironmentData : ScriptableObject {

    [System.Serializable]
    public class EnvironmentTileData
    {
        public GameObject _prefab;
    }

    public int _numTotalTiles = 10;

    public GameObject _emptyTilePrefab;
    public GameObject _endTilePrefab;
    public EnvironmentTileData[] _tileData;

    public GameObject GetRandomTilePrefab()
    {
        return _tileData[Random.Range(0, _tileData.Length)]._prefab;
    }
}
