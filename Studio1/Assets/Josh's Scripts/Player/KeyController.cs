using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

//Used to determine what sort of buttons are currently being pressed
//Allows for normalisation of movement

public class KeyController : MonoBehaviour
{
    public int numberOfButtonsDown;
    public bool wDown;
    public bool aDown;
    public bool sDown;
    public bool dDown;
    public bool fDown;
    public bool lShiftDown;

    // Update is called once per frame
    void Update()
    {
        #region KeyChecking
        if (Input.GetKeyDown(KeyCode.W))
        {
            wDown = true;
            numberOfButtonsDown += 1;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            wDown = false;
            numberOfButtonsDown -= 1;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            aDown = true;
            numberOfButtonsDown += 1;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            aDown = false;
            numberOfButtonsDown -= 1;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            sDown = true;
            numberOfButtonsDown += 1;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            sDown = false;
            numberOfButtonsDown -= 1;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            dDown = true;
            numberOfButtonsDown += 1;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            dDown = false;
            numberOfButtonsDown -= 1;
        }

        if (wDown)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                fDown = true;
                numberOfButtonsDown += 1;
            }

            if (Input.GetKeyUp(KeyCode.F))
            {
                fDown = false;
                numberOfButtonsDown -= 1;
            }
        }

        #endregion

        #region Error Fixes
        if (!wDown && fDown)
        {
            fDown = false;
            if (numberOfButtonsDown >= 0 && Input.anyKeyDown == false)
            {
                numberOfButtonsDown = 0;
            }
        }
        #endregion
    }
}
