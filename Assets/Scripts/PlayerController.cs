using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Spaceship
{
    public void InputMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            Move(direction);
        }
    }

    public void InputDodgeLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Dodge(false);
        }
    }

    public void InputDodgeRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Dodge(true);
        }
    }

    public void InputFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Fire();
        }
    }

    public void InputPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Pause Game");
        }
    }
}
