using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class RockDestroyer : MonoBehaviour
{
    public GuardianController2 guardian;
    public Rigidbody rb;
    public ConvictionCalculator conviction;

    private GameObject currentEnemy; 

    public float timer = 3;
    public bool collisionReached = false;
    public bool hitEnemy = false;

    public void Awake()
    {
        collisionReached = false;

        //If there are errors, ensure that the gameObject holding the guardian controller is named like this
        GameObject guardianController = GameObject.Find("Guardian Controller");
        guardian = guardianController.GetComponent<GuardianController2>();
    }

    public void Update()
    {
        if (gameObject.name == "Flying Rock")
        {
            Quaternion rotation = guardian.playerRotation;

            transform.position += Vector3.MoveTowards(transform.position, guardian.hitPosition, 100 * Time.deltaTime);
            transform.rotation = rotation;
        }

        if (collisionReached == false)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                guardian.flyingRock = false;
                Destroy(gameObject);
            }
        }

        if (currentEnemy != null)
        {
            if (hitEnemy)
            {

                hitEnemy = false;
            }
        }

    }


    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 0 && col.gameObject.name != "Flying Rock")
        {
            guardian.flyingRock = false;
            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("Blade"))
        {
            currentEnemy = col.gameObject;
            currentEnemy.GetComponent<BladeAI>().bladeHealth -= 1;
            hitEnemy = true;
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
