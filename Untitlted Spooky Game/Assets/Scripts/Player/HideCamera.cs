using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HideCamera : MonoBehaviour
{
    public float sensitivity; //player mouse sensitity speed
    public float controllerSensitivty; //player controller sensitivty for camera movement 
    public float currentSensitivity; //the players current sensitivity 
    private float lookRotation; //keep track of current look rotation
    private Vector2 look; //for movement and looking 
    public GameObject hideCamera;

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

    void NormalLook()
    {
        transform.Rotate(Vector3.up * look.x * currentSensitivity); //turns the camera around at the sensitivity set 

        lookRotation += (-look.y * currentSensitivity); //ensure that the the player up and down look is at the sensitivity set
        lookRotation = Mathf.Clamp(lookRotation, -90, 90); //clamps the rotation to -90 and 90 so it restricted between these two values
        hideCamera.transform.eulerAngles = new Vector3(lookRotation, hideCamera.transform.eulerAngles.y, hideCamera.transform.eulerAngles.z); //this sets the rotatation of camera holder
                                                                                                                                           //so that it stays unchanged on the y and z and
                                                                                                                                           //only rotates on the x
    }


    private void LateUpdate()
    {
        NormalLook(); //call the normal look function
    }
}
