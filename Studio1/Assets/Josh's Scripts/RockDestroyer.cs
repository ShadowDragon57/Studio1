using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDestroyer : MonoBehaviour
{
    public GuardianController guardian;

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 0)
        {
            guardian.flyingRock = false;
            Destroy(gameObject);
        }
    }
}
