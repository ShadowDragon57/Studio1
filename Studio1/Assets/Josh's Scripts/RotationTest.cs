using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    //References
    public Transform cam;
    public Quaternion prevRotation;

    //Bools
    public bool rightButtonDown = false;


    // Start is called before the first frame update
    void Start()
    {
        prevRotation = cam.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if camera is rotating
        if (prevRotation != transform.rotation)
        {
            cam.rotation = prevRotation;
            Debug.Log("Blocking Rotation");
        }

        if (rightButtonDown == false)
        {

        }

        //Checks if right mouse button is down before allowing player to move the camera
        if (Input.GetMouseButtonDown(1))
        {
            rightButtonDown = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            rightButtonDown = false;
            prevRotation = cam.rotation;
        }
    }
}
