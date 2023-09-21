using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TouchManager : MonoBehaviour
{
    private PlayerInput playerInput;

    private InputAction touchPositionAction;

    private InputAction touchPressAction;

    public Transform target;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        touchPositionAction = playerInput.actions["Position"];
        touchPressAction = playerInput.actions["Press"];
    }
    private void OnEnable()
    {
        touchPressAction.performed += TouchPressed;
        
    }

    private void OnDisable()
    {
        touchPressAction.performed -= TouchPressed;

    }


    // Update is called once per frame


    void TouchPressed(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        Vector2 position = Camera.main.ScreenToWorldPoint(touchPositionAction.ReadValue<Vector2>());
        var ray = new Ray2D(position, Vector2.zero);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            Debug.Log($"Collision name : {hit.collider.name}, {hit.collider.transform.position}");
            target = hit.collider.transform;
        }
        Debug.Log($"Pressed : {position}");
    }


}
