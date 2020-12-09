using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    Animator anim;
    Transform cam;
    KeyController key;
    Transform groundCheck;

    //Movement Vars
    [SerializeField]
    public float leftSpeed, rightSpeed, forwardSpeed, backSpeed, sprintSpeed, airMovement;
    private float oriLeft, oriRight, oriFor, oriBack, oriSprint;
    float currentSpeed;
    private bool boostActive = false;

    //Ground Collision
    float groundDistance = 0.5f;
    public LayerMask groundMask = 8;
    public bool isGrounded;

    //Direction Smoothing
    private float turnSmoothTime = 0.15f;
    float turnSmoothVeloctiy;
    Vector3 velocity;
    public float gravity = -9.807f;

    // Start is called before the first frame update
    void Awake()
    {
        #region Assigning References
        controller = GameObject.Find("Graphics").GetComponent<CharacterController>();
        anim = GameObject.Find("Graphics").GetComponent<Animator>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        key = GetComponent<KeyController>();
        groundCheck = GameObject.Find("Ground Detection").GetComponent<Transform>();
        #endregion
    }

    void Start()
    {
        #region Speed Var Definitions
        leftSpeed = 20f;
        rightSpeed = 20f;
        forwardSpeed = 40f;
        backSpeed = 20f;
        sprintSpeed = 80f;
        airMovement = 5f;

        oriLeft = leftSpeed;
        oriRight = rightSpeed;
        oriFor = forwardSpeed;
        oriBack = backSpeed;
        oriSprint = sprintSpeed;
        #endregion
    }


    // Update is called once per frame
    void Update()
    {
        #region Gravity

        //Checks if the player on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        #endregion

        #region Setting Up Movement
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
        #endregion

        #region

        boostActive = GameObject.Find("Abilities").GetComponent<Abilities3>().boostActive;
        //Activates the changes that occur for boost
        if (boostActive)
        {
            leftSpeed = 35f;
            rightSpeed = 35f;
            forwardSpeed = 55f;
            backSpeed = 35f;
            sprintSpeed = 95f;
            airMovement = 5f;
        }
        if (!boostActive)
        {
            leftSpeed = 20f;
            rightSpeed = 20f;
            forwardSpeed = 40f;
            backSpeed = 20f;
            sprintSpeed = 80f;
            airMovement = 5f;
        }
        #endregion

        //Magnitude checks if you're moving in any direction
        if (direction.magnitude >= 0.00000001f)
        {
            #region Movement Variation & Animation
            //Makes model begin walk animation
            anim.SetBool("running", true); //Maybe Change this to walking later
            anim.SetInteger("condition", 1);

            //Determines the angle
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVeloctiy, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f).normalized;
            Vector3 moveFor = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                controller.Move(moveFor.normalized * 0 * Time.deltaTime);
            }
            #endregion

            #region Movement
            if (key.numberOfButtonsDown > 0)
            {
                if (key.numberOfButtonsDown == 1)
                {
                    if (isGrounded)
                    {
                        if (key.wDown)
                        {
                            controller.Move(moveFor.normalized * forwardSpeed * Time.deltaTime);
                        }

                        if (key.aDown)
                        {
                            controller.Move(moveFor.normalized * leftSpeed * Time.deltaTime);
                        }

                        if (key.sDown)
                        {
                            controller.Move(moveFor.normalized * backSpeed * Time.deltaTime);
                        }

                        if (key.dDown)
                        {
                            controller.Move(moveFor.normalized * rightSpeed * Time.deltaTime);
                        }
                    }

                    if (!isGrounded)
                    {
                        controller.Move(moveFor.normalized * airMovement * Time.deltaTime);
                    }

                }


                if (key.numberOfButtonsDown == 2)
                {
                    if (isGrounded)
                    {
                        if (key.wDown && key.aDown)
                        {
                            currentSpeed = Mathf.Pow(forwardSpeed, 2) + Mathf.Pow(leftSpeed, 2);
                            currentSpeed = Mathf.Sqrt(currentSpeed);
                            controller.Move(moveFor.normalized * currentSpeed * Time.deltaTime);
                        }

                        if (key.wDown && key.dDown)
                        {
                            currentSpeed = Mathf.Pow(forwardSpeed, 2) + Mathf.Pow(rightSpeed, 2);
                            currentSpeed = Mathf.Sqrt(currentSpeed);
                            controller.Move(moveFor.normalized * currentSpeed * Time.deltaTime);
                        }

                        if (key.sDown && key.aDown)
                        {
                            currentSpeed = Mathf.Pow(backSpeed, 2) + Mathf.Pow(leftSpeed, 2);
                            currentSpeed = Mathf.Sqrt(currentSpeed);
                            controller.Move(moveFor.normalized * currentSpeed * Time.deltaTime);
                        }

                        if (key.sDown && key.dDown)
                        {
                            currentSpeed = Mathf.Pow(backSpeed, 2) + Mathf.Pow(rightSpeed, 2);
                            currentSpeed = Mathf.Sqrt(currentSpeed);
                            controller.Move(moveFor.normalized * currentSpeed * Time.deltaTime);
                        }

                        if (key.wDown && key.fDown)
                        {
                            controller.Move(moveFor.normalized * sprintSpeed * Time.deltaTime);
                        }
                    }

                    if (!isGrounded)
                    {
                        controller.Move(moveFor.normalized * airMovement * Time.deltaTime);
                    }
                }

                if (key.numberOfButtonsDown == 3)
                {
                    if (key.sDown && key.aDown && key.dDown)
                    {
                        currentSpeed = Mathf.Pow(backSpeed, 3f) + Mathf.Pow(leftSpeed, 3f) + Mathf.Pow(rightSpeed, 3f);
                        currentSpeed = Mathf.Pow(currentSpeed, 1f / 3f);
                        controller.Move(moveFor.normalized * currentSpeed * Time.deltaTime);
                    }

                    if (key.wDown && key.aDown && key.dDown)
                    {
                        currentSpeed = Mathf.Pow(forwardSpeed, 3f) + Mathf.Pow(leftSpeed, 3f) + Mathf.Pow(rightSpeed, 3f);
                        currentSpeed = Mathf.Pow(currentSpeed, 1f / 3f);
                        controller.Move(moveFor.normalized * currentSpeed * Time.deltaTime);
                    }

                    if (key.wDown && key.fDown && key.dDown)
                    {
                        currentSpeed = Mathf.Pow(sprintSpeed, 2) + Mathf.Pow(rightSpeed, 2);
                        currentSpeed = Mathf.Sqrt(currentSpeed);
                        controller.Move(moveFor.normalized * currentSpeed * Time.deltaTime);
                    }

                    if (key.wDown && key.fDown && key.aDown)
                    {
                        currentSpeed = Mathf.Pow(sprintSpeed, 2) + Mathf.Pow(leftSpeed, 2);
                        currentSpeed = Mathf.Sqrt(currentSpeed);
                        controller.Move(moveFor.normalized * currentSpeed * Time.deltaTime);
                    }

                }

                if (key.numberOfButtonsDown == 4)
                {
                    if (key.wDown && key.aDown && key.fDown && key.dDown)
                    {
                        currentSpeed = Mathf.Pow(sprintSpeed, 3f) + Mathf.Pow(leftSpeed, 3f) + Mathf.Pow(rightSpeed, 3f);
                        currentSpeed = Mathf.Pow(currentSpeed, 1 / 3f);
                        controller.Move(moveFor.normalized * currentSpeed * Time.deltaTime);
                    }
                }
            }
            #endregion
        }
        
    }
}
