using System;
using JetBrains.Annotations;
using UnityEngine;
using TMPro;


public class TouchManager : MonoBehaviour
{
    public delegate void OnTouchDelegate(Vector2 position);

    public static event OnTouchDelegate OnTouch;
    public static event OnTouchDelegate OnTouchEnded;
    private Vector2 _touchPosition;
    public int minimumSwipeDistance = 20;
    [SerializeField] TextMeshProUGUI _debugTestTex1, _debugTestTex2;
    public bool isDebugUsing = false;


    void TouchStartEventTrigger()
    {
        OnTouch?.Invoke(_touchPosition);
    }

    void TouchEndedEventTrigger()
    {
        OnTouchEnded?.Invoke(_touchPosition);
    }

    private void Start()
    {
        PrintDebugTestFNC("", "");
    }

    private void Update()
    {
        MoveToDirectionsFNC();
    }

    void PrintDebugTestFNC(string text1, string text2)
    {
        _debugTestTex1.gameObject.SetActive(isDebugUsing);
        _debugTestTex2.gameObject.SetActive(isDebugUsing);

        if (_debugTestTex1 && _debugTestTex2)
        {
            _debugTestTex1.text = text1;
            _debugTestTex2.text = text2;
        }
    }

    string SwipeActionFNC(Vector2 position)
    {
        string direction = "";
        if (Mathf.Abs(position.x) > Mathf.Abs(position.y))
        {
            direction = (position.x > 0) ? "Right" : "Left";
        }
        else
        {
            direction = (position.y > 0) ? "Up" : "Down";
        }
        return direction;
    }

    private void MoveToDirectionsFNC()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                _touchPosition = Vector2.zero;
                PrintDebugTestFNC("", "");
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                _touchPosition = touch.deltaPosition;
                if (_touchPosition.magnitude > minimumSwipeDistance)
                {
                    TouchStartEventTrigger();
                    PrintDebugTestFNC("Touch Debug", _touchPosition.ToString() + " " + SwipeActionFNC(_touchPosition));
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                TouchEndedEventTrigger();
                _touchPosition = Vector2.zero;
                PrintDebugTestFNC("", "");
            }
        }
    }
}