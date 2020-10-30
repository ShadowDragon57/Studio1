using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stoning : MonoBehaviour
{
    //References
    public GuardianController2 guardian;
    public Rigidbody rb;
    public ConvictionCalculator conviction;

    //Public Vars
    public float timer = 3;
    public bool collisionReached;

    // Start is called before the first frame update
    void Awake()
    {
        collisionReached = false;

        //If there are errors, ensure that the gameObject holding the guardian controller is named like this
        GameObject guardianController = GameObject.Find("Guardian Controller");
        guardian = guardianController.GetComponent<GuardianController2>();
    }

    // Update is called once per frame
    void Update()
    {
        //Sends the gameObject flying towards it's given location
        if (gameObject.name == "Flying Rock")
        {
            Quaternion rotation = guardian.playerRotation;

            transform.position += Vector3.MoveTowards(transform.position, guardian.hitPosition, 100 * Time.deltaTime);
            transform.rotation = rotation;
        }

        //Destroys the rock after a certain amount of time has passed
        if (!collisionReached)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                guardian.flyingRock = false;
                Destroy(gameObject);
            }
        }
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 0 && col.gameObject.name != "Flying Rock")
        {
            guardian.flyingRock = false;
            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("Blade"))
        {
            guardian.flyingRock = false;
            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("Player"))
        {
            guardian.antiMouseLock = true;
            conviction.convictionCount -= 10;
            guardian.flyingRock = false;
            Destroy(gameObject);
        }

        else
        {
            guardian.flyingRock = false;
            Destroy(gameObject);
        }
    }
}
