using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollission : MonoBehaviour
{
    //References
    PlayerController2 control;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            control.grounded = true;
        }

        else
        {
            control.grounded = false;
        }
    }
}
