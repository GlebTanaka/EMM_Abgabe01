using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomizeRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RandomYRotation();
    }
    
    void RandomYRotation()
    {
        // Rotate only over Y Aches
        Quaternion randYRotation = Quaternion.Euler(0,Random.Range(0,360),0);
        transform.rotation = randYRotation;
    }

    // Update is called once per frame
    /*void Update()
    {
        if (MenuScript.IsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            // make the Object spin oder Y Aches
            transform.rotation *= Quaternion.Euler(0, 2, 0);
        }
    }*/

    private void FixedUpdate()
    {
        if (MenuScript.IsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            // make the Object spin oder Y Aches
            transform.rotation *= Quaternion.Euler(0, 2, 0);
        }
        
    }
}
