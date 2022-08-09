using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : Spaceship
{
    [SerializeField]
    private float doublePointsDuration = 10.0f;
    [SerializeField]
    private float xBounds = 13f;
    [SerializeField]
    private float yBounds = 7f;

    public SO_ScoreData ScoreAsset;

    private Vector2 direction = Vector2.zero; // Holds direction of movement. 0,0 by default
    private bool performDodge = false; // Tracks whether or not a dodge should be performed in this frame
    private bool isRight = false; // If dodge is being performed, keeps track of whether it is to the left or right
    private int dodgeRot = 0; // Tracks how much the ship has been rotated if dodge roll is happening
    private bool gamePaused = false;
    private bool canFire = true;

    // Public accessor for shipData, for power-ups
    public SO_SpaceshipData ShipData { get { return shipData; } }

    // Updates direction of movement
    public void InputMove(InputAction.CallbackContext context)
    {
        direction  = context.ReadValue<Vector2>();
    }

    public void FixedUpdate()
    {
        if (performDodge == true) // Execute a dodge if performDodge tag is on
        {
            if (dodgeRot < 350) // Will rotate (roll) 350 degrees max, then reset to 0
            {
                if (dodgeRot > (350 - 2*shipData.DodgeRotation)) // When ship is two rotations away from completing dodge, enable firing weapon
                {
                    canFire = true; // Enable firing weapon
                }
                else
                {
                    canFire = false; // Disable firing weapon during dodge
                }

                if (dodgeRot <= 180)
                {
                    Dodge(isRight, shipData.DodgeRotation, -1);
                }
                else
                {
                    Dodge(isRight, shipData.DodgeRotation, 1);
                }
                //Dodge(isRight, shipData.DodgeRotation);
                dodgeRot += shipData.DodgeRotation; // To animate, there is a 5 degree rotation per frame
            }
            else
            {
                if (transform.position.z != 0) // Extra check to make sure ship is in correct position
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                }

                dodgeRot = 0; // Completed one dodge/barrel roll, reset rotation back to 0
                shipModelTransform.rotation = baseRotation;
                performDodge = false;
                isDamageable = true; // Make vulnerable again when dodge is complete
            }
        }
        else // Did not need to perform a dodge, check for movement
        {
            shipModelTransform.rotation = baseRotation; // Resets rotation (for tilt) to base before every move

            // Make sure player cannot leave screen
            if (transform.position.x <= -xBounds && direction.x < 0)
            {
                direction.x = 0;
            } 
            if (transform.position.x >= xBounds && direction.x > 0)
            {
                direction.x = 0;
            }
            if (transform.position.y <= -yBounds && direction.y < 0)
            {
                direction.y = 0;
            }
            if (transform.position.y >= yBounds && direction.y > 0)
            {
                direction.y = 0;
            }
            
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
            if (canFire)
            {
                Fire(); // Calls fire function in base class
            }
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

    // Quickly move ship to right if isRight is true, move to left otherwise
    public void Dodge(bool isRight, int tiltRot, int zVal = 0)
    {
        if (movementEnabled)
        {
            isDamageable = false; // invulnerable when dodging

            if (isRight)
            {
                // Change ship's location
                Vector3 velocity = new Vector3(1, 0, zVal) * shipData.DodgeSpeed * Time.deltaTime;
                transform.position += velocity;
                // Change ship's rotation (in each frame, tilt 5 degrees to the right)
                Tilt(new Vector2(1, 0), tiltRot);
            }
            else
            {
                Vector3 velocity = new Vector3(-1, 0, zVal) * shipData.DodgeSpeed * Time.deltaTime;
                transform.position += velocity;

                Tilt(new Vector2(-1, 0), tiltRot);
            }

            if (transform.position.z > 0) // if dodge overshoots trajectory, reset to correct z position
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }

            // Make sure player cannot leave screen via dodging
            if (transform.position.x < -xBounds)
            {
                transform.position = new Vector3(-xBounds, transform.position.y, transform.position.z);
            }
            if (transform.position.x > xBounds)
            {
                transform.position = new Vector3(xBounds, transform.position.y, transform.position.z);
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
