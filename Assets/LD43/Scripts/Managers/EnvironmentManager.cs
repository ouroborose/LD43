using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VuLib;

public class EnvironmentManager : Singleton<EnvironmentManager> {
    public EnvironmentData _environmentData;
    public Transform _scrollParent;
    public float _scrollSpeed;
    public float _maxScrollSpeedScale = 1.0f;
    public int _tilesSpawned = 0;
    public bool _endSpawned = false;
    public bool _endReached = false;

    public float _progress = 0.0f;

    public List<GameObject> _initialTiles;

    public List<BaseEnvironmentTile> _tiles = new List<BaseEnvironmentTile>();
    public BaseEnvironmentTile _lastTile;

    protected float _scrollThreshold;

    public void Init()
    {
        for (int i = 0; i < _initialTiles.Count; ++i)
        {
            AddTile(_initialTiles[i]);
        }

        if(!_endSpawned)
        {
            _scrollThreshold = -_tiles[0]._height * 2;
        }
    }


    protected BaseEnvironmentTile AddTile(GameObject prefab)
    {
        GameObject tileObj = Instantiate(prefab, _scrollParent);
        BaseEnvironmentTile tile = tileObj.GetComponent<BaseEnvironmentTile>();

        if(_lastTile != null)
        {
            tile.transform.localPosition = _lastTile.transform.localPosition + Vector3.up * _lastTile._height;
        }

        _lastTile = tile;
        _tiles.Add(tile);

        if(!_endSpawned)
        {
            _tilesSpawned++;
            _progress = Mathf.Clamp01((float)_tilesSpawned / _environmentData._numTotalTiles);
            EventManager.OnProgress.Dispatch(_progress);
            if(_tilesSpawned >= _environmentData._numTotalTiles)
            {
                SpawnEndTile();
            }
        }

        return tile;
    }

    public void SpawnEndTile()
    {
        _endSpawned = true;
        AddTile(_environmentData._emptyTilePrefab);
        BaseEnvironmentTile end = AddTile(_environmentData._endTilePrefab);
        _scrollThreshold = _scrollParent.transform.localPosition.y - end.transform.position.y + (600 - end._height);
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

    public void UpdateEnvironmentScrolling()
    {
        float scrollSpeed = _scrollSpeed;
        scrollSpeed += _scrollSpeed * _maxScrollSpeedScale * _progress;

        float scrollDelta = Time.deltaTime * scrollSpeed;
        Vector3 scrollPos = _scrollParent.localPosition;
        scrollPos.y -= scrollDelta;

        if (scrollPos.y < _scrollThreshold)
        {
            if(_endSpawned)
            {
                scrollPos.y = _scrollThreshold;
                _endReached = true;
            }
            else
            {
                BaseEnvironmentTile firstTile = _tiles[0];
                _tiles.Remove(firstTile);
                Destroy(firstTile.gameObject);

                AddRandomTile();
                if(!_endSpawned)
                {
                    _scrollThreshold -= _tiles[0]._height;
                }
                
            }
        }

        _scrollParent.localPosition = scrollPos;
    }

}
