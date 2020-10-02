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
    public int abiCoolDown3 = 3;
    public int abiCoolDown4 = 5;
    public int abiCoolDown5 = 3;
    public int abiCoolDown6 = 5;
    public int abiCoolDown7 = 3;
    public int abiCoolDown8 = 5;
    public int abiCoolDown9 = 3;
    public int abiCoolDown10 = 5;
    public int abiCoolDown11 = 3;
    public int abiCoolDown12 = 5;

    //Ability 1 Variables
    float jumpForce = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    //Abilities

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

    public void Ability3()
    {
        Debug.Log("Ability 3 Used");
    }

    public void Ability4()
    {
        Debug.Log("Ability 4 Used");
    }

    public void Ability5()
    {
        Debug.Log("Ability 5 Used");
    }

    public void Ability6()
    {
        Debug.Log("Ability 6 Used");
    }

    public void Ability7()
    {
        Debug.Log("Ability 7 Used");
    }

    public void Ability8()
    {
        Debug.Log("Ability 8 Used");
    }

    public void Ability9()
    {
        Debug.Log("Ability 9 Used");
    }

    public void Ability10()
    {
        Debug.Log("Ability 10 Used");
    }

    public void Ability11()
    {
        Debug.Log("Ability 11 Used");
    }

    public void Ability12()
    {
        Debug.Log("Ability 12 Used");
    }
}
