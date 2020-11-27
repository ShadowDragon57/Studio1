<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BladeAIFinal : MonoBehaviour
{

    NavMeshAgent agent;
    Animator anim;

    //Player Detection
    Transform playerPos;
    GameObject target;
    float angleFOV = 110;
    bool playerSighted;
    static Vector3 lastPlayerSighting;

    //Ground Detection
    Transform groundDetection;
    GameObject toBeDestroyed;
    float groundDistance;
    LayerMask ground = 8;
    bool isGrounded;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    float walkPointRange;
    float walkPointDistance;
    public GameObject groundCheck;

    //Attacking
    bool canAttack;
    float attackTimer;

    //States
    float sightRange, attackRange;
    public bool inSightRange, inAttackRange;

    void Awake()
    {
        #region Defining Vars

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.Find("Graphics");
        sightRange = 50;
        attackRange = 5;
        playerSighted = false;

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = target.transform;

        if (Vector3.Distance(playerPos.position, transform.position) <= sightRange)
        {
            Vector3 direction = playerPos.position - transform.position;
            float angle = Vector3.Angle(direction, transform.position);

            //If it finds that the player is within half the angle for the field of view, it has seen the player
            if (angle <= angleFOV / 2)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position + (transform.up * 2.25f), direction.normalized, out hit, sightRange))
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    { 
                        inSightRange = true;
                    }
                }
            }

        }

        if (GameObject.Find("GroundCheck(Clone)") != null)
        {
            toBeDestroyed = GameObject.Find("GroundCheck(Clone)");
        }


        if (Vector3.Distance(playerPos.position, transform.position) <= attackRange)
        {
            inAttackRange = true;
        }

        if (Vector3.Distance(playerPos.position, transform.position) >= attackRange)
        {
            inAttackRange = false;
        }

        if (!inSightRange && !inAttackRange) Patrolling();
        if (inSightRange && !inAttackRange) ChasePlayer();
        if (inSightRange && inAttackRange) AttackPlayer();

    }

    void Patrolling()
    {
        if (!walkPointSet)
        {
            WalkPointCreation();
        }

        if (walkPointSet)
        {
            GameObject.Instantiate(groundCheck, new Vector3(walkPoint.x, walkPoint.y, walkPoint.z), Quaternion.identity);

            if (GameObject.Find("GroundCheck(Clone)") != null)
            {
                isGrounded = Physics.CheckSphere(walkPoint, 0.5f, ground);
            }
        }


        if (isGrounded)
        {
            Destroy(toBeDestroyed);
            agent.SetDestination(walkPoint);
        }

        if (!isGrounded)
        {
            walkPointSet = false;
            Destroy(toBeDestroyed);
        }

        walkPointDistance = Vector3.Distance(transform.position, walkPoint);

        if (walkPointDistance <= 5)
        {
            walkPointSet = false;
        }
    }

    void WalkPointCreation()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomY = Random.Range(0, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(randomX, randomY, randomZ);
        walkPointSet = true;
    }

    void ChasePlayer()
    {
        agent.SetDestination(playerPos.position);
    }

    void AttackPlayer()
    {
        agent.SetDestination(playerPos.position);

        transform.LookAt(playerPos);

        if (canAttack)
        {
            //Attack
            StartCoroutine(nameof(Attack));
        }

        if (!canAttack)
        {
            //Begin Reset
            Invoke(nameof(ResetAttack), attackTimer);
        }
    }

    void ResetAttack()
    {
        canAttack = true;
    }


    #region AttackAnimation Initiation
    void Attack()
    {
        StartCoroutine(AttackRoutine());
    }

    //Done by Sophia
    IEnumerator AttackRoutine()
    {
        anim.SetBool("attacking", true);
        anim.SetInteger("condition", 2);
        yield return new WaitForSeconds(2);
        anim.SetBool("attacking", false);
        anim.SetInteger("condition", 0);
        canAttack = false;
    }
    #endregion

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BladeAIFinal : MonoBehaviour
{

    NavMeshAgent agent;
    Animator anim;

    //Player Detection
    Transform playerPos;
    GameObject target;
    float angleFOV = 110;
    bool playerSighted;
    static Vector3 lastPlayerSighting;

    //Ground Detection
    Transform groundDetection;
    GameObject toBeDestroyed;
    float groundDistance;
    LayerMask ground = 8;
    bool isGrounded;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    float walkPointRange;
    float walkPointDistance;
    public GameObject groundCheck;

    //Attacking
    bool canAttack;
    float attackTimer;

    //States
    float sightRange, attackRange;
    public bool inSightRange, inAttackRange;

    void Awake()
    {
        #region Defining Vars

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.Find("Graphics");
        sightRange = 50;
        attackRange = 5;
        playerSighted = false;

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = target.transform;

        if (Vector3.Distance(playerPos.position, transform.position) <= sightRange)
        {
            Vector3 direction = playerPos.position - transform.position;
            float angle = Vector3.Angle(direction, transform.position);

            //If it finds that the player is within half the angle for the field of view, it has seen the player
            if (angle <= angleFOV / 2)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position + (transform.up * 2.25f), direction.normalized, out hit, sightRange))
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    { 
                        inSightRange = true;
                    }
                }
            }

        }

        if (GameObject.Find("GroundCheck(Clone)") != null)
        {
            toBeDestroyed = GameObject.Find("GroundCheck(Clone)");
        }


        if (Vector3.Distance(playerPos.position, transform.position) <= attackRange)
        {
            inAttackRange = true;
        }

        if (Vector3.Distance(playerPos.position, transform.position) >= attackRange)
        {
            inAttackRange = false;
        }

        if (!inSightRange && !inAttackRange) Patrolling();
        if (inSightRange && !inAttackRange) ChasePlayer();
        if (inSightRange && inAttackRange) AttackPlayer();

    }

    void Patrolling()
    {
        if (!walkPointSet)
        {
            WalkPointCreation();
        }

        if (walkPointSet)
        {
            GameObject.Instantiate(groundCheck, new Vector3(walkPoint.x, walkPoint.y, walkPoint.z), Quaternion.identity);

            if (GameObject.Find("GroundCheck(Clone)") != null)
            {
                isGrounded = Physics.CheckSphere(walkPoint, 0.5f, ground);
            }
        }


        if (isGrounded)
        {
            Destroy(toBeDestroyed);
            agent.SetDestination(walkPoint);
        }

        if (!isGrounded)
        {
            walkPointSet = false;
            Destroy(toBeDestroyed);
        }

        walkPointDistance = Vector3.Distance(transform.position, walkPoint);

        if (walkPointDistance <= 5)
        {
            walkPointSet = false;
        }
    }

    void WalkPointCreation()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomY = Random.Range(0, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(randomX, randomY, randomZ);
        walkPointSet = true;
    }

    void ChasePlayer()
    {
        agent.SetDestination(playerPos.position);
    }

    void AttackPlayer()
    {
        agent.SetDestination(playerPos.position);

        transform.LookAt(playerPos);

        if (canAttack)
        {
            //Attack
            StartCoroutine(nameof(Attack));
        }

        if (!canAttack)
        {
            //Begin Reset
            Invoke(nameof(ResetAttack), attackTimer);
        }
    }

    void ResetAttack()
    {
        canAttack = true;
    }


    #region AttackAnimation Initiation
    void Attack()
    {
        StartCoroutine(AttackRoutine());
    }

    //Done by Sophia
    IEnumerator AttackRoutine()
    {
        anim.SetBool("attacking", true);
        anim.SetInteger("condition", 2);
        yield return new WaitForSeconds(2);
        anim.SetBool("attacking", false);
        anim.SetInteger("condition", 0);
        canAttack = false;
    }
    #endregion

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
>>>>>>> Stashed changes
