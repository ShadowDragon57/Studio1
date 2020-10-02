using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMouse : MonoBehaviour
{
    public Transform lookAt;
    public Transform camTransform;

    private Camera cam;

    //Controls how far away the camera is from the player as well as how 
    private float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;

    //Controls how quickly the camera follows the mouse
    private float sensitivityX = 4.0f;
    private float sensitivityY = 1.0f;
    private float sensitivity = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        camTransform = transform;
        cam = Camera.main;
    }

    void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");
        //distance += Input.GetAxis("Mouse ScrollWheel") * sensitivity;

       

    }
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, 1);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }
}
