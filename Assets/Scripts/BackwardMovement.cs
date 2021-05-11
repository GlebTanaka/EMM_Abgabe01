using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class BackwardMovement : MonoBehaviour
{
    public GameObject backwardBtnObj;

    private bool _backward;
    
    //Referenes
    [Header("References")] public Transform trans;

    public Transform modelTrans;

    public CharacterController CharacterController;

    //Movement
    [Header("Movement")] [Tooltip("Units moved per second at maximum speed.")]
    public float movespeed = 1;

    [Tooltip("Time, in seconds, to reach maximum speed.")]
    public float timeToMaxSpeed = .26f;

    private float VelocityGainPerSecond
    {
        get { return movespeed / timeToMaxSpeed; }
    }

    [Tooltip("Time, in seconds, to go from maximum speed to stationary.")]
    public float timeToLoseMaxSpeed = .2f;

    private float VelocityLossPerSecond
    {
        get { return movespeed / timeToLoseMaxSpeed; }
    }

    [Tooltip("Multiplier for momentum when attempting to move in a direction opposite the current traveling" +
             " direction (e.g. trying to move right when already moving left).")]
    public float reverseMomentumMultiplier = 2.2f;

    private Vector3 movementVelocity = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        backwardBtnObj = GameObject.Find("BackwardBtn");
        backwardBtnObj.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        backwardBtnObj.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
    }
    
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        _backward = true;

        Debug.Log("BTN backward Pressed");
       
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        _backward = false;
        Debug.Log("BTN backward Released");
    }
    
    private void Movement()
    {
        // if backward VirtualButton is pressed:
        if (_backward)
        {
            if (movementVelocity.z > 0) // if we're already moving forward
            {
                movementVelocity.z = Mathf.Max(0,
                    movementVelocity.z - VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
            }
            else // if we're moving back ar not moving at all
            {
                movementVelocity.z = Mathf.Max(-movespeed, movementVelocity.z - VelocityGainPerSecond * Time.deltaTime);
            }

        }
        else // if neither forward nor back are being held
        {
            // we must bring the Z velocity back to 0 over time.
            if (
                movementVelocity.z >
                0) // if we're moving up, decrease Z velocity by VelocityLossPerSecond, but don't go any lower than 0:
            {
                movementVelocity.z = Mathf.Max(0, movementVelocity.z - VelocityLossPerSecond * Time.deltaTime);
            }
            else // if we're moving down, increase Z velocity (back towards 0) by VelocityLossPerSecond, but don't go any higher than 0;
            {
                movementVelocity.z = Mathf.Min(0, movementVelocity.z + VelocityGainPerSecond * Time.deltaTime);
            }
        }
        // if the player is moving in either direction 
        if (movementVelocity.x != 0 || movementVelocity.z != 0)
        {
            // applying the movement velocity:
            CharacterController.Move(movementVelocity * Time.deltaTime);
            
            // Keeping the model holder rotated towards the last movement direction:
            modelTrans.rotation = Quaternion.Slerp(modelTrans.rotation,Quaternion.LookRotation(movementVelocity),.18f );
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
}
