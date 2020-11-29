using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Yes, this is being remade yet another time.
//I want to die

public class BladeAI3 : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;

    Transform playerPos;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    float walkPointRange;
    float angleFOV;

    //Chasing
    bool playerSighted;

    //Attacking
    bool canAttack;
    float attackTimer;

    //States
    public float sightRange, attackRange;
    public bool inSightRange, inAttackRange;

    void Awake()
    {
        #region Defining References

        playerPos = GameObject.Find("Player Controller").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        #endregion

        #region Defining Vars

        attackTimer = 2;
        angleFOV = 110f;
        playerSighted = false;
        walkPointSet = false;
        
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = playerPos.position - transform.position;
        float angle = Vector3.Angle(direction, transform.position);

        inSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        #region States
        if (!inSightRange && !inAttackRange) Patrolling();

        if (inSightRange && !inAttackRange) ChasePlayer();

        if (inSightRange && inAttackRange) Attacking();
        #endregion

    }

    void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        float distanceToWalkPoint = Vector3.Distance(transform.position, walkPoint);

        //Checks of the enemy has reached the walkPoint and then recalibrates it if so
        if (distanceToWalkPoint <= 2)
        {
            walkPointSet = false;
        }

    }

    void SearchWalkPoint()
    {
        #region Defining Walk Point
        //Finds the positions where the AI will patrol to next
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        #endregion

        #region Walk Point GroundCheck
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
        #endregion
    }

    void ChasePlayer()
    {
        agent.SetDestination(playerPos.position);

    }

    #region Attacking Vars
    void Attacking()
    {
        agent.SetDestination(playerPos.position);

        transform.LookAt(playerPos);

        if (canAttack)
        {
            //Attack
            StartCoroutine(nameof(Attack));
            canAttack = false;
        }

        //Begin Reset
        Invoke(nameof(ResetAttack), attackTimer);
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
    #endregion

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
