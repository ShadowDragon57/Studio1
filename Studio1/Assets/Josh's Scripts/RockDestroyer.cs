using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class RockDestroyer : MonoBehaviour
{
    public GuardianController guardian;
    public Rigidbody rb;
    public ConvictionCalculator conviction;

    private GameObject currentEnemy; 

    public float timer = 5;
    public bool collisionReached = false;
    public bool hitEnemy = false;

    public void Update()
    {
        if (gameObject.name == "Flying Rock")
        {
            GameObject guardianController = GameObject.Find("Guardian Controller");
            guardian = guardianController.GetComponent<GuardianController>();
            Quaternion rotation = guardian.playerRotation;
            Vector3 direction = rotation * Vector3.forward;

            transform.position += direction * Time.deltaTime * 100;
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
        GameObject guardianController = GameObject.Find("Guardian Controller");
        guardian = guardianController.GetComponent<GuardianController>();

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
            guardian.playerHit = true;
            conviction.convictionCount -= 10;
            guardian.flyingRock = false;
            Destroy(gameObject);
        }
    }
}
