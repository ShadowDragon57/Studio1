using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    //References
    public Rigidbody rb;

    //CoolDown Length Variables
    public int abiCoolDown1 = 3;
    public int abiCoolDown2 = 5;

    //Ability 1 Variables
    float jumpForce = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    //Test Jump Ability
    public void Ability1()
    {
        Debug.Log("Ability 1 used");

        rb.AddForce(0, jumpForce, 0);
    }

    public void Ability2()
    {
        Debug.Log("Ability 2 Used");
    }
}
