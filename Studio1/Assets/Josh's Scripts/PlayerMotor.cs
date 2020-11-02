using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Means this script won't run unless a navmeshagent is available
[RequireComponent(typeof(NavMeshAgent))]

public class PlayerMotor : MonoBehaviour
{
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToPoint (Vector3 point)
    {

    }

}
