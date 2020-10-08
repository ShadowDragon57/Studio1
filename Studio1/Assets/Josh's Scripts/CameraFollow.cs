using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Transform camTransform;

    //The distance away from the object
    [SerializeField]
    private Vector3 offset = new Vector3(0, 2, -10);
    public Quaternion rotation;

    //Holds the value for the rotation
    public float currentX = 0.0f;

    //Manages how quickly the mouse rotates
    //As of testing on the 8/10/2020, 200 appears to be the most optimal sensitivity
    [SerializeField]
    private float mouseSensitivity = 200f;

    //Check if button is pressed
    [SerializeField]
    private bool rightButtonDown = false;

    //Miscellanious Vars
    public bool rotationChanged = false;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;

        if (Input.GetMouseButtonDown(1))
        {
            rightButtonDown = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            rightButtonDown = false;
            rotationChanged = true;
        }

        if (rightButtonDown == true)
        {
            //Adding the time.deltatime at the end allows this to be frame independent
            currentX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        }

    }

    void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 2, -5);
        rotation = Quaternion.Euler(0, currentX, 0);
        camTransform.position = player.position + rotation * dir;
        camTransform.LookAt(player.position);
    }
}
