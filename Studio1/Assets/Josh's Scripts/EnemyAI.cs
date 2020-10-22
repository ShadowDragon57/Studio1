using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //Reference for player
    public GameObject player;
    public Vector3 target;

    //Distances
    public int alertDistance = 10;
    public int attackDistance = 5;
    
    //Timer
    float timer1 = 3;

    //Conditions
    bool attackTime = false;
    bool alertTimer = false;

    //Other Vars
    public float moveSpeed = 100f;

    public void Awake()
    {
        player = GameObject.Find("CoatBase");
        target = player.GetComponent<Transform>().position;
    }


    // Update is called once per frame
    void Update()
    {
        //Alerted State
        //Checks for the distance between the player and the object
        //If player get too close, they will be alerted
        if (Vector3.Distance(target, transform.position) <= alertDistance)
        {
            Vector3 direction = target - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;

            alertTimer = true;
        }

        //If they player gets even closer, they'll be attacked

        //if (Vector3.Distance(target.position, transform.position) <= attackDistance)
        //{
        //    transform.position += transform.forward * moveSpeed * Time.deltaTime;
        //}

        //If the player stays within the alert radious for too long, they will be attacked
        if (alertTimer == true && timer1 >= 0)
        {
            timer1 -= Time.deltaTime;
        }

        //Causes the enemy to get all up in the players face and yeet off screen
        if (timer1 <= 0 && gameObject.name == "Bomb")
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.time);
        }

        //Follows the player around 
        if (timer1 <= 0)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}
