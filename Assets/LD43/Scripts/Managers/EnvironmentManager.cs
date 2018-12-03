using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuLib;

public class EnvironmentManager : Singleton<EnvironmentManager> {
    public EnvironmentData _environmentData;

    public Transform _scrollParent;

    public float _scrollSpeed;

    public List<GameObject> _initialTiles;

    public List<BaseEnvironmentTile> _tiles = new List<BaseEnvironmentTile>();
    public BaseEnvironmentTile _lastTile;

    protected float _scrollThreshold;

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < _initialTiles.Count; ++i)
        {
            AddTile(_initialTiles[i]);
        }

        _scrollThreshold = -_tiles[0]._height * 2;
    }

    protected void Update()
    {

        if(Main.Instance._gamePhase == Main.GamePhase.Game)
        {
            UpdateEnvironmentScrolling();
        }
    }

    protected void AddTile(GameObject prefab)
    {
        GameObject tileObj = Instantiate(prefab, _scrollParent);
        BaseEnvironmentTile tile = tileObj.GetComponent<BaseEnvironmentTile>();

        if(_lastTile != null)
        {
            tile.transform.localPosition = _lastTile.transform.localPosition + Vector3.up * _lastTile._height;
        }

        _lastTile = tile;
        _tiles.Add(tile);
    }


    protected void AddRandomTile()
    {
        if(Main.Instance._gamePhase == Main.GamePhase.Game)
        {
            AddTile(_environmentData.GetRandomTilePrefab());
        }
        else
        {
            AddTile(_environmentData._emptyTilePrefab);
        }
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

            AddRandomTile();
            _scrollThreshold -= _tiles[0]._height;
        }

        _scrollParent.localPosition = scrollPos;
    }

}
