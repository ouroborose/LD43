﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VuLib;
using DG.Tweening;

public class Main : BaseMain<Main>
{
    public GameObject _personPrefab;
    public int _numStartingPeople = 10;

    public float _selectionRadius = 50.0f;
    public float _radiusChangeDelta = 5.0f;
    public float _minSelectionRadius = 20.0f;
    public float _maxSelectionRadius = 100.0f;
    public SelectionIndicator _selectionIndicator;
    public LineRenderer _movementLine;

    public Color _candidateColor = Color.cyan;
    public Color _movmentColor = Color.green;

    protected List<BasePerson> _people = new List<BasePerson>();
    protected List<BasePerson> _selectedPeople = new List<BasePerson>();

    protected Camera _mainCamera;
    protected Plane _groundPlane;

    protected Vector3 _mouseWorldPos;

    public enum GamePhase
    {
        Title,
        Game,
        Win,
        Lose,
    }

    public GamePhase _gamePhase = GamePhase.Title;

    protected override void Awake()
    {
        base.Awake();

        SceneManager.LoadScene(1, LoadSceneMode.Additive);

        _mainCamera = Camera.main;
        _groundPlane = new Plane(Vector3.forward, Vector3.zero);
        _selectionIndicator.gameObject.SetActive(false);

        EventManager.OnStartGame.Register(OnGameStart);
    }

    protected void OnGameStart()
    {
        _gamePhase = GamePhase.Game;
        _selectionIndicator.gameObject.SetActive(true);
    }

    protected override void OnDestroy()
    {
        EventManager.OnStartGame.Unregister(OnGameStart);
        base.OnDestroy();
    }

    public override void Start()
    {
        base.Start();

        InitGame();
    }
    

    public override void Update()
    {
        base.Update();

        if(_gamePhase == GamePhase.Game)
        {
            UpdatePlayerControls();
        }
    }


    protected void InitGame()
    {
        // spawn people
        for(int i = 0; i < _numStartingPeople; ++i)
        {
            SpawnPerson(new Vector3(UnityEngine.Random.Range(-100, 100), UnityEngine.Random.Range(250, 350), 0));
        }
    }

    protected void UpdatePlayerControls()
    {
        Ray mouseRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        float hitDist;
        if (_groundPlane.Raycast(mouseRay, out hitDist))
        {
            _mouseWorldPos = mouseRay.GetPoint(hitDist);
            _selectionIndicator.transform.localPosition = _mouseWorldPos;
        }
        
        if(Input.GetMouseButtonDown(0))
        {
            StartPeopleMovement();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            StopPeopleMovement();
        }
        else if(Input.GetMouseButton(0))
        {
            UpdatePeopleMovement();
        }
        else
        {
            UpdatePeopleSelection();
        }
    }
    
    public BasePerson SpawnPerson(Vector3 pos)
    {
        GameObject personObj = Instantiate(_personPrefab, transform);
        BasePerson person = personObj.GetComponent<BasePerson>();
        person.transform.position = pos;

        _people.Add(person);
        _selectedPeople.Add(person);
        
        return person;
    }


    public void RemovePerson(BasePerson person)
    {
        _people.Remove(person);
        _selectedPeople.Remove(person);
    }

    protected void StartPeopleMovement()
    {
        for (int i = 0; i < _selectedPeople.Count; ++i)
        {
            _selectedPeople[i].SetColor(_movmentColor);
        }

        if(_selectedPeople.Count > 0)
        {
            _movementLine.gameObject.SetActive(true);
        }
    }

    protected void StopPeopleMovement()
    {
        
        for (int i = 0; i < _selectedPeople.Count; ++i)
        {
            _selectedPeople[i].Stop();
        }
    }
    
    protected void UpdatePeopleMovement()
    {

        if (_selectedPeople.Count <= 0)
        {
            return;
        }

        Vector3 center = Vector3.zero;
        int livingCount = 0;
        for (int i = 0; i < _selectedPeople.Count; ++i)
        {
            if(!_selectedPeople[i]._isAlive)
            {
                continue;
            }

            _selectedPeople[i].MoveTowards(_mouseWorldPos);
            center += _selectedPeople[i].transform.position;
            livingCount++;
        }

        if(livingCount > 0)
        {
            center /= livingCount;

            _movementLine.SetPosition(0, _mouseWorldPos);
            _movementLine.SetPosition(1, center);
        }
    }

    protected void UpdatePeopleSelection()
    {
        // handle people selection
        _selectionRadius = Mathf.Clamp(_selectionRadius + Input.mouseScrollDelta.y * _radiusChangeDelta, _minSelectionRadius, _maxSelectionRadius);
        _selectionIndicator._radius = _selectionRadius;


        _selectedPeople.Clear();
        float sqrSelectionRadius = _selectionRadius * _selectionRadius;
        for (int i = 0; i < _people.Count; ++i)
        {
            BasePerson person = _people[i];

            if (!person._isAlive)
            {
                continue;
            }

            Vector3 center = person.transform.position;
            center.y += 16; // offset to center of person
            float sqrDist = Vector3.SqrMagnitude(center - _mouseWorldPos);
            if (sqrDist <= sqrSelectionRadius)
            {
                person.SetColor(_candidateColor);
                person.Select();

                _selectedPeople.Add(person);
            }
            else
            {
                person.Deselect();
            }
        }

        if(_movementLine.gameObject.activeSelf)
        {
            Vector3 head = _movementLine.GetPosition(0);
            Vector3 tail = _movementLine.GetPosition(1);
            _movementLine.SetPosition(1, Vector3.Lerp(tail, head, Time.deltaTime * 10.0f));
            if(Vector3.SqrMagnitude(head-tail) < 1f)
            {
                _movementLine.gameObject.SetActive(false);
            }
        }
    }
}
