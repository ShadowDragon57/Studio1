using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Yes, this is being remade yet another time.
//I want to die

public class BladeAI3 : MonoBehaviour
{
    NavMeshAgent agent;

    public Transform playerPos;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patrolling
    public Transform[] wayPoints;
    public Vector3 walkPoint;
    int wayPointIndex;
    float distance;

    //Attacking
    bool canAttack;
    float timer;

    //States
    public float sightRange, attackRange;
    public bool inSightRange, inAttackRange;

    void Awake()
    {
        playerPos = GameObject.Find("Player Controller").transform;
        agent = GetComponent<NavMeshAgent>();

        wayPointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        inSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsGround);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!inSightRange && !inAttackRange) Patrolling();
        if (inSightRange && !inAttackRange) ChasePlayer();
        if (inSightRange && inAttackRange) Attacking();

    }

    void Patrolling()
    {

    }

    void ChasePlayer()
    {

    }

    void Attacking()
    {

    }

}
