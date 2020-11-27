using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class Abilities3 : MonoBehaviour
{
    [SerializeField]
    private Transform player, boosterTransform, earthenTransform, shieldTransform, pulseTransform, voidTransform, targetTransform;
    [SerializeField]
    private Transform[] bladeTransforms;
    [SerializeField]
    private GameObject strike, earthen, sun, wall, beam, booster, lance, shield, pulse, voidAbility, targetObject, qAbilityActive, eAbilityActive, abilityUsed;
    [SerializeField]
    private GameObject[] bladeObjects;

    private Vector3 playerPos, playerDirection, spawnPos; //these values can be changed for each ability meaning that each ability can have unique properties.

    private float spawnDis, beamCount, shieldCount, voidCount, pulseCount, elapsedQ, elapsedE;
    private float moveSpeed = 0.1f;
    private float[] distancesToPlayer;

    private bool beamActive = false;
    private bool shieldActive = false;
    private bool voidActive = false;
    private bool pulseActive = false;

    void Update()
    {
        //functionaliy for beam following and rotating with player, and space to add for it to stop the player from moving when used.
        if(beamActive == true && beamCount < 3) //making the beam follow the character, and counts the amount of time for it to be active.
        {
            beamCount += Time.deltaTime;
            playerPos = new Vector3(player.position.x, player.position.y + 2, player.position.z);
            qAbilityActive.transform.position = playerPos;
            qAbilityActive.transform.rotation = player.rotation;
        }
        if(beamCount >= 3) //deactivating the ability
        {
<<<<<<< Updated upstream
            GameObject.Find("Player (Josh)").GetComponent<PlayerController3>().BeamDeactivated();
=======
            //GameObject.Find("Player Controller").GetComponent<PlayerController3>().BeamDeactivated();
>>>>>>> Stashed changes
            Destroy(qAbilityActive);
            beamCount = 0;
            beamActive = false;
        }
        
        //functionaility for shield following player, and space for it to reduce damage to player once we add health.
        if(shieldActive == true && shieldCount < 5) //same thing, but for the shield ability
        {
            shieldCount += Time.deltaTime;
            playerPos = new Vector3(player.position.x, player.position.y + 2, player.position.z);
            eAbilityActive.transform.position = playerPos;
        }
        if(shieldCount >= 5)
        {
            Destroy(eAbilityActive);
            shieldCount = 0;
            shieldActive = false;
        }

        //functionality for void 'pulling toward player' ability
        if(voidActive == true && voidCount < 1.5f)
        {
            voidCount += Time.deltaTime;
            elapsedE += Time.deltaTime;
            playerPos = new Vector3(player.position.x, player.position.y + 2, player.position.z);
            eAbilityActive.transform.position = playerPos;

            bladeObjects = GameObject.FindGameObjectsWithTag("Blade"); //finds all the enemies tagged with blade
            bladeTransforms = new Transform[bladeObjects.Length]; //makes the transfrom array the same size as the gameobjects one
            distancesToPlayer = new float[bladeObjects.Length]; //makes the float array the same size as the gameobjects one
            if (elapsedE >= 0.01f) //this if statement happens every 0.3 seconds
            {
                elapsedE %= 0.01f; //this makes the elapsed time counter reset itself every 0.3 seconds
                for (int i = 0; i < bladeObjects.Length; i++)
                {
                    bladeTransforms[i] = bladeObjects[i].transform; //turns all the gameobjects into transforms and puts them in the array
                    distancesToPlayer[i] = Vector3.Distance(player.position, bladeTransforms[i].position);
                }
                for (int i = 0; i < bladeTransforms.Length; i++)
                {
                    if(distancesToPlayer[i] <= 15) //checks to see if the enemies are within a certain distance to the player
                    {
                        bladeTransforms[i].position = Vector3.MoveTowards(bladeTransforms[i].position, player.position, moveSpeed); //moves all the transforms toward the player
                    }           
                }
            }
        }
        if(voidCount >= 1.5f)
        {
            Destroy(eAbilityActive);
            voidCount = 0;
            voidActive = false;
        }

        //functionality for the pulse 'moving enemies away from player' ability
        if(pulseActive == true && pulseCount < 1)
        {
            pulseCount += Time.deltaTime;
            elapsedQ += Time.deltaTime;

            bladeObjects = GameObject.FindGameObjectsWithTag("Blade");
            bladeTransforms = new Transform[bladeObjects.Length];
            distancesToPlayer = new float[bladeObjects.Length];
            if (elapsedQ >= 0.01f)
            {
                elapsedQ %= 0.01f;
                for (int i = 0; i < bladeObjects.Length; i++)
                {
                    bladeTransforms[i] = bladeObjects[i].transform; //turns all the gameobjects into transforms and puts them in the array
                    distancesToPlayer[i] = Vector3.Distance(player.position, bladeTransforms[i].position);
                }
                for (int i = 0; i < bladeTransforms.Length; i++)
                {
                    if (distancesToPlayer[i] <= 10) //checks to see if the enemies are within a certain distance to the player
                    {
                        bladeTransforms[i].position = Vector3.MoveTowards(bladeTransforms[i].position, player.position, moveSpeed); //moves all the transforms toward the player
                    }
                }
            }
        }
        if(pulseCount >= 1)
        {
            pulseCount = 0;
            pulseActive = false;
        }
    }

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
<<<<<<< Updated upstream
        playerPos = new Vector3(player.position.x, player.position.y - 22, player.position.z);
=======
        playerPos = new Vector3(player.position.x, player.position.y, player.position.z);
>>>>>>> Stashed changes
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
        beamActive = true;
<<<<<<< Updated upstream
        GameObject.Find("Player (Josh)").GetComponent<PlayerController3>().BeamActivated();
=======
        //GameObject.Find("Player (Josh)").GetComponent<PlayerController3>().BeamActivated();
>>>>>>> Stashed changes
    }

    public void BoosterCast()
    {
        playerPos = player.position;
        playerDirection = player.forward;
        spawnPos = playerPos + playerDirection;
        Destroy(eAbilityActive);
        eAbilityActive = Instantiate(booster, spawnPos, boosterTransform.rotation);
<<<<<<< Updated upstream
        GameObject.Find("Player (Josh)").GetComponent<PlayerController3>().BoosterActivated();
=======
        //GameObject.Find("Player (Josh)").GetComponent<PlayerController3>().BoosterActivated();
>>>>>>> Stashed changes
    }

    //hatred abilities
    public void LanceCast()
    {
        playerPos = new Vector3(player.position.x, player.position.y + 2, player.position.z);
        Destroy(qAbilityActive);
        qAbilityActive = Instantiate(lance, playerPos, player.rotation);
    }

    public void ShieldCast()
    {
        playerPos = new Vector3(player.position.x, player.position.y + 2, player.position.z);
        Destroy(eAbilityActive);
        shieldActive = true;
        eAbilityActive = Instantiate(shield, playerPos, shieldTransform.rotation);
    }

    //revelry abilities
    public void PulseCast()
    {
        playerPos = player.position;
        Destroy(qAbilityActive);
        pulseActive = true;
        qAbilityActive = Instantiate(pulse, playerPos, pulseTransform.rotation);
    }

    public void VoidCast()
    {
        playerPos = player.position;
        Destroy(eAbilityActive);        
        voidActive = true;
        eAbilityActive = Instantiate(voidAbility, playerPos, voidTransform.rotation);
    }

    public void DestroyQAbility() //destroying the active abilities using the timer from the player controller
    {
        Destroy(qAbilityActive);
    }

    public void DestroyEAbility()
    {
        Destroy(eAbilityActive);
    }

    void OnParticleCollision(GameObject other) //if the ability hits something with the enemy layer it does damage to it based on the ability used
    {
        abilityUsed = GameObject.Find("Abilities").GetComponent<Abilities3>().qAbilityActive;
        switch (abilityUsed.name)
        {
            case "Pulse(Clone)":
                other.GetComponent<BladeHealth>().PulseDamage();
                break;
            case "Beam(Clone)":
                other.GetComponent<BladeHealth>().BeamDamage();
                break;
            case "Strike(Clone)":
                other.GetComponent<BladeHealth>().StrikeDamage();
                break;
            case "Sun(Clone)":
                other.GetComponent<BladeHealth>().SunDamage();
                break;
            case "Lance(Clone)":
                other.GetComponent<BladeHealth>().LanceDamage();
                break;
        }
    }
}
