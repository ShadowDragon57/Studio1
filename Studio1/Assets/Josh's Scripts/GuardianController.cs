using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;
using Boo.Lang.Environments;

public class GuardianController : MonoBehaviour
{
    //References
    public CinemachineFreeLook freeLook;
    public PlayerController2 controller;

    public GameObject rockPrefab;
    public Vector3 positionMouse;

    private GameObject throwableRock;

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

    [SerializeField]
    private bool leftButtonDown = false;

    public bool flyingRock = false;

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

        if (Input.GetMouseButtonDown(0))
        {
            leftButtonDown = true;
            RaycastHit hit;
            //Uses an area within the cameras view to find teh mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "grabbableRock" && numberOfRocks < 1)
                {
                    Instantiate(rockPrefab, hit.point, Quaternion.identity);
                    numberOfRocks += 1;
                }
            }
        }

        //Gets the spawned rock to follow the mouse while the left mouse button is being held
        if (leftButtonDown && flyingRock == false)
        {
            throwableRock = GameObject.Find("rockPrefab(Clone)");
            throwableRock.GetComponent<Transform>().position = Camera.main.ScreenToWorldPoint(positionMouse);   
        }


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
            flyingRock = true;
            
        }

        if (flyingRock == true)
        {
            numberOfRocks = 0;
            //makes object shoot off one mouse button is release
            throwableRock.GetComponent<Rigidbody>().AddRelativeForce(throwableRock.transform.forward * speed * Time.deltaTime);
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
}
