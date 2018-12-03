using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VuLib;
using DG.Tweening;
using TMPro;

public class Main : BaseMain<Main>
{
    public GameObject _personPrefab;
    public int _numStartingPeople = 10;

    public float _selectionRadius = 50.0f;
    public float _radiusChangeDelta = 5.0f;
    public float _minSelectionRadius = 20.0f;
    public float _maxSelectionRadius = 100.0f;
    public float _speedMultiplierDist = 100.0f;
    public SelectionIndicator _selectionIndicator;
    public LineRenderer _movementLine;

    public Color _candidateColor = Color.cyan;
    public Color _movmentColor = Color.green;

    public Transform _destructionRocks;

    public AudioClip[] _deathSounds;
    public AudioClip _spawnSound;

    public TextMeshPro _instructionText;
    protected float _instructionTextTimer = 0.0f;

    public int _sacrificeCount = 0;

    protected List<BasePerson> _people = new List<BasePerson>();
    protected List<BasePerson> _selectedPeople = new List<BasePerson>();

    protected Camera _mainCamera;
    protected Plane _groundPlane;

    protected Vector3 _mouseWorldPos;
    protected bool _firstClickCompleted = false;

    public enum GamePhase
    {
        Title,
        Game,
        Finale,
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
        _instructionText.alpha = 0.0f;

        EventManager.OnStartGame.Register(OnGameStart);
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

        switch(_gamePhase)
        {
            case GamePhase.Game:
                UpdatePlayerControls();
                if(_firstClickCompleted)
                {
                    EnvironmentManager.Instance.UpdateEnvironmentScrolling();
                }

                if (_instructionText != null)
                {
                    if(_firstClickCompleted)
                    {
                        _instructionText.alpha = Mathf.Lerp(_instructionText.alpha, 0.0f, Time.deltaTime * 2.0f);
                    }
                    else
                    {
                        _instructionTextTimer += Time.deltaTime;
                        _instructionText.alpha = Mathf.Abs(Mathf.Sin(_instructionTextTimer));
                    }
                }

                if(_people.Count <= 0)
                {
                    TriggerLose();
                }
                else if(EnvironmentManager.Instance._endReached)
                {
                    TriggerFinale();
                }
                break;
            case GamePhase.Finale:
                UpdatePlayerControls();
                if(_people.Count <= 0)
                {
                    TriggerWin();
                }
                break;
            case GamePhase.Lose:
                HideMovementLine();
                break;
            case GamePhase.Win:
                HideMovementLine();
                break;
        }
    }


    protected void InitGame()
    {
        EnvironmentManager.Instance.Init();

        // spawn people
        for (int i = 0; i < _numStartingPeople; ++i)
        {
            SpawnPerson(new Vector3(Random.Range(-100, 100), Random.Range(250, 350), 0));
        }
    }

    protected void OnGameStart()
    {
        _gamePhase = GamePhase.Game;
        _selectionIndicator.gameObject.SetActive(true);
        _selectionIndicator.transform.localScale = Vector3.zero;
        StopPeopleMovement();
    }

    protected void TriggerFinale()
    {
        _gamePhase = GamePhase.Finale;
        _sacrificeCount = 0;
        _destructionRocks.DOMoveY(-100, 1.0f, true).SetEase(Ease.InBack);
        EventManager.OnFinale.Dispatch();
    }

    protected void TriggerLose()
    {
        _gamePhase = GamePhase.Lose;
        StartPeopleMovement();
        EventManager.OnLoseGame.Dispatch();
    }

    protected void TriggerWin()
    {
        _gamePhase = GamePhase.Win;
        StartPeopleMovement();
        EventManager.OnWinGame.Dispatch();
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
        //_selectedPeople.Add(person);
        
        return person;
    }


    public void RemovePerson(BasePerson person)
    {
        _people.Remove(person);
        _selectedPeople.Remove(person);

        AudioManager.Instance.PlayOneShot(_deathSounds[Random.Range(0, _deathSounds.Length)]);
        _sacrificeCount++;
    }

    protected void StartPeopleMovement()
    {
        for (int i = 0; i < _selectedPeople.Count; ++i)
        {
            _selectedPeople[i].SetColor(_movmentColor);
        }

        if(_selectedPeople.Count > 0)
        {
            if(!_firstClickCompleted)
            {
                _firstClickCompleted = true;
                _destructionRocks.DOMoveY(0, 1.0f, true).SetEase(Ease.OutBack);
            }
            _movementLine.gameObject.SetActive(true);
        }

        _selectionIndicator.transform.DOScale(0.0f, 0.33f).SetEase(Ease.InBack);
    }

    protected void StopPeopleMovement()
    {
        for (int i = 0; i < _selectedPeople.Count; ++i)
        {
            _selectedPeople[i].Stop();
        }

        _selectionIndicator.transform.DOScale(1.0f, 0.33f).SetEase(Ease.OutBack);
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
            BasePerson person = _selectedPeople[i];
            if(!person._isAlive)
            {
                continue;
            }

            float dist = Vector3.Distance(person.transform.position, _mouseWorldPos);
            float speedMultiplier = Mathf.Max(0.5f, dist / _speedMultiplierDist);

            person.MoveTowards(_mouseWorldPos, speedMultiplier);
            center += person.transform.position;
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

        HideMovementLine();
    }

    protected void HideMovementLine()
    {
        if (_movementLine.gameObject.activeSelf)
        {
            Vector3 head = _movementLine.GetPosition(0);
            Vector3 tail = _movementLine.GetPosition(1);
            _movementLine.SetPosition(1, Vector3.Lerp(tail, head, Time.deltaTime * 10.0f));
            if (Vector3.SqrMagnitude(head - tail) < 1f)
            {
                _movementLine.gameObject.SetActive(false);
            }
        }
    }
}
