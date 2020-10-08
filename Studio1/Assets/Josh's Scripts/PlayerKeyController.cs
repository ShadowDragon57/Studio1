using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerKeyController : MonoBehaviour
{
    //References
    Abilities abilities;
    ConvictionCalculator conviction;
    CameraFollow camscript;

    public Rigidbody rb;
    public Transform cam;

    //Turning Vars
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //Other Vars
    public bool grounded = false;
    public bool wDown;
    public bool fDown;
    public bool QabiUp;
    public bool EabiUp;
    public Vector3 stayStill = new Vector3(0, 0, 0);

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
        if (col.gameObject.tag == "Ground")
        {
            grounded = true;
        }

        else
        {
            grounded = false;
        }

    }

    public void Update()
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
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothTime, turnSmoothVelocity);
            //Normalising in this instance makes it a gradual change
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f).normalized;

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

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
    }

    public void FixedUpdate()
    {
        //Movement Keys
        if (Input.GetKey(KeyCode.W) && fDown != true && grounded == true)
        {
            rb.position += Vector3.forward * Time.deltaTime * forwardSpeed;
        }

        if (Input.GetKey(KeyCode.A) && grounded == true)
        {
            rb.position += Vector3.left * Time.deltaTime * leftSpeed;
        }

        if (Input.GetKey(KeyCode.S) && grounded == true)
        {
            rb.position += Vector3.back * Time.deltaTime * rightSpeed;
        }

        if (Input.GetKey(KeyCode.D) && grounded == true)
        {
            rb.position += Vector3.right * Time.deltaTime * backSpeed;
        }

        if (Input.GetKey(KeyCode.F) && wDown == true && grounded == true)
        {
            rb.position += Vector3.forward * Time.deltaTime * sprintSpeed;
        }


        //Checks if Player is in the air
        //Air Movement

        if (Input.GetKey(KeyCode.W) && grounded == false)
        {
            rb.position += Vector3.forward * Time.deltaTime * airMovement;
        }

        if (Input.GetKey(KeyCode.A) && grounded == false)
        {
            rb.position += Vector3.left * Time.deltaTime * airMovement;
        }

        if (Input.GetKey(KeyCode.S) && grounded == false)
        {
            rb.position += Vector3.back * Time.deltaTime * airMovement;
        }

        if (Input.GetKey(KeyCode.D) && grounded == false)
        {
            rb.position += Vector3.right * Time.deltaTime * airMovement;
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
            if (conviction.convictionCount <= 20 && conviction.convictionCount > 0)
            {
                abilities.Q_Ability1();
                Qtimer = abilities.QabiCoolDown1;
                QabiUp = false;
            }

            if (conviction.convictionCount <= 40 && conviction.convictionCount > 20)
            {
                abilities.Q_Ability2();
                Qtimer = abilities.QabiCoolDown2;
                QabiUp = false;
            }

            if (conviction.convictionCount <= 60 && conviction.convictionCount > 40)
            {
                abilities.Q_Ability3();
                Qtimer = abilities.QabiCoolDown3;
                QabiUp = false;
            }


            if (conviction.convictionCount <= 80 && conviction.convictionCount > 60)
            {
                abilities.Q_Ability4();
                Qtimer = abilities.QabiCoolDown4;
                QabiUp = false;
            }

            if (conviction.convictionCount < 100 && conviction.convictionCount > 80)
            {
                abilities.Q_Ability5();
                Qtimer = abilities.QabiCoolDown5;
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
            if (conviction.convictionCount <= 20 && conviction.convictionCount > 0)
            {
                abilities.E_Ability1();
                Qtimer = abilities.EabiCoolDown1;
                EabiUp = false;
            }

            if (conviction.convictionCount <= 40 && conviction.convictionCount > 20)
            {
                abilities.E_Ability2();
                Qtimer = abilities.EabiCoolDown2;
                EabiUp = false;
            }

            if (conviction.convictionCount <= 60 && conviction.convictionCount > 40)
            {
                abilities.E_Ability3();
                Qtimer = abilities.EabiCoolDown3;
                EabiUp = false;
            }


            if (conviction.convictionCount <= 80 && conviction.convictionCount > 60)
            {
                abilities.E_Ability4();
                Qtimer = abilities.EabiCoolDown4;
                EabiUp = false;
            }

            if (conviction.convictionCount < 100 && conviction.convictionCount > 80)
            {
                abilities.E_Ability5();
                Qtimer = abilities.EabiCoolDown5;
                EabiUp = false;
            }
        }
    }

}
