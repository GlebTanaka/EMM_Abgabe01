using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    
    private float _angle = 0.0f;
    
    void Move()
    {
        float vertical = Input.GetAxis("Vertical");
        //Move the character in its own forward direction while taking acceleration and time into account
        transform.Translate(transform.forward * (vertical * Time.deltaTime), Space.World);
    }

    void Rotate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        _angle += horizontal * Time.deltaTime;
        Vector3 targetDirection2 = new Vector3(Mathf.Sin(_angle), 0, Mathf.Cos(_angle));
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(targetDirection2), .18f);
        
    }
    
    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }
}
