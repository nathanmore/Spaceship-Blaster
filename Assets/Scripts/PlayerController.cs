using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : Spaceship
{
    [SerializeField]
    private float doublePointsDuration = 10.0f;

    public SO_ScoreData ScoreAsset;

    private Vector2 direction = Vector2.zero; // Holds direction of movement. 0,0 by default
    private bool performDodge = false; // Tracks whether or not a dodge should be performed in this frame
    private bool isRight = false; // If dodge is being performed, keeps track of whether it is to the left or right
    private int dodgeRot = 0; // Tracks how much the ship has been rotated if dodge roll is happening
    private bool gamePaused = false;

    // Public accessor for shipData, for power-ups
    public SO_SpaceshipData ShipData { get { return shipData; } }

    // Updates direction of movement
    public void InputMove(InputAction.CallbackContext context)
    {
        direction  = context.ReadValue<Vector2>();
    }

    // Update, called every frame
    new public void Update()
    {
        base.Update(); // Call the child class update

    }

    public void FixedUpdate()
    {
        if (performDodge == true) // Execute a dodge if performDodge tag is on
        {
            if (dodgeRot < 350) // Will rotate (roll) 350 degrees max, then reset to 0
            {
                Dodge(isRight, shipData.DodgeRotation);
                dodgeRot += shipData.DodgeRotation; // To animate, there is a 5 degree rotation per frame
            }
            else
            {
                dodgeRot = 0; // Completed one dodge/barrel roll, reset rotation back to 0
                shipModelTransform.rotation = baseRotation;
                performDodge = false;
            }
        }
        else // Did not need to perform a dodge, check for movement
        {
            shipModelTransform.rotation = baseRotation; // Resets rotation (for tilt) to base before every move
            Move(direction); // Move according to direction
            Tilt(direction, 25); // Tilt 25 degrees around ship's y-axis while moving
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
            SoundManager.PlaySound(SoundManager.Sound.playerAttack);
        }
    }

    // Pause the game when button pressed.
    public void InputPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (gamePaused == false)
            {
                Time.timeScale = 0.0f;
                gamePaused = true;
                SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
            }
            else if (gamePaused == true)
            {
                SceneManager.UnloadSceneAsync("PauseMenu");
                Time.timeScale = 1.0f;
                gamePaused = false;
            }
        }
    }

    public void ActivateDoublePoints()
    {
        StartCoroutine(DoublePointsCoroutine());
    }

    public IEnumerator DoublePointsCoroutine()
    {
        ScoreAsset.SetDoublePointsBool(true);

        yield return new WaitForSeconds(doublePointsDuration);

        ScoreAsset.SetDoublePointsBool(false);
    }
}
