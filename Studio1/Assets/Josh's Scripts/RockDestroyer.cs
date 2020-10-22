using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class RockDestroyer : MonoBehaviour
{
    public GuardianController guardian;
    public Rigidbody rb;

    public float timer = 5;
    public bool collisionReached = false;
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
                Destroy(gameObject);
            }
        }

    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 0 && col.gameObject.name != "Flying Rock")
        {
            collisionReached = true;
            GameObject guardianController = GameObject.Find("Guardian Controller");
            guardian = guardianController.GetComponent<GuardianController>();

            guardian.flyingRock = false;
            Destroy(gameObject);
        }
    }
}
