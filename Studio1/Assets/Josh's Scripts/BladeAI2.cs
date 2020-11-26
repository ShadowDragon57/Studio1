using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BladeAI2 : MonoBehaviour
{
    Animator anim;
    Transform target;
    NavMeshAgent agent;

    GameObject player;

    private float triggerRadius = 50f;
    private float attackRadius = 10f;
    public float angleFOV = 110f;

    public int healthPoints = 5;

    public bool playerSighted = false;
    public Vector3 sightedLoc;
    public Vector3 previousSighting;
    public bool wereInSight = false;

    public bool beenHit = false;

    public float attackTimer = 1;
    public bool canAttack;


    public void Start()
    { 
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        //Checks if player is currently allowed to attack
        if (attackTimer != 0 && canAttack == false)
        {
            attackTimer -= Time.deltaTime;
        }

        if (attackTimer <= 0)
        {
            canAttack = true;
        }

        target = GameObject.FindGameObjectWithTag("Player").transform;
        float distance = Vector3.Distance(target.position, transform.position);

        if (playerSighted)
        {
            //Sophias Work
            anim.SetBool("running", true);
            anim.SetInteger("condition", 1);
            agent.SetDestination(target.position);
            previousSighting = target.position;

            if (distance <= agent.stoppingDistance)
            {
                //Sophias Work
                anim.SetBool("running", false);
                anim.SetInteger("condition", 0);
                FaceTarget();


                //Done by Sophia
                if (canAttack)
                {
                    Attacking();
                }

            }
        }



        //Audio Detection
        //For Audio later
        if (CalculatePathLength(target.position) <= triggerRadius)
        {
            previousSighting = target.position;
        }

        //Visual Detection
        if (distance <= triggerRadius)
        {
            //Finds the current direction that the player is facing
            Vector3 direction = target.position - transform.position;
            float angle = Vector3.Angle(direction, transform.position);

            //If it finds that the player is within half the angle for the field of view, it has seen the player
            if (angle <= angleFOV / 2)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position + (transform.up * 2.25f), direction.normalized, out hit, attackRadius))
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        playerSighted = true;
                        sightedLoc = target.position;
                        player = hit.collider.gameObject;
                    }
                }
            }
        }
    }

    //I DON'T KNOW WHY THIS FUNCTION WORKS BUT IT WORK, SO SCREW IT AND I AM GOING TO SLEEP
    //Calculates the length of the path between the player and the enemy
    private float CalculatePathLength(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();

        if (agent.enabled)

            agent.CalculatePath(targetPosition, path);

        //Finds all the corners on the path and adds them all up to get the total distance
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        //Enemies current location
        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        //Will always look one less times because there is always one less distance for the total of corners
        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        float pathLength = 0f;

        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }


        return pathLength;
    }

    private void FixedUpdate()
    {
        if (beenHit)
        {
            healthPoints -= 1;
            beenHit = false;
        }

    }


    //Done by Sophia
    void Attacking()
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
        attackTimer = 1;
    }

    //Displays the triggerRadius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, triggerRadius);
    }

    //Rotates blade to face player
    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        //Smooths out the rotation speed
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Flying Rock")
        {
            beenHit = true;
        }
    }




}
