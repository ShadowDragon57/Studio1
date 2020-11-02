using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Means this script won't run without a playermotor script
[RequireComponent(typeof(PlayerMotor))]
public class NavAgentTut : MonoBehaviour
{
    public PlayerMotor motor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

        }
    }

}
