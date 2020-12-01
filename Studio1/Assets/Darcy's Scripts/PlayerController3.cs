using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController3 : MonoBehaviour
{
    //References
    ConvictionCalculator1 conviction;
    public CharacterController controller;
    public Animator anim;
    public Transform cam;
    public GuardianController2 guardian;
    public KeyController key;

    //Ability related
    [SerializeField]
    private GameObject currentIdeology;
    public float speedBoostCount;
    private bool boostActive, beamActive, qAbiUsed, eAbiUsed;
    [SerializeField]
    private Text qCooldownText, eCooldownText;

    //Direction Related
    private float turnSmoothTime = 0.15f;
    float turnSmoothVeloctiy;
    Vector3 velocity;
    public float gravity = -9.807f;

    //Ground Collision
    public Transform groundCheck;
    public float groundDistance = 0.0f;
    public LayerMask groundMask;
    public bool isGrounded;

    //Other Vars
    public bool fDown;

    //Movement Speed Varsbl
    [SerializeField]
    private float leftSpeed, rightSpeed, forwardSpeed, backSpeed, sprintSpeed, airMovement;
    private float currentSpeed;

    //Timer Var
    [SerializeField]
    private float qTimer, eTimer;

    void Start()
    {
        boostActive = false;
        beamActive = false;
        qAbiUsed = false;
        eAbiUsed = false;
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
        currentIdeology = GameObject.Find("Canvas").GetComponent<ConvictionConsequences1>().currentIdeology;

        //timer for the speed boost from booster ability, and the increase for the speed variables
        if(boostActive == true)
        {
            speedBoostCount += Time.deltaTime;
            leftSpeed = 35f;
            rightSpeed = 35f;
            forwardSpeed = 55f;
            backSpeed = 35f;
        }
        if(speedBoostCount > 3)
        {
            leftSpeed = 20f;
            rightSpeed = 20f;
            forwardSpeed = 40f;
            backSpeed = 20f;
            boostActive = false;
            speedBoostCount = 0;
        }

        if(beamActive == true)
        {
            leftSpeed = 0f;
            rightSpeed = 0f;
            forwardSpeed = 0f;
            backSpeed = 0f;
        }

        if(beamActive == false)
        {
            leftSpeed = 20f;
            rightSpeed = 20f;
            forwardSpeed = 40f;
            backSpeed = 20f;
        }

        //Ability CoolDowns
        if (qAbiUsed == true)
        {
            qTimer += Time.deltaTime;
            qCooldownText.text = qTimer.ToString("0");
        }
        if(qTimer >= 5)
        {
            qTimer = 0;
            qCooldownText.text = qTimer.ToString("0");
            GameObject.Find("Abilities").GetComponent<Abilities3>().DestroyQAbility();
            qAbiUsed = false;
        }

        if(eAbiUsed == true)
        {
            eTimer += Time.deltaTime;
            eCooldownText.text = eTimer.ToString("0");
        }
        if (eTimer >= 5)
        {
            eTimer = 0;
            eCooldownText.text = eTimer.ToString("0");
            GameObject.Find("Abilities").GetComponent<Abilities3>().DestroyEAbility();
            eAbiUsed = false;
        }

        //Checks if the player is touching the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
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
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVeloctiy, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f).normalized;
            Vector3 moveFor = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            

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
                }


            }
        }
    }

    public void BoosterActivated() //telling this script that the boost has been activated
    {
        boostActive = true;
    }

    public void BeamActivated()
    {
        beamActive = true;
    }

    public void BeamDeactivated()
    {
        beamActive = false;
    }

    void FixedUpdate()
    {
        //Abilities

        if (Input.GetKey(KeyCode.Q) && qAbiUsed == false)
        {
            //Changes Q Ability based on current ideology
            if (currentIdeology.CompareTag("revelry"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities3>().PulseCast();
                qAbiUsed = true;
            }

            if (currentIdeology.CompareTag("bliss"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities3>().BeamCast();
                qAbiUsed = true;
            }

            if (currentIdeology.CompareTag("animosity"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities3>().StrikeCast();
                qAbiUsed = true;
            }

            if (currentIdeology.CompareTag("discontent"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities3>().SunCast();

                qAbiUsed = true;
            }

            if (currentIdeology.CompareTag("hatred"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities3>().LanceCast();
                qAbiUsed = true;
            }
        }


        if (Input.GetKey(KeyCode.E) && eAbiUsed == false)
        {
            //Changes Q Ability based on current ideology
            if (currentIdeology.CompareTag("revelry"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities3>().VoidCast();
                eAbiUsed = true;
            }

            if (currentIdeology.CompareTag("bliss"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities3>().BoosterCast();
                eAbiUsed = true;
            }

            if (currentIdeology.CompareTag("animosity"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities3>().EarthenCast();
                eAbiUsed = true;
            }

            if (currentIdeology.CompareTag("discontent"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities3>().WallCast();
                eAbiUsed = true;
            }

            if (currentIdeology.CompareTag("hatred"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities3>().ShieldCast();
                eAbiUsed = true;
            }
        }
    }
}


