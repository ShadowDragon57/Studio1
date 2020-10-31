using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentInteraction : MonoBehaviour
{
    private SphereCollider col;
    public bool inRange;

    public void Start()
    {
        col = gameObject.GetComponent<SphereCollider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("interactables"))
        {

        }
    }

}
