﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyController : MonoBehaviour
{
    //Temp Vars
    public int Karma = 0;

    //References
    public Abilities abilities;
    public Rigidbody rb;

    //Timers
    float timer1 = 0;
    float timer2 = 0;

    //Movement Speeds
    float forwardForce = 1000f;
    float backForce = -1000f;
    float leftForce = -500f;
    float rightForce = 500f;

    float airMovementSpeed = 200f;

    float sprintSpeed = 1500f;

    //Checkers
    public bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Checks if the gameObject is touching the floor
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }

        else
        {
            grounded = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Movement Commands
        //Checks in the player is grounded before allowing them to move
        if (grounded == true)
        { 
            if (Input.GetKey(KeyCode.W))
            {
                //The brackets contain x, y, z
                //X refers to left and right (generally) and z, front and back
                //Y refers to height e.g Jumping
                rb.AddForce(0, 0, forwardForce * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(leftForce * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(0, 0, backForce * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(rightForce * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.AddForce(0, 0, sprintSpeed * Time.deltaTime);
            }
        }

        //Determines movement speed in the air
        if (grounded == false)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(0, 0, airMovementSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(airMovementSpeed * Time.deltaTime, 0, 0);
            }

            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(0, 0, airMovementSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(airMovementSpeed * Time.deltaTime, 0, 0);
            }
        }

        //Ability Activations
        switch (Karma)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.Q) && timer1 < 0)
                {
                    //Causes Ability
                    abilities.Ability1();

                    //Abilty CoolDown
                    timer1 = abilities.abiCoolDown1;
                }

                if (timer1 > -1)
                {
                    timer1 -= 1 * Time.deltaTime;
                    Debug.Log(timer1);
                }

                if (Input.GetKeyDown(KeyCode.E) && timer2 < 0)
                {
                    //Causes Ability
                    abilities.Ability2();


                    //Abilty CoolDown
                    timer2 = abilities.abiCoolDown2;
                }

                if (timer2 >= 0)
                {
                    timer2 -= 1 * Time.deltaTime;
                    Debug.Log(timer2);
                }
                break;
            case 20:


                break;
            case 40:


                break;
            case 60:


                break;
            case 80:


                break;
            case 100:


                break;
        }               
    }
}
