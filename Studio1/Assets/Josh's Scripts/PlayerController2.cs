using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    //References
    ConvictionCalculator conviction;
    public CharacterController controller;
    public Animator anim;
    public Transform cam;
    //public SphereCollider col;
    public GuardianController2 guardian;

    //Ability Related
    public GameObject currentIdeology;

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

    //Movement Speed Varsbl
    [SerializeField]
    private float leftSpeed, rightSpeed, forwardSpeed, backSpeed, sprintSpeed, airMovement;

    //Timer Var
    private float Qtimer;
    private float Etimer;

    void Start()
    {
        //Defining Speed Vars
        leftSpeed = 20f;
        rightSpeed = 20f;
        forwardSpeed = 40f;
        backSpeed = 20f;
        sprintSpeed = 80f;
        airMovement = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        //Grabbing the current ideology from ConvictionConsequences
        currentIdeology = GameObject.Find("Canvas").GetComponent<ConvictionConsequences>().currentIdeology;

        //Ability CoolDowns
        if (Qtimer >= 0)
        {
            Qtimer -= Time.deltaTime;
            QabiUp = true;
        }

        if (Etimer >= 0)
        {
            Etimer -= Time.deltaTime;
            EabiUp = true;
        }

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
        if (Input.GetKeyDown(KeyCode.F) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            fDown = true;
        }

        if (Input.GetKeyUp(KeyCode.F) && Input.GetKeyUp(KeyCode.LeftShift))
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

        //Stops walk animation
        if (direction.magnitude < 0.000000001f)
        {
            anim.SetBool("running", false);
            anim.SetInteger("condition", 0);
        }

        //Magnitude checks if you're moving in any direction
        if (direction.magnitude >= 0.00000001f)
        {
            //Makes model begin walk animation
            anim.SetBool("running", true); //Maybe Change this to walking later
            anim.SetInteger("condition", 1);

            //Determines the angle
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothTime, smoothVeloctiy);

            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f).normalized;
            Vector3 moveFor = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (Input.GetKey(KeyCode.W) && fDown != true && isGrounded == true)
            {
                controller.Move(moveFor * forwardSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A) && isGrounded == true)
            {
                controller.Move(moveFor * leftSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S) && isGrounded == true)
            {

                controller.Move(moveFor * backSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D) && isGrounded == true)
            {
                controller.Move(moveFor * rightSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.F) && wDown == true && isGrounded == true)
            {
                controller.Move(moveFor * sprintSpeed * Time.deltaTime);
            }

            //Checks if Player is in the air
            //Air Movement

            if (Input.GetKey(KeyCode.W) && isGrounded == false)
            {
                controller.Move(moveFor * airMovement * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A) && isGrounded == false)
            {
                controller.Move(moveFor * airMovement * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S) && isGrounded == false)
            {
                controller.Move(moveFor * airMovement * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D) && isGrounded == false)
            {
                controller.Move(moveFor * airMovement * Time.deltaTime);
            }
        }
    }

    void FixedUpdate()
    {
        //Abilities

        if (Input.GetKey(KeyCode.Q) /*&& QabiUp == true*/)
        {
            //Changes Q Ability based on current ideology
            if (currentIdeology.CompareTag("revelry"))
            {
                QabiUp = false;
            }

            if (currentIdeology.CompareTag("bliss"))
            {
                QabiUp = false;
            }

            if (currentIdeology.CompareTag("animosity"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities2>().StrikeCast();
                QabiUp = false;
            }

            if (currentIdeology.CompareTag("discontent"))
            {
                QabiUp = false;
            }

            if (currentIdeology.CompareTag("hatred"))
            {
                QabiUp = false;
            }
        }


        if (Input.GetKey(KeyCode.E) /*&& EabiUp == true*/)
        {
            //Changes Q Ability based on current ideology
            if (currentIdeology.CompareTag("revelry"))
            {
                EabiUp = false;
            }

            if (currentIdeology.CompareTag("bliss"))
            {
                EabiUp = false;
            }

            if (currentIdeology.CompareTag("animosity"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities2>().EarthenCast();
                EabiUp = false;
            }

            if (currentIdeology.CompareTag("discontent"))
            {
                EabiUp = false;
            }

            if (currentIdeology.CompareTag("hatred"))
            {
                EabiUp = false;
            }
        }
    }
}


