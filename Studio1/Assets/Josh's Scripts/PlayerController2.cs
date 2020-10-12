using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    //References
    ConvictionCalculator conviction;
    public Abilities abilities;
    public CharacterController controller;
    public Transform cam;

    //Direction Related
    public float turnSmoothTime = 0.1f;
    float smoothVeloctiy;
    Vector3 velocity;
    public float gravity = -9.807f;

    //Ground Collision
    public Transform groundCheck;
    public float groundDistance = 0.0f;
    public LayerMask groundMask;
    public bool isGrounded;

    //Other Vars
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

    // Update is called once per frame
    void Update()
    {
        //Checks if the player is touching the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Checks if W is down to see if the player can incrase speed
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

        //gravity

        //Because the original equation of velocity requires time sqared, this is the best way to do it
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


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

            if (Input.GetKey(KeyCode.W) && fDown != true && isGrounded == true)
            {
                Vector3 moveFor = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveFor * forwardSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A) && isGrounded == true)
            {
                Vector3 moveLeft = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveLeft * leftSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S) && isGrounded == true)
            {
                Vector3 moveBack = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveBack * backSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D) && isGrounded == true)
            {
                Vector3 moveRight = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveRight * rightSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.F) && wDown == true && isGrounded == true)
            {
                Vector3 moveFor = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveFor * sprintSpeed * Time.deltaTime);
            }

            //Checks if Player is in the air
            //Air Movement

            if (Input.GetKey(KeyCode.W) && isGrounded == false)
            {
                Vector3 moveFor = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveFor * airMovement * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A) && isGrounded == false)
            {
                Vector3 moveFor = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveFor * airMovement * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S) && isGrounded == false)
            {
                Vector3 moveFor = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveFor * airMovement * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D) && isGrounded == false)
            {
                Vector3 moveFor = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveFor * airMovement * Time.deltaTime);
            }
        }
    }

    void FixedUpdate()
    {
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
