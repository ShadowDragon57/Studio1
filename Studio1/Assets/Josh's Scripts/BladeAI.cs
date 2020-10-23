using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class BladeAI : MonoBehaviour
{

    //Public variables
    public float fieldOfViewAngle = 110f;
    public bool playerSighted = false;
    public Animator anim;
    

    //Private References
    private SphereCollider col;
    private GameObject player;
    private Vector3 playerLoc;
    private Vector3 formerTransform;
    private Quaternion formerRotation;




    //Speed Variables
    public float movementSpeed = 200;
    public bool refreshTrigger = false;
    public bool inRange = false;
    public bool repeatAttack = false;

    // Start is called before the first frame update
    public void Awake()
    {
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    // Update is called once per frame
    void Update()
    {
        playerLoc = player.GetComponent<Transform>().position;

        if (playerSighted && !inRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerLoc, movementSpeed * Time.deltaTime);
            Vector3 direction = playerLoc - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;

            anim.SetBool("running", true);
            anim.SetInteger("condition", 1);

        }

        //Stops the player if they're
        if (Vector3.Distance(transform.position, playerLoc) <= 5f)
        {
            //Activate Attack Animation. Deactivate Running Animation
            inRange = true;
            anim.SetBool("running", false);
            anim.SetInteger("condition", 0);
            transform.rotation = transform.rotation;
            
        }

        if (inRange)
        {
            Attacking();
        }

        if (Vector3.Distance(transform.position, playerLoc) >= 5f)
        {
            inRange = false;
            //Activate Running Animation

        }

    }

    void Attacking()

    {
        StartCoroutine(AttackRoutine());
    }
    IEnumerator AttackRoutine()
    {
        anim.SetBool("attacking", true);
        anim.SetInteger("condition", 2);
        yield return new WaitForSeconds(2);
        anim.SetBool("attacking", false);
        anim.SetInteger("condition", 0);
    }

    public void FixedUpdate()
    {
        //Continually checks if player has entered field of vision

        if (refreshTrigger)
        {
            col.enabled = false;
            col.enabled = true;
            refreshTrigger = false;
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
            if (angle < fieldOfViewAngle)
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
                    }
                }
                else
                {
                    refreshTrigger = true;
                }

            }
        }




    }
}
