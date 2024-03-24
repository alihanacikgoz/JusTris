using System;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using UnityEngine;
using TMPro;


public class TouchManager : MonoBehaviour
{
    public delegate void OnTouchDelegate(Vector2 position);

    public static event OnTouchDelegate OnDragEvent;
    public static event OnTouchDelegate OnSwipeEvent;
    public static event OnTouchDelegate OnTapEvent;
    private Vector2 _touchPosition;
    
    [Range(20,250)]
    public int minimumDragDistance = 5;
    [Range(20,250)]
    public int minimumSwipeDistance = 5;
    
    private float _tapMaxTime = 0f;
    public float tapTime = .1f;


    void TouchStartEventTrigger()
    {
        OnDragEvent?.Invoke(_touchPosition);
    }
    
    void TappedFNC()
    {
        OnTapEvent?.Invoke(_touchPosition);
    }

    void TouchEndedEventTrigger()
    {
        OnSwipeEvent?.Invoke(_touchPosition);
    }

    private void Update()
    {
        MoveToDirectionsFNC();
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
                _tapMaxTime = Time.time + tapTime;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                _touchPosition = touch.deltaPosition;
                if (_touchPosition.magnitude > minimumDragDistance)
                {
                    TouchStartEventTrigger();
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (_touchPosition.magnitude > minimumSwipeDistance)
                {
                    TouchEndedEventTrigger();
                }
                else if (Time.time < _tapMaxTime)
                {
                    TappedFNC();
                }
                _touchPosition = Vector2.zero;
            }
        }
    }
}