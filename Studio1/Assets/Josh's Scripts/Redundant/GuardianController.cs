using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;
<<<<<<< Updated upstream
using Boo.Lang.Environments;
=======
//using Boo.Lang.Environments;
>>>>>>> Stashed changes

public class GuardianController : MonoBehaviour
{
    //References
    public CinemachineFreeLook freeLook;
    public PlayerController2 controller;

    public GameObject rockPrefab;
    public Vector3 positionMouse;

    private GameObject throwableRock;

    public Quaternion playerRotation;
    public Quaternion camRotation;

    [SerializeField]
    private int numberOfRocks;

    //Movement Values
    [SerializeField]
    private int xMovement = 300;

    [SerializeField]
    private int yMovement = 3;

    [SerializeField]
    private float speed;

    //Mouse Button Triggers
    [SerializeField]
    private bool camLocked;

    public bool leftButtonDown = false;

    public bool flyingRock = false;
    public bool holdingObject = false;
    public bool antiMouseLock = false;
    public bool playerHit = false;

    // Start is called before the first frame update
    void Start()
    {
        camLocked = true;
        numberOfRocks = 0;
        speed = 10000;

    }

    // Update is called once per frame
    void Update()
    {
        //Tracks Mouse Location
        positionMouse = Input.mousePosition;
        positionMouse.z = 10f;

        //Allows interaction with game objects
        GameObject playerController = GameObject.Find("Player");

        if (Input.GetMouseButtonDown(0) && antiMouseLock == false)
        {
            leftButtonDown = true;
            RaycastHit hit;
            //Uses an area within the cameras view to find teh mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("grabbableRock") && numberOfRocks < 1 && flyingRock == false)
                {
                    Instantiate(rockPrefab, hit.point, Quaternion.Euler(0, playerController.GetComponent<Transform>().rotation.y, 0));
                    playerRotation = playerController.GetComponent<Transform>().rotation;
                    numberOfRocks += 1;
                }

                //if (hit.collider.gameObject.layer == 12)
                //{
                //    currentHeldObject = hit.collider.gameObject;
                //    holdingObject = true;
                //}

                else
                {
                    return;
                }
            }

            else
            {
                return;
            }
        }

        //Gets the spawned rock to follow the mouse while the left mouse button is being held
        if (leftButtonDown && numberOfRocks > 0)
        {
            throwableRock = GameObject.Find("rockPrefab(Clone)");
            throwableRock.GetComponent<Transform>().position = Camera.main.ScreenToWorldPoint(positionMouse);
        }

        //if (currentHeldObject != null && holdingObject)
        //{
        //    currentHeldObject.GetComponent<Transform>().position = Camera.main.ScreenToViewportPoint(positionMouse);

        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        currentHeldObject = null;
        //    }
        //}


        //If mouse button is pressed, then it will turn the bool to true
        if (Input.GetMouseButtonDown(1))
        {
            camLocked = false;
        }

        //If mouse button goes up, the bools will be turned false

        if (Input.GetMouseButtonUp(1))
        {
            camLocked = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            leftButtonDown = false;
            if (numberOfRocks > 0)
            {
                flyingRock = true;
            }
           
        }

        if (flyingRock == true)
        {
            numberOfRocks = 0;
            //makes object shoot off one mouse button is release
            throwableRock.GetComponent<Rigidbody>().AddRelativeForce(transform.TransformDirection(throwableRock.transform.forward) * speed * Time.deltaTime);
            //Allows another rock to be picked up and thrown
            throwableRock.name = "Flying Rock";
        }

        //Determine whether the camera can move or cannot move
        if (camLocked == false)
        {
            freeLook.m_YAxis.m_MaxSpeed = yMovement;
            freeLook.m_XAxis.m_MaxSpeed = xMovement;
        }

        if (camLocked == true)
        {
            freeLook.m_YAxis.m_MaxSpeed = 0;
            freeLook.m_XAxis.m_MaxSpeed = 0;
        }

    }

    private void FixedUpdate()
    {
        if (antiMouseLock)
        {
            leftButtonDown = false;
            antiMouseLock = false;
        }

        if (playerHit == true)
        {
            flyingRock = false;
            numberOfRocks = 0;
            playerHit = false;
        }
    }
}
