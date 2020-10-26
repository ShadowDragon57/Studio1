using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class Abilities2 : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private GameObject strike, earthen, qAbilityActive, eAbilityActive;

    public void StrikeCast()
    {
        Destroy(qAbilityActive); //destroying the previous instantiated objects, if any
        qAbilityActive = Instantiate(strike, player.position, player.rotation); //creating the ability
    }

    public void EarthenCast()
    {
        Destroy(eAbilityActive);
        eAbilityActive = Instantiate(earthen, player.position, Quaternion.identity);
    }

    void OnParticleCollision(GameObject other) //if the ability hits something with the enemy layer it destroys it
    {
        Destroy(other);
    }
}
