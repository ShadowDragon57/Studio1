using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    //References
    public Rigidbody rb;
    private Vector3 playerVelocity;

    //CoolDown Length Variables
    public int QabiCoolDown1 = 3;
    public int QabiCoolDown2 = 5;
    public int QabiCoolDown3 = 3;
    public int QabiCoolDown4 = 5;
    public int QabiCoolDown5 = 3;
    public int QabiCoolDown6 = 5;
    public int QabiCoolDown7 = 5;

    public int EabiCoolDown1 = 3;
    public int EabiCoolDown2 = 5;
    public int EabiCoolDown3 = 3;
    public int EabiCoolDown4 = 5;
    public int EabiCoolDown5 = 3;
    public int EabiCoolDown6 = 5;
    public int EabiCoolDown7 = 5;

    //Ability 1 Variables
    float jumpForce = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    //Abilities

    //Test Jump Ability
    public void Q_Ability1()
    {
        Debug.Log("Ability 1 used");

        playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f);
    }

    public void Q_Ability2()
    {
        Debug.Log("Ability 2 Used");
    }

    public void Q_Ability3()
    {
        Debug.Log("Ability 3 Used");
    }

    public void Q_Ability4()
    {
        Debug.Log("Ability 4 Used");
    }

    public void Q_Ability5()
    {
        Debug.Log("Ability 5 Used");
    }

    public void Q_Ability6()
    {
        Debug.Log("Ability 6 Used");
    }

    public void Q_Ability7()
    {
        Debug.Log("Ability 7 Used");
    }

    public void E_Ability1()
    {
        Debug.Log("Ability 8 Used");
    }

    public void E_Ability2()
    {
        Debug.Log("Ability 9 Used");
    }

    public void E_Ability3()
    {
        Debug.Log("Ability 10 Used");
    }

    public void E_Ability4()
    {
        Debug.Log("Ability 11 Used");
    }

    public void E_Ability5()
    {
        Debug.Log("Ability 12 Used");
    }
    
    public void E_Ability6()
    {
        Debug.Log("Ability 13 Used");
    }
    
    public void E_Ability7()
    {
        Debug.Log("Ability 14 Used");
    }
}
