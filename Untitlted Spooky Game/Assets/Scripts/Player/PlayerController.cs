using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb; //reference player rigidbody
    public GameObject camHolder; //reference player game object 

    private Vector2 move, look; //for movement and looking 

    public bool grounded; //to check if the player grounded or not
    public float speed; //player movement speed

    public float sensitivity; //player mouse sensitity speed
    public float controllerSensitivty; //player controller sensitivty for camera movement 
    public float currentSensitivity; //the players current sensitivity 
    private float lookRotation; //keep track of current look rotation
    public float maxForce; //the max force that can be applied on the player

    public float normalHeight = 2.0f;
    public float crouchHeight = 1.0f;

    public CapsuleCollider capsule;
    private bool isCrouching = false;


    public void OnMove(InputAction.CallbackContext context)  
    {
       move = context.ReadValue<Vector2>(); //this detects input along the vector and allows movement 
    }

    public void OnLook(InputAction.CallbackContext context) //Uses the new input system to call function when button pressed
    {
        look = context.ReadValue<Vector2>(); //this detects input along the vector and allows look which controls where the player sees 

        InputDevice device = context.control.device; //this checks what device provided the input

        if (device is Mouse)
        {
            currentSensitivity = sensitivity; //if mouse set the sensitivity to mouse
        }
        else if (device is Gamepad)
        {
            currentSensitivity = controllerSensitivty; //if controller set the sensitivity to controller
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToggleCrouch();
        }
    }

    //Potato Code. (2022, May 15). How to Make a Rigidbody Player Controller with Unity's Input System[Video]. Youtube. https://www.youtube.com/watch?v=1LtePgzeqjQ

    private void FixedUpdate()
    {
        if (grounded) //if the player is on the ground
        {
          Move();  //call the move function
        }        
    }

   
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //locks the cursor when the game begins
        Cursor.visible = false; //ensure the cursor is not visible 
    }

    private void Move()
    {
        Vector3 currentVelocity = rb.velocity; //find the players velocity
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y); //take our movement and turn it to vector to move our character
        targetVelocity *= speed; //this controls how fast the character moves

        targetVelocity = transform.TransformDirection(targetVelocity); //ensure the velocity is being applied in the correct direction

        Vector3 velocityChange = (targetVelocity - currentVelocity); //calculate the force that will applied on the player
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z); //apply the forces on the velocitychange for x and z

        Vector3.ClampMagnitude(velocityChange, maxForce); //limit the force that can be added on the player  
        rb.AddForce(velocityChange, ForceMode.VelocityChange); //applies force to the rigidbody ignoring its mass
    }

    private void ToggleCrouch()
    {
        if (isCrouching)
        {
            // Stand up
            capsule.height = normalHeight;
            isCrouching = false;
        }
        else
        {
            // Crouch down
            capsule.height = crouchHeight;
            isCrouching = true;
        }
    }


    void NormalLook()
    {
        transform.Rotate(Vector3.up * look.x * currentSensitivity); //turns the camera around at the sensitivity set 

        lookRotation += (-look.y * currentSensitivity); //ensure that the the player up and down look is at the sensitivity set
        lookRotation = Mathf.Clamp(lookRotation, -90, 90); //clamps the rotation to -90 and 90 so it restricted between these two values
        camHolder.transform.eulerAngles = new Vector3(lookRotation, camHolder.transform.eulerAngles.y, camHolder.transform.eulerAngles.z); //this sets the rotatation of camera holder
                                                                                                                                           //so that it stays unchanged on the y and z and
                                                                                                                                           //only rotates on the x
    }
  

    private void LateUpdate()
    {
        NormalLook(); //call the normal look function
    }

    public void SetGrounded(bool state) //this is used to allow me to check for grounded in a collsion script
    {
        grounded = state;
    }

  
}
