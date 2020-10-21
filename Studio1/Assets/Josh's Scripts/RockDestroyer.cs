using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDestroyer : MonoBehaviour
{
    public GuardianController guardian;
    public Rigidbody rb;

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
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 0 && col.gameObject.name != "Flying Rock")
        {
            GameObject guardianController = GameObject.Find("Guardian Controller");
            guardian = guardianController.GetComponent<GuardianController>();

            guardian.flyingRock = false;
            Destroy(gameObject);
        }
    }
}
