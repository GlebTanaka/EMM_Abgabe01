using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;

    private Vector3 _cameraOffset;
    private Space _offsetRotated = Space.Self;

    public bool lookAtPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - playerTransform.position;
    }

    void Follow()
    {
        if (_offsetRotated == Space.Self)
        {
            transform.position = playerTransform.TransformPoint(_cameraOffset);
        }
        else
        {
            Vector3 newPos = playerTransform.position + _cameraOffset;
            transform.position = Vector3.Slerp(transform.position, newPos, 0.5f);
        }

        if (lookAtPlayer)
        {
            transform.LookAt(playerTransform);
        }
        else
        {
            transform.rotation = playerTransform.rotation;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        Follow();
    }
}