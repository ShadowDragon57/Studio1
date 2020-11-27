<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BladeAIIDK : MonoBehaviour
{
    #region Vars
    //References
    NavMeshAgent agent;
    Animator anim;
    PlayerHealth playerHP;
    BladeHealth1 bladeHealth;

    //Player Detection 
    GameObject target;
    float angleFOV = 110;
    Vector3 playerPosition;
    bool lostSightofPlayer;
    static Vector3 lastPlayerSighted;
    float distance;
    LayerMask playerLayer = 10;

    //Ground Detection
    Transform groundDetection;
    NavMeshHit navHit;
    LayerMask ground = 8;
    float groundDistance = 0.5f;
    public bool isGrounded;

    //Patrolling
    public bool playerSighted;
    public Vector3 walkPoint;
    bool currentlyWalking;
    bool walkPointSet;
    float walkPointRange = 1000;
    float walkPointDistance;
    bool walkPointGrounded;
    bool hitPlayer;
    int damageChecker;

    //Attacking
    bool canAttack;
    float attackTimer = 3;

    //States
    float sightRange;
    float attackRange, lingeringSightRange;
    public bool inSightRange, inAttackRange;

    #endregion

    void Awake()
    {
        #region Assigning References

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.Find("Graphics");
        bladeHealth = GetComponent<BladeHealth1>();
        playerHP = GameObject.Find("Player Controller").GetComponent<PlayerHealth>();

        #endregion

        #region Defining Vars

        attackRange = 5;
        lingeringSightRange = 50;
        sightRange = 20;
        playerSighted = false;

        #endregion
    }

    void FixedUpdate()
    {
        if (hitPlayer)
        {
            DamagePlayer();
        }
    }

    void DamagePlayer()
    {
        playerHP.playerHP = playerHP.playerHP - 10;
        hitPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = target.transform.position;

        distance = Vector3.Distance(transform.position, playerPosition);
        Vector3 direction = playerPosition - transform.position;
        float angle = Vector3.Angle(direction, transform.position);

        #region Player Frontal Cone Detection
        if (distance <= sightRange)
        {
            inSightRange = true;
        }

        //Allows the enemy to lsoe sight of the player
        if (distance > sightRange)
        {
            inSightRange = false;
        }
        #endregion

        #region Losing Sight of Player
        if (distance > lingeringSightRange)
        {
            playerSighted = false;
            lostSightofPlayer = true;
        }

        if (lostSightofPlayer)
        {
            lastPlayerSighted = playerPosition;
            lostSightofPlayer = false;
        }
        #endregion

        #region GroundDetection

        //Checks if the walkPoint is on the ground
        if (!walkPointGrounded && walkPointSet)
        {
            CheckForGround();
        }

        #endregion

        #region Attack Timer

        //Checks if the enemy is currently able to attack;
        if (!canAttack)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                canAttack = true;
            }
        }

        #endregion

        #region AttackRange

        //Determines if Player is within the Attack Range
        if (distance <= attackRange)
        {
            inAttackRange = true;
        }

        if (distance > attackRange)
        {
            inAttackRange = false;
        }

        #endregion

        #region States
        if (!inSightRange && !inAttackRange || playerSighted == false)
        {
            Patrolling();
        }

        if (playerSighted)
        {
            ChasePlayer();
        }

        if (playerSighted && inSightRange && inAttackRange)
        {
            AttackPlayer();
        }

        #endregion
    }

    void CheckForGround()
    {
        //Checks whether walkPoint is touching the ground
        isGrounded = NavMesh.SamplePosition(walkPoint, out navHit, 1.0f, NavMesh.AllAreas);

        if (isGrounded)
        {
            walkPointGrounded = true;
        }

        if (!isGrounded)
        {
            walkPointSet = false;
        }

    }

    void Patrolling()
    {
        if (!walkPointSet)
        {
            WalkPointCreation();
        }

        if (walkPointSet && walkPointGrounded)
        {
            //Sophias Work
            anim.SetBool("running", true);
            anim.SetInteger("condition", 1);
            agent.SetDestination(walkPoint);
        }

        walkPointDistance = Vector3.Distance(transform.position, walkPoint);

        if (walkPointDistance <= 5)
        {
            walkPointSet = false;
            walkPointGrounded = false;
        }
    }

    void WalkPointCreation()
    {
        //Finds 3 randomised values
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomY = Random.Range(0, 80);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        //Inputs the 3 random values and makes them into a singular Vector3 that goes from the enemies current location
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z + randomZ);
        walkPointSet = true;
    }

    void ChasePlayer()
    {
        //Makes enemy chase player
        agent.SetDestination(playerPosition);
    }

    void AttackPlayer()
    {
        agent.SetDestination(playerPosition);
        //Begins the attack
        //Sophias Work
        anim.SetBool("running", true);
        anim.SetInteger("condition", 1);

        FaceTarget();

        if (distance <= agent.stoppingDistance)
        {
            //Sophias Work
            anim.SetBool("running", false);
            anim.SetInteger("condition", 0);
        }

        if (canAttack)
        {
            StartCoroutine(nameof(Attack));
        }
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
        attackTimer = 2;
        canAttack = false;
        hitPlayer = true;
    }
    #endregion

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public void FaceTarget()
    {
        Vector3 direction = (playerPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        //Smooths out the rotation speed
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == ("Flying Rock"))
        {
            bladeHealth.bladeHealth -= 2;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            walkPointSet = false;
        }

    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BladeAIIDK : MonoBehaviour
{
    #region Vars
    //References
    NavMeshAgent agent;
    Animator anim;
    PlayerHealth playerHP;
    BladeHealth1 bladeHealth;

    //Player Detection 
    GameObject target;
    float angleFOV = 110;
    Vector3 playerPosition;
    bool lostSightofPlayer;
    static Vector3 lastPlayerSighted;
    float distance;
    LayerMask playerLayer = 10;

    //Ground Detection
    Transform groundDetection;
    NavMeshHit navHit;
    LayerMask ground = 8;
    float groundDistance = 0.5f;
    public bool isGrounded;

    //Patrolling
    public bool playerSighted;
    public Vector3 walkPoint;
    bool currentlyWalking;
    bool walkPointSet;
    float walkPointRange = 1000;
    float walkPointDistance;
    bool walkPointGrounded;
    bool hitPlayer;
    int damageChecker;

    //Attacking
    bool canAttack;
    float attackTimer = 3;

    //States
    float sightRange;
    float attackRange, lingeringSightRange;
    public bool inSightRange, inAttackRange;

    #endregion

    void Awake()
    {
        #region Assigning References

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.Find("Graphics");
        bladeHealth = GetComponent<BladeHealth1>();
        playerHP = GameObject.Find("Player Controller").GetComponent<PlayerHealth>();

        #endregion

        #region Defining Vars

        attackRange = 5;
        lingeringSightRange = 50;
        sightRange = 20;
        playerSighted = false;
        canAttack = false;

        #endregion
    }

    void FixedUpdate()
    {
        if (hitPlayer)
        {
            DamagePlayer();
        }
    }

    void DamagePlayer()
    {
        playerHP.playerHP = playerHP.playerHP - 10;
        hitPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = target.transform.position;

        distance = Vector3.Distance(transform.position, playerPosition);
        Vector3 direction = playerPosition - transform.position;
        float angle = Vector3.Angle(direction, transform.position);

        #region Player Frontal Cone Detection
        if (distance <= sightRange)
        {
            inSightRange = true;
        }

        //Allows the enemy to lsoe sight of the player
        if (distance > sightRange)
        {
            inSightRange = false;
        }
        #endregion

        #region Losing Sight of Player
        if (distance > lingeringSightRange)
        {
            playerSighted = false;
            lostSightofPlayer = true;
        }

        if (lostSightofPlayer)
        {
            lastPlayerSighted = playerPosition;
            lostSightofPlayer = false;
        }
        #endregion

        #region GroundDetection

        //Checks if the walkPoint is on the ground
        if (!walkPointGrounded && walkPointSet)
        {
            CheckForGround();
        }

        #endregion

        #region Attack Timer

        //Checks if the enemy is currently able to attack;
        if (!canAttack)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                canAttack = true;
            }
        }

        #endregion

        #region AttackRange

        //Determines if Player is within the Attack Range
        if (distance <= attackRange)
        {
            inAttackRange = true;
        }

        if (distance > attackRange)
        {
            inAttackRange = false;
        }

        #endregion

        #region States
        if (!inSightRange && !inAttackRange || playerSighted == false)
        {
            Patrolling();
        }

        if (playerSighted)
        {
            ChasePlayer();
        }

        if (playerSighted && inSightRange && inAttackRange)
        {
            AttackPlayer();
        }

        #endregion
    }

    void CheckForGround()
    {
        //Checks whether walkPoint is touching the ground
        isGrounded = NavMesh.SamplePosition(walkPoint, out navHit, 1.0f, NavMesh.AllAreas);

        if (isGrounded)
        {
            walkPointGrounded = true;
        }

        if (!isGrounded)
        {
            walkPointSet = false;
        }

    }

    void Patrolling()
    {
        if (!walkPointSet)
        {
            WalkPointCreation();
        }

        if (walkPointSet && walkPointGrounded)
        {
            //Sophias Work
            anim.SetBool("running", true);
            anim.SetInteger("condition", 1);
            agent.SetDestination(walkPoint);
        }

        walkPointDistance = Vector3.Distance(transform.position, walkPoint);

        if (walkPointDistance <= 5)
        {
            walkPointSet = false;
            walkPointGrounded = false;
        }
    }

    void WalkPointCreation()
    {
        //Finds 3 randomised values
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomY = Random.Range(0, 80);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        //Inputs the 3 random values and makes them into a singular Vector3 that goes from the enemies current location
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z + randomZ);
        walkPointSet = true;
    }

    void ChasePlayer()
    {
        //Makes enemy chase player
        agent.SetDestination(playerPosition);
    }

    void AttackPlayer()
    {
        agent.SetDestination(playerPosition);
        //Begins the attack
        //Sophias Work
        anim.SetBool("running", true);
        anim.SetInteger("condition", 1);

        FaceTarget();

        if (distance <= agent.stoppingDistance)
        {
            //Sophias Work
            anim.SetBool("running", false);
            anim.SetInteger("condition", 0);
        }

        if (canAttack)
        {
            StartCoroutine(nameof(Attack));
        }
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
        attackTimer = 2;
        canAttack = false;
        hitPlayer = true;
    }
    #endregion

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public void FaceTarget()
    {
        Vector3 direction = (playerPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        //Smooths out the rotation speed
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == ("Flying Rock"))
        {
            bladeHealth.bladeHealth -= 2;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            walkPointSet = false;
        }

    }
}
>>>>>>> Stashed changes
