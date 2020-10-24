using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    //Offset for the mosue to keep the gameObject in it''s original relative position from the player
    private Vector3 mOffset;

    public bool grounded;

    private float mZCoord;
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
            transform.position = new Vector3(transform.position.x, 0,
                transform.position.z);
            grounded = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            grounded = true;
        }
    }
}

