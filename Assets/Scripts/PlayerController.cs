using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Spaceship
{
    Vector2 direction = Vector2.zero; // Holds direction of movement. 0,0 by default


    // Updates direction of movement
    public void InputMove(InputAction.CallbackContext context)
    {
        direction  = context.ReadValue<Vector2>();
    }

    // Update, called every frame
    new public void Update()
    {
        base.Update(); // Call the child class update
        transform.rotation = Quaternion.identity; // Resets rotation (for tilt) to 0 before every move
        Move(direction); // Move according to direction

    }

    // Called when dodge left action is performed
    public void InputDodgeLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Dodge(false); // Calls dodge function with isRight as false
        }
    }

    // Called when dodge right action is performed
    public void InputDodgeRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Dodge(true); // Calls dodge function with isRight as true
        }
    }

    // Called when fire button is pressed
    public void InputFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Fire(); // Calls fire function in base class
        }
    }

    // Pause the game when button pressed.
    public void InputPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Pause Game");
        }
    }
}
