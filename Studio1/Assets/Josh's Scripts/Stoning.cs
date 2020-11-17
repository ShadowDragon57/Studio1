using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stoning : MonoBehaviour
{
    //References
    GuardianAttack guardian;
    ConvictionCalculator conviction;

    //Public Vars
    public bool collisionReached;
    public bool test = true;

    //Private Vars
    private Vector3 endPoint;

    private float speed = 100;
    private float timer = 3;

    private bool callibration = true;



    // Start is called before the first frame update
    void Awake()
    {
        collisionReached = false;

        //If there are errors, ensure that the gameObject holding the guardian controller is named like this
        guardian = GameObject.Find("Player Controller").GetComponent<GuardianAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        //Sends the gameObject flying towards it's given location
        if (gameObject.name == "Flying Rock")
        {
            //Ensures that the rock will only face one direction
            if (callibration == true)
            {
                endPoint = guardian.hitPosition;
                transform.LookAt(endPoint);
                callibration = false;
            }


            Quaternion rotation = transform.rotation;
            Vector3 direction = rotation * Vector3.forward;
            transform.position += direction * speed * Time.deltaTime;
        }

        //Destroys the rock after a certain amount of time has passed
        if (!collisionReached)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 0 && col.gameObject.name != "Flying Rock")
        {
            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("Player"))
        {
            conviction.convictionCount -= 10;
            Destroy(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
