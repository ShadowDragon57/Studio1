using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Transform camTransform;
    public Vector3 offset;

    private Camera cam;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }
}
