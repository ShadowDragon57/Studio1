using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BladeAI : MonoBehaviour
{

    //Public variables
    public float fieldOfViewAngle = 110f;
    public bool playerSighted = false;
    public Animator anim;

    //Timers
    [SerializeField]
    private float timer = 10;
    private float previousTimerValue;
    [SerializeField]
    private float attackTimer = 1;
    private float previousAttackValue = 1;

    public int timesHit = 0;

    //Private References
    private SphereCollider col;
    private GameObject player;
    private Vector3 playerLoc;
    private Vector3 previousPlayerPosition;
    private NavMeshAgent agent;

    //Health and such
    public int bladeHealth = 4;

    //Attack Variables
    public bool inRange = false;
    public bool canAttack= false;

    //Speed Variables
    public float movementSpeed = 20;
    public bool refreshTrigger = false;


    // Start is called before the first frame update
    public void Awake()
    {
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    public void Start()
    {
        previousAttackValue = attackTimer;
        previousTimerValue = timer;
    }

    // Update is called once per frame
    void Update()
    {
        playerLoc = player.GetComponent<Transform>().position;

        //Checks if player is currently allowed to attack
        if (attackTimer != 0 && canAttack == false)
        {
            attackTimer -= Time.deltaTime;
        }

        if (attackTimer <= 0)
        {
            canAttack = true;
        }

        if (playerSighted && !inRange)
        {
            //Done by Sophia
            transform.position = Vector3.MoveTowards(transform.position, playerLoc, movementSpeed * Time.deltaTime);
            Vector3 direction = playerLoc - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;

            anim.SetBool("running", true);
            anim.SetInteger("condition", 1);

        }

        //Stops the player if they're
        if (Vector3.Distance(transform.position, playerLoc) <= 5f && playerSighted)
        {
            //Activate Attack Animation. Deactivate Running Animation
            //Done by Sophia
            inRange = true;
            anim.SetBool("running", false);
            anim.SetInteger("condition", 0);
            transform.rotation = transform.rotation;
            
        }

        if (inRange)
        {
            if (timer >=0)
            {
                //Resets scene if player dies
                timer -= Time.deltaTime;
            }

            if (timer <= 0)
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }

            //Done by Sophia
            if (canAttack)
            {
                Attacking();
            }
        }



        if (Vector3.Distance(transform.position, playerLoc) >= 5f)
        {
            inRange = false;
            //Activate Running Animation

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

    public void FixedUpdate()
    {
        //Continually checks if player has entered field of vision

        if (bladeHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (refreshTrigger)
        {
            col.enabled = false;
            col.enabled = true;
            refreshTrigger = false;
        }
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("throwRock"))
        {
            bladeHealth -= 1;
        }
    }

    //Used to detect if the player is within the enemies field of view
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Grabs the direction the enemy is currently facing
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            //If it finds that the player is within half the angle for the field of view, it has seen the player
            if (angle <= fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;


                /*As most object will begin raycasting from the bottom of their feet, there's potential that it might hit the ground
                So transform.position + transform.up brings it approciamately to the middle
                transform.up is essentially 1 unit. Coat is 4.5 units tall. So 2.25 is the middle point of Coat*/
                if (Physics.Raycast(transform.position + (transform.up * 2.25f), direction.normalized, out hit, col.radius))
                {
                    //When the player gets detected
                    if (hit.collider.gameObject.CompareTag("Player") && !inRange)
                    {
                        playerSighted = true;
                        previousPlayerPosition = player.transform.position;
                    }
                }

                ////Later for audio

                //if (CalculatePathLength(player.transform.position) <= col.radius)
                //{
                //    previousPlayerPosition = player.transform.position;
                //}
            }

            else
            {
                refreshTrigger = true;
            }
        }

    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        playerSighted = false;
    //    }
    //}


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
                pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i+1]);
            }
        

        return pathLength;
    }

}
