using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    private SpawnerManager _spawnerManager;
    private ShapeManager _activeShape;
    private BoardManager _boardManager;

    [Header("Counters")] [Range(0.01f, 1f)] [SerializeField]
    private float _moveDownTime = 1f;

    [Range(0.01f, 1f)] [SerializeField] private float _levelMultiplyer;

    private float _moveDownCounter = 0f,
        _moveDownLevelCounter,
        _horizontal,
        _rightLeftMovePressCounter,
        _rightLeftRotatePressCounter,
        _downButtonPressCounter;

    [Range(0.01f, 1f)] [SerializeField] private float _moveButtonPressingTime = .25f;
    [Range(0.01f, 1f)] [SerializeField] private float _rotateButtonPressingTime = .25f;
    [Range(0.01f, 1f)] [SerializeField] private float _downButtonPressingTime = .25f;

    public bool _gameOver = false;
    [SerializeField] private GameObject _gameOverPanel; 
    //[SerializeField] private Sprite _nextShapeSprite;

    private ScoreManager _scoreManager;

    private FollowUpTheShapeManager _followUpTheShapeManager;

    private bool isStarted = false;

    public ParticleManager[] LevelUpEffects;

    private void Awake()
    {
        _spawnerManager = FindObjectOfType<SpawnerManager>();
        _boardManager = FindObjectOfType<BoardManager>();
        _scoreManager = FindObjectOfType<ScoreManager>();
        _followUpTheShapeManager = FindObjectOfType<FollowUpTheShapeManager>();
        if (_gameOverPanel)
        {
            _gameOverPanel.SetActive(false);
        }
    }

    // Start is called before the first frame update
    public void StartTheGame()
    {
        isStarted= true;
        _spawnerManager.MakeNullFNC();

        if (_spawnerManager)
        {
            _activeShape = _spawnerManager.CreateAShape();
            _activeShape.transform.position = VectorToIntFNC(_activeShape.transform.position);
        }
        _moveDownLevelCounter = _moveDownTime;
    }

    private void Update()
    {
        if (!_boardManager || !_spawnerManager || !_activeShape || _gameOver || !_scoreManager)
        {
            return;
        }
        InputControlFNC();
    }

    private void LateUpdate()
    {
        if (_followUpTheShapeManager && isStarted)
        {
            _followUpTheShapeManager.CreateFollowUpShapeFNC(_activeShape, _boardManager);
        }
    }

    private void InputControlFNC()
    {
        if ((Input.GetKey("right") && Time.time > _rightLeftMovePressCounter) || Input.GetKeyDown("right"))
        {
            MoveRight();
        }
        else if ((Input.GetKey("left") && Time.time > _rightLeftMovePressCounter) || Input.GetKeyDown("left"))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown("up") && Time.time > _rightLeftRotatePressCounter)
        {
            RotateShape();
        }
        else if (((Input.GetKey("down") && Time.time > _downButtonPressCounter)) || Time.time > _moveDownCounter)
        {
            MoveDown();
        }
    }

    public void MoveDown()
    {
        _moveDownCounter = Time.time + _moveDownLevelCounter;
        _downButtonPressCounter = Time.time + _downButtonPressingTime;
        
        if (_activeShape)
        {
            _activeShape.MoveDownFNC();
            if (!_boardManager.InValidPosition(_activeShape))
            {
                if (_boardManager.IsOutOfBounds(_activeShape))
                {
                    _activeShape.MoveUpFNC();
                    _gameOver = true;
                    SoundManager.instance.StopBackgroundMusic();
                    if (_gameOverPanel)
                    {
                        _gameOverPanel.SetActive(true);
                    }

                    SoundManager.instance.PlaySoundEffectFNC(6);
                }
                else
                {
                    SoundManager.instance.PlaySoundEffectFNC(5);
                    PlacedFNC();
                }
            }
        }
    }

    public void RotateShape()
    {
        _activeShape.RotateRightFNC();
        _rightLeftRotatePressCounter = Time.time + _rotateButtonPressingTime;
        if (!_boardManager.InValidPosition(_activeShape))
        {
            SoundManager.instance.PlaySoundEffectFNC(1);
            _activeShape.RotateLeftFNC();
        }
        else
        {
            SoundManager.instance.PlaySoundEffectFNC(3);
        }
    }

    public void MoveLeft()
    {
        _activeShape.MoveLeftFNC();
        _rightLeftMovePressCounter = Time.time + _moveButtonPressingTime;
        if (!_boardManager.InValidPosition(_activeShape))
        {
            SoundManager.instance.PlaySoundEffectFNC(1);
            _activeShape.MoveRightFNC();
        }
        else
        {
            SoundManager.instance.PlaySoundEffectFNC(3);
        }
    }

    public void MoveRight()
    {
        _activeShape.MoveRightFNC();
        _rightLeftMovePressCounter = Time.time + _moveButtonPressingTime;
        if (!_boardManager.InValidPosition(_activeShape))
        {
            SoundManager.instance.PlaySoundEffectFNC(1);
            _activeShape.MoveLeftFNC();
        }
        else
        {
            SoundManager.instance.PlaySoundEffectFNC(3);
        }
    }

    private void PlacedFNC()
    {
        _rightLeftMovePressCounter = Time.time;
        _rightLeftRotatePressCounter = Time.time;
        _activeShape.MoveUpFNC();
        _boardManager.BringTheShapeInToTheGridFNC(_activeShape);
        if (_spawnerManager)
        {
            _activeShape = _spawnerManager.CreateAShape();
        }

        if (_followUpTheShapeManager)
        {
            _followUpTheShapeManager.FollowUpShapeDestroyFNC();
        }

        _boardManager.ClearAllLinesFNC();
        if (_boardManager._completedLines > 0)
        {
            if (_scoreManager)
            {
                _scoreManager.LineScore(_boardManager._completedLines);
            }

            if (_scoreManager.isLevelPassed)
            {
                if (_scoreManager.level % 5 == 0)
                    SoundManager.instance.PlaySoundEffectFNC(8);
                else
                    SoundManager.instance.PlaySoundEffectFNC(2);
                
                StartCoroutine(LevelUpEffectFnc());
                _moveDownLevelCounter = _moveDownTime - Mathf.Clamp(((float)_scoreManager.level - 1) * _levelMultiplyer, 0.05f, 1f);
            }
            else
            {
                if (_boardManager._completedLines > 1)
                {
                    SoundManager.instance.PlayRandomVocalSoundFNC();
                }
            }
            SoundManager.instance.PlaySoundEffectFNC(4);
        }
    }

    Vector2 VectorToIntFNC(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }

    IEnumerator LevelUpEffectFnc()
    {
        yield return new WaitForSeconds(0.3f);
        int counter = 0;
        while (counter<LevelUpEffects.Length)
        {
            LevelUpEffects[counter].PlayEffectFNC();
            yield return new WaitForSeconds(0.3f);
            counter++;
        }
        
    }
}