using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    //Offset for the mosue to keep the gameObject in it''s original relative position from the player
    private Vector3 mOffset;
    //Original Position;
    private Vector3 oriPos;

    public bool grounded;
    public bool returnedTo0;


    private float mZCoord;

    public void Awake()
    {
        oriPos = transform.position;
    }
    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        //Z Coordinate of game object in game
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {

        transform.position = GetMouseWorldPos() + mOffset;

    }

    public void Update()
    {
        if (transform.position.y <= -1)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            grounded = false;
            returnedTo0 = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            grounded = true;
        }

        if (other.gameObject.layer != 0 && returnedTo0)
        {
            transform.position = oriPos;
            returnedTo0 = false;
        }
    }
}

