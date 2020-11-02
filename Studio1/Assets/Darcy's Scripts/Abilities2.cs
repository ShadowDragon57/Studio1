using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class Abilities2 : MonoBehaviour
{
    [SerializeField]
    private Transform player, boosterTransform, earthenTransform;
    [SerializeField]
    private GameObject strike, earthen, sun, wall, beam, booster, qAbilityActive, eAbilityActive;

    private Vector3 playerPos, playerDirection, spawnPos; //these values can be changed for each ability meaning that each ability can have unique properties.

    private float spawnDis;

    //animosity abilties
    public void StrikeCast()
    {
        playerPos = new Vector3(player.position.x, player.position.y + 2, player.position.z);
        Destroy(qAbilityActive); //destroying the previous instantiated objects, if any
        qAbilityActive = Instantiate(strike, playerPos, player.rotation); //creating the ability
    }

    public void EarthenCast()
    {
        playerPos = player.position;
        playerDirection = player.forward;
        spawnDis = 3;
        spawnPos = playerPos + playerDirection * spawnDis;
        Destroy(eAbilityActive);
        eAbilityActive = Instantiate(earthen, spawnPos, earthenTransform.rotation); //these values make the earthen portal always spawn where the player is looking, 
                                                                                    //and slightly in front of the player.
    }

    //discontent abilities
    public void SunCast()
    {
        playerPos = new Vector3(player.position.x, player.position.y + 2, player.position.z);
        playerDirection = player.forward;
        spawnDis = 5;
        spawnPos = playerPos + playerDirection * spawnDis;
        Destroy(qAbilityActive);
        qAbilityActive = Instantiate(sun, spawnPos, player.rotation);
    }

    public void WallCast()
    {
        playerPos = new Vector3(player.position.x, player.position.y - 22, player.position.z);
        playerDirection = player.forward;
        spawnDis = 4;
        spawnPos = playerPos + playerDirection * spawnDis;
        Destroy(eAbilityActive);
        eAbilityActive = Instantiate(wall, spawnPos, player.rotation);
    }

    //bliss abilties
    public void BeamCast()
    {
        playerPos = new Vector3(player.position.x, player.position.y + 2, player.position.z);
        Destroy(qAbilityActive); 
        qAbilityActive = Instantiate(beam, playerPos, player.rotation);
    }

    public void BoosterCast()
    {
        playerPos = player.position;
        playerDirection = player.forward;
        spawnPos = playerPos + playerDirection;
        Destroy(eAbilityActive);
        eAbilityActive = Instantiate(booster, spawnPos, boosterTransform.rotation);
        GameObject.Find("Player").GetComponent<PlayerController2>().BoosterActivated();
    }

    void OnParticleCollision(GameObject other) //if the ability hits something with the enemy layer it destroys it
    {
        Destroy(other);
        GameObject.Find("Canvas").GetComponent<ConvictionCalculator>().EnemyKilledByCoat();
    }
}
