using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IInputManager
{
    public event Action<Vector2> OnPointerMove;
    public event Action OnPrimaryConfirm;
    public event Action OnSecondaryConfirm;
    public event Action OnCancel;

    private Vector3 _currentMousePosition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnCancel?.Invoke();
        }

        if (EventSystem.current.IsPointerOverGameObject()) return;


        Vector3 mousePosition = Input.mousePosition;

        if (mousePosition != _currentMousePosition)
        {
            _currentMousePosition = mousePosition;
            OnPointerMove?.Invoke(new Vector2(mousePosition.x, mousePosition.y));
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnPrimaryConfirm?.Invoke();
        }

        else if (Input.GetMouseButtonDown(1))
        {
            OnSecondaryConfirm?.Invoke();
        }

    }
}
