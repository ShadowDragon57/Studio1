using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class Abilities2 : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private GameObject strike, earthen, sun, qAbilityActive, eAbilityActive;

    public void StrikeCast()
    {
        Destroy(qAbilityActive); //destroying the previous instantiated objects, if any
        qAbilityActive = Instantiate(strike, new Vector3(player.position.x, player.position.y + 2, player.position.z), player.rotation); //creating the ability
    }

    public void EarthenCast()
    {
        Destroy(eAbilityActive);
        eAbilityActive = Instantiate(earthen, new Vector3(player.position.x, player.position.y + 1, player.position.z), Quaternion.identity);
    }

    public void SunCast()
    {
        Destroy(qAbilityActive);
        float direction = player.rotation.y; //getting the value of where the player is looking, this is so that i can make the ability spawn in front of coat.
        if(direction == 0)
        {
            qAbilityActive = Instantiate(sun, new Vector3(player.position.x , player.position.y + 1, player.position.z), player.rotation);
        }
        if(direction == 90)
        {
            qAbilityActive = Instantiate(sun, new Vector3(player.position.x + 5, player.position.y + 1, player.position.z), player.rotation);
        }
    }

    void OnParticleCollision(GameObject other) //if the ability hits something with the enemy layer it destroys it
    {
        Destroy(other);
    }
}
