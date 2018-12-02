using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuLib;

public class EnvironmentManager : Singleton<EnvironmentManager> {
    public GameObject _tilePrefab;

    public Transform _scrollParent;

    public float _scrollSpeed;
    public int _numActiveTiles = 10;

    protected List<BaseEnvironmentTile> _tiles = new List<BaseEnvironmentTile>();
    protected BaseEnvironmentTile _lastTile;

    protected float _scrollThreshold;

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < _numActiveTiles; ++i)
        {
            AddTile();
        }

        _scrollThreshold = -_tiles[0]._height * 2;
    }

    protected void Update()
    {
        UpdateEnvironmentScrolling();
    }

    protected void AddTile()
    {
        GameObject tileObj = Instantiate(_tilePrefab, _scrollParent);
        BaseEnvironmentTile tile = tileObj.GetComponent<BaseEnvironmentTile>();

        if(_lastTile != null)
        {
            tile.transform.localPosition = _lastTile.transform.localPosition + Vector3.up * _lastTile._height;
        }

        _lastTile = tile;
        _tiles.Add(tile);
    }

    protected void UpdateEnvironmentScrolling()
    {
        float scrollDelta = Time.deltaTime * _scrollSpeed;
        Vector3 scrollPos = _scrollParent.localPosition;
        scrollPos.y -= scrollDelta;

        if (scrollPos.y < _scrollThreshold)
        {
            BaseEnvironmentTile firstTile = _tiles[0];
            _tiles.Remove(firstTile);
            Destroy(firstTile.gameObject);

            AddTile();
            _scrollThreshold -= _tiles[0]._height;
        }

        _scrollParent.localPosition = scrollPos;
    }
}
