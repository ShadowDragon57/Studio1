using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    private GameObject playerController;
    private Vector3 playerPosition;
    private float distance;

    //Offset for the mosue to keep the gameObject in it''s original relative position from the player
    private Vector3 mOffset;
    //Original Position;
    private Vector3 oriPos;
    private EnvironmentInteraction environ;


    public bool grounded;

    public bool inRange;
    private float mZCoord;

    public void Awake()
    {
        oriPos = transform.position;
        environ = GameObject.Find("Collider").GetComponent<EnvironmentInteraction>();
    }

    private void OnMouseDown()
    {
        if (inRange)
        {
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

            //Holds the object at the same position away from the character
            //Allows the player to move with an object
            mOffset = gameObject.transform.position - GetMouseWorldPos();

        }
    }

    private Vector3 GetMouseWorldPos()
    {
        //Grabs Mouse Position
        Vector3 mousePoint = Input.mousePosition;

        //Z Coordinate of game object in game
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        if (inRange)
        {
            //Ensures that the mouse remains at its relative distance
            transform.position = GetMouseWorldPos() + mOffset;
        }
    }

    public void Update()
    {
        playerController = GameObject.FindGameObjectWithTag("Player");
        playerPosition = playerController.transform.position;
        distance = Vector3.Distance(transform.position, playerPosition);


        if (distance >= 200)
        {
            inRange = false;
        }

        if (distance <= 200)
        {
            inRange = true;
        }


        if (transform.position.y <= -1)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            grounded = false;
        }

        if (Input.GetKey(KeyCode.R))
        {
            transform.position = oriPos;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        //Checks if the object is currently going through the ground
        //Make sure to add in colliders where the object shouldn't be going through the floor
        if (other.gameObject.layer == 8)
        {
            grounded = true;
        }
    }
}

