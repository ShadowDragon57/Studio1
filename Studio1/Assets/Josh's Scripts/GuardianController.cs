using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GuardianController : MonoBehaviour
{
    //References
    public CinemachineFreeLook freeLook;

    //Movement Values
    [SerializeField]
    private int xMovement = 300;

    [SerializeField]
    private int yMovement = 3;

    //Mouse Button Triggers
    [SerializeField]
    private bool camLocked;

    [SerializeField]
    private bool leftButtonDown = false;

    // Start is called before the first frame update
    void Start()
    {
        camLocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        //If mouse button is pressed, then it will turn the bool to true
        if (Input.GetMouseButtonDown(1))
        {
            camLocked = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            leftButtonDown = true;
        }

        //If mouse button goes up, the bools will be turned false

        if (Input.GetMouseButtonUp(1))
        {
            camLocked = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            leftButtonDown = false;
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

    //Checks if Mouse if hovering over a collider
    //void OnMouseOver()
    //{
    //    if (leftButtonDown)
    //    {

    //    }
    //}

}
