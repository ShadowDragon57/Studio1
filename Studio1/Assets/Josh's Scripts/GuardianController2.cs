using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;
using Boo.Lang.Environments;
using UnityStandardAssets.Cameras;

public class GuardianController2 : MonoBehaviour
{
    //References
    public CinemachineFreeLook freeLook;
    public PlayerController2 playerController;

    public GameObject rockPrefab;


    //Public Vars
    public Quaternion playerRotation;
    public Vector3 hitPosition;

    public bool lockedCam;
    public bool flyingRock;
    public bool antiMouseLock;

    public int rocksCollected;

    //Private Vars
    private GameObject thrownRocks;

    private Vector3 mouseLoc;
    private Vector3 mouseWorldPos;

    private float xMovement = 300;
    private float yMovement = 3;

    private int rockLimit = 16;
    
    public void Awake()
    {
        lockedCam = true;
    }

    public void Update()
    {
        mouseLoc = Input.mousePosition;
        //This Z offset means the item will spawn a certain distance away from the camera
        mouseLoc.z = 10;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseLoc);

        if (Input.GetMouseButtonDown(0))
        {
            //Finds the point on the screen where the mouse is.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform.gameObject.tag == "grabbableRock" && rocksCollected < rockLimit)
                {
                    //Adds a collection of rocks to the 
                    rocksCollected += 1;
                }

                if (hit.transform.gameObject.tag != "grabbableRock")
                {
                    if (rocksCollected > 0)
                    {
                        rocksCollected -= 1;
                        Instantiate(rockPrefab, mouseWorldPos, Quaternion.Euler(0, playerController.GetComponent<Transform>().rotation.y, 0));
                        playerRotation = playerController.GetComponent<Transform>().rotation;
                        hitPosition = hit.point;
                    }

                    if (rocksCollected <= 0)
                    {
                        Debug.Log("Coat says:" + " " + "No more rocks to throw");
                    }

                    if (rocksCollected == rockLimit)
                    {
                        Debug.Log("You find yourself burdened by the weight of these material things. You cannot hold anymore");
                    }
                }
            }
        }

        if (GameObject.Find("rockPrefab(Clone)") != null)
        {
            thrownRocks = GameObject.Find("rockPrefab(Clone)");
            thrownRocks.name = "Flying Rock";
        }

        if (Input.GetMouseButtonDown(1))
        {
            lockedCam = false;
        }

        if (Input.GetMouseButtonUp(1))
        {
            lockedCam = true;
        }


        //Determine whether the camera can move or cannot move
        if (lockedCam == false)
        {
            freeLook.m_YAxis.m_MaxSpeed = yMovement;
            freeLook.m_XAxis.m_MaxSpeed = xMovement;
        }

        if (lockedCam == true)
        {
            freeLook.m_YAxis.m_MaxSpeed = 0;
            freeLook.m_XAxis.m_MaxSpeed = 0;
        }
    }


}
