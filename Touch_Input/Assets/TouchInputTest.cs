using System;
using UnityEngine;

public class TouchInputTest : MonoBehaviour
{
    // To cache references for better perfromance
    private Camera mainCamera;
    private InputManager inputManager;


    private float startTouchTime;

    private void Awake()
    {
        inputManager = InputManager.Instance;
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        // Subscribe to the OnStartTouchEvent
        inputManager.OnStartTouch += Move;
        inputManager.OnEndTouch += GetTime;
        inputManager.OnTouchMoved += Drag;
    }

    private void GetTime(Vector2 position, float time)
    {
        float duration = time - startTouchTime;
        Debug.Log("Duration: " + duration);
    }

    private void OnDisable()
    {
        // Unsubscribe to the OnStartTouchEvent
        inputManager.OnStartTouch -= Move;
        inputManager.OnEndTouch -= GetTime;
        inputManager.OnTouchMoved -= Drag;
    }
    
    public void Move(Vector2 screenPosition, float time)
    {
        transform.position = GetWorldCoordinates(screenPosition);
        startTouchTime = time;
    }

    public void Drag(Vector2 screenPosition, float time)
    {
        transform.position = GetWorldCoordinates(screenPosition);
    }

    Vector3 GetWorldCoordinates(Vector2 screenPos)
    {
        // We want to convert our screen position to world coordinates

        // For Z-axis, put the distance the camera has from the
        // plane you are trying to touch
        Vector3 screenCoord = new Vector3(screenPos.x, screenPos.y, mainCamera.nearClipPlane);

        Vector3 worldCoord = mainCamera.ScreenToWorldPoint(screenCoord);
        worldCoord.z = 0;

        return worldCoord;
    }
}
