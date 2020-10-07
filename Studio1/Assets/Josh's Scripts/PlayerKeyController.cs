using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerKeyController : MonoBehaviour
{
    //References
    Abilities abilities;
    ConvictionCalculator conviction;

    public Rigidbody rb;

    //Other Vars
    public bool grounded = false;
    public bool wDown;
    public bool fDown;
    public bool QabiUp;
    public bool EabiUp;

    //Movement Speed Vars
    [SerializeField]
    private float leftSpeed, rightSpeed, forwardSpeed, backSpeed, sprintSpeed, airMovement;

    //Timer Var
    private float Qtimer;
    private float Etimer;

    void Start()
    {
        //Defining Speed Vars
        leftSpeed = 50f;
        rightSpeed = 50f;
        forwardSpeed = 100f;
        backSpeed = 100f;
        sprintSpeed = 150f;
        airMovement = 20f;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Ground")
        {
            grounded = true;
        }

        else
        {
            grounded = false;
        }

    }

    void FixedUpdate()
    {
        //Checks to see if they're walking forward
        if (Input.GetKeyDown(KeyCode.W))
        {
            wDown = true;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            wDown = false;
        }

        //Checks to see if the F button is being pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            fDown = true;
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            fDown = false;
        }

        //Checks if the player is currently on the ground
        if (grounded == true)
        {
            //Movement Keys
            if (Input.GetKey(KeyCode.W) && fDown != true)
            {
                rb.position += Vector3.forward * Time.deltaTime * forwardSpeed;
            }

            if (Input.GetKey(KeyCode.A))
            {
                rb.position += Vector3.left * Time.deltaTime * leftSpeed;
            }

            if (Input.GetKey(KeyCode.S))
            {
                rb.position += Vector3.back * Time.deltaTime * rightSpeed;
            }

            if (Input.GetKey(KeyCode.D))
            {
                rb.position += Vector3.right * Time.deltaTime * backSpeed;
            }

            if (Input.GetKey(KeyCode.F) && wDown == true)
            {
                rb.position += Vector3.forward * Time.deltaTime * sprintSpeed;
            }
        }

        //Checks if Player is in the air
        //Air Movement
        if (grounded == false)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.position += Vector3.forward * Time.deltaTime * airMovement;
            }

            if (Input.GetKey(KeyCode.A))
            {
                rb.position += Vector3.left * Time.deltaTime * airMovement;
            }

            if (Input.GetKey(KeyCode.S))
            {
                rb.position += Vector3.back * Time.deltaTime * airMovement;
            }

            if (Input.GetKey(KeyCode.D))
            {
                rb.position += Vector3.right * Time.deltaTime * airMovement;
            }
        }

        //Abilities

        if (Qtimer >= 0)
        {
            Qtimer -= Time.deltaTime;
            QabiUp = true;
        }

        if (Input.GetKey(KeyCode.Q) && QabiUp == true)
        {
            //Changes Q Ability based on faith
            if (conviction.convictionCount == 0)
            {
                abilities.Q_Ability1();
                Qtimer = abilities.QabiCoolDown1;
                QabiUp = false;
            }

            if (conviction.convictionCount <= 20 && conviction.convictionCount > 0)
            {
                abilities.Q_Ability2();
                Qtimer = abilities.QabiCoolDown2;
                QabiUp = false;
            }

            if (conviction.convictionCount <= 40 && conviction.convictionCount > 20)
            {
                abilities.Q_Ability3();
                Qtimer = abilities.QabiCoolDown3;
                QabiUp = false;
            }

            if (conviction.convictionCount <= 60 && conviction.convictionCount > 40)
            {
                abilities.Q_Ability4();
                Qtimer = abilities.QabiCoolDown4;
                QabiUp = false;
            }


            if (conviction.convictionCount <= 80 && conviction.convictionCount > 60)
            {
                abilities.Q_Ability5();
                Qtimer = abilities.QabiCoolDown5;
                QabiUp = false;
            }

            if (conviction.convictionCount < 100 && conviction.convictionCount > 80)
            {
                abilities.Q_Ability6();
                Qtimer = abilities.QabiCoolDown6;
                QabiUp = false;
            }

            if (conviction.convictionCount == 100)
            {
                abilities.Q_Ability7();
                Qtimer = abilities.QabiCoolDown7;
                QabiUp = false;
            }
        }

        if (Etimer >= 0)
        {
            Etimer -= Time.deltaTime;
            EabiUp = true;
        }

        if (Input.GetKey(KeyCode.E) && EabiUp == true)
        {
            //Changes E Ability based on faith
            if (conviction.convictionCount == 0)
            {
                abilities.E_Ability1();
                Qtimer = abilities.EabiCoolDown1;
                EabiUp = false;
            }

            if (conviction.convictionCount <= 20 && conviction.convictionCount > 0)
            {
                abilities.E_Ability2();
                Qtimer = abilities.EabiCoolDown2;
                EabiUp = false;
            }

            if (conviction.convictionCount <= 40 && conviction.convictionCount > 20)
            {
                abilities.E_Ability3();
                Qtimer = abilities.EabiCoolDown3;
                EabiUp = false;
            }

            if (conviction.convictionCount <= 60 && conviction.convictionCount > 40)
            {
                abilities.E_Ability4();
                Qtimer = abilities.EabiCoolDown4;
                EabiUp = false;
            }


            if (conviction.convictionCount <= 80 && conviction.convictionCount > 60)
            {
                abilities.E_Ability5();
                Qtimer = abilities.EabiCoolDown5;
                EabiUp = false;
            }

            if (conviction.convictionCount < 100 && conviction.convictionCount > 80)
            {
                abilities.E_Ability6();
                Qtimer = abilities.EabiCoolDown6;
                EabiUp = false;

            }

            if (conviction.convictionCount == 100)
            {
                abilities.E_Ability7();
                Qtimer = abilities.EabiCoolDown7;
                EabiUp = false;
            }
        }
    }
}
