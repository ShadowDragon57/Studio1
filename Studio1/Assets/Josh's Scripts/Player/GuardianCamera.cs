using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GuardianCamera : MonoBehaviour
{
    //References
    CinemachineFreeLook freeLook;

    public bool lockedCam;

    float xMovement, yMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        lockedCam = true;

        yMovement = 5;
        xMovement = 300;
    }

    // Update is called once per frame
    void Update()
    {
        freeLook = GameObject.FindGameObjectWithTag("CamController").GetComponent<CinemachineFreeLook>();

        #region Camera Movement
        if (Input.GetMouseButtonDown(1))
        {
            lockedCam = false;
        }

        if (Input.GetMouseButtonUp(1))
        {
            lockedCam = true;
        }


        //Determine whether the camera can move or cannot move
        if (!lockedCam)
        {
            freeLook.m_YAxis.m_MaxSpeed = yMovement;
            freeLook.m_XAxis.m_MaxSpeed = xMovement;
        }

        if (lockedCam)
        {
            freeLook.m_YAxis.m_MaxSpeed = 0;
            freeLook.m_XAxis.m_MaxSpeed = 0;
        }
        #endregion
    }
}
