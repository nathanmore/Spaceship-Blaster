using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Spaceship
{
    Vector2 direction = Vector2.zero; // Holds direction of movement. 0,0 by default

    bool performDodge = false;
    bool isRight = false;
    float dodgeRot = 0.0f;

    // Updates direction of movement
    public void InputMove(InputAction.CallbackContext context)
    {
        direction  = context.ReadValue<Vector2>();
    }

    // Update, called every frame
    new public void Update()
    {
        base.Update(); // Call the child class update

        if(performDodge == true) // Execute a dodge if performDodge tag is on
        {
            if (dodgeRot < 300) // Will rotate (roll) 300 degrees max, then reset to 0
            {
                Dodge(isRight);
                dodgeRot += 15; // To animate, 15 degree rotation per frame
            }
            else
            {
                dodgeRot = 0.0f; // Completed one dodge/barrel roll, reset rotation back to 0
                transform.rotation = Quaternion.identity;
                performDodge = false;
            }
        }
        else // Did not need to perform a dodge, check for movement
        {
            transform.rotation = Quaternion.identity; // Resets rotation (for tilt) to 0 before every move
            Move(direction); // Move according to direction
        }

    }

    // Called when dodge left action is performed
    public void InputDodgeLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            performDodge = true; // Turn on tag so a dodge is performed on next call to Update
            isRight = false; // Call Dodge function with isRight as false (left dodge)
        }
    }

    // Called when dodge right action is performed
    public void InputDodgeRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            performDodge = true;
            isRight = true;
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
