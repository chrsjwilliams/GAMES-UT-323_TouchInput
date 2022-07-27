using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;

    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;

    public delegate void TouchMovedEvent(Vector2 position, float time);
    public event TouchMovedEvent OnTouchMoved;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += FingerUp;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove += FingerMoved;
    }

    

    private void OnDisable()
    {
        TouchSimulation.Disable();
        EnhancedTouchSupport.Disable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp -= FingerUp;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove -= FingerMoved;
    }


    private void FingerDown(Finger finger)
    {
        Debug.Log("Touch position " + finger.screenPosition);

        if (OnStartTouch != null)
        {
            OnStartTouch(finger.screenPosition, Time.time);
        }
    }

    private void FingerUp(Finger finger)
    {
        Debug.Log("Touch ended");

        if (OnEndTouch != null)
        {
            OnEndTouch(finger.screenPosition, Time.time);
        }
    }

    private void FingerMoved(Finger finger)
    {
        if(OnTouchMoved != null)
        {
            OnTouchMoved(finger.screenPosition, Time.time);
        }
    }
}
