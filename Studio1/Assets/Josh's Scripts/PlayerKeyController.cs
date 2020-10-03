using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyController : MonoBehaviour
{
    //References
    Abilities abilities;
    FaithCalculator faith;

    public Rigidbody rb;

    //Movement Speed Vars
    [SerializeField]
    private float leftSpeed, rightSpeed, forwardSpeed, backSpeed, sprintSpeed, airMovement;

    //Timer Var
    private float timer;

    void Start()
    {
        //Defining Speed Vars
        leftSpeed = 500f;
        rightSpeed = 500f;
        forwardSpeed = 1000f;
        backSpeed = 1000f;
        sprintSpeed = 1500f;
        airMovement = 200f;
    }

    void Update()
    {
        //Movement Keys
        if (Input.GetKey(KeyCode.W))
        {
            rb.position += Vector3.forward * Time.deltaTime * forwardSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.position += Vector3.left * Time.deltaTime * leftSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.position += Vector3.right * Time.deltaTime * rightSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.position += Vector3.back * Time.deltaTime * backSpeed;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            if (faith.faithCount == 0)
            {

            }

            if (faith.faithCount <= 20)
            {

            }
        }

        if (Input.GetKey(KeyCode.E))
        {

        }
    }
}
