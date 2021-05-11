using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class LeftMovement : MonoBehaviour
{
    public GameObject leftBtnObj;

    private bool _left;
    
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
        leftBtnObj = GameObject.Find("LeftBtn");
        leftBtnObj.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        leftBtnObj.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
    }
    
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        _left = true;
        Debug.Log("BTN left Pressed");
       
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        _left = false;
        Debug.Log("BTN left Released");
    }
    
    private void Movement()
    {
        // if left VirtualButton is pressed:
        if (_left)
        {
            if (movementVelocity.x > 0) // if we're already moving right
            {
                movementVelocity.x = Mathf.Max(0,
                    movementVelocity.x - VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
            }
            else // if we're moving left or not moving at all
            {
                movementVelocity.x = Mathf.Max(-movespeed, movementVelocity.x - VelocityGainPerSecond * Time.deltaTime);
            }

        }
        else // if neither right nor left are being held
        {
            // we must bring the X velocity back to 0 over time.
            if (movementVelocity.x > 0) // if we're moving right, ...
            {
                movementVelocity.x = Mathf.Max(0, movementVelocity.x - VelocityLossPerSecond * Time.deltaTime);
            }
            else // if we're moving left, ..
            {
                movementVelocity.x = Mathf.Min(0, movementVelocity.x + VelocityLossPerSecond * Time.deltaTime);
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
