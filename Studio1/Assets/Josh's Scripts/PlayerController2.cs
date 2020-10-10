using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float smoothVeloctiy;

    // Update is called once per frame
    void Update()
    {
        //Allows you to press the arrow keys or the wasd to move?
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Normalised ensures that if you press 2 buttons, it won't accelerate to be twice as fast
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //Magnitude checks if you're moving in any direction
        if (direction.magnitude >= 0.1f)
        {
            //Determines the angle
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothTime, smoothVeloctiy);
            //Normalising in this instance makes it a gradual change
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f).normalized;

            if (Input.GetKey(KeyCode.W) /*&& fDown != true && grounded == true*/)
            {
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A) /*&& grounded == true*/)
            {
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.left;
                controller.Move(moveDir * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S) /*&& grounded == true*/)
            {
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.back;
                controller.Move(moveDir * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D) /*&& grounded == true*/)
            {
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.right;
                controller.Move(moveDir * speed * Time.deltaTime);
            }
        }
    }
}
