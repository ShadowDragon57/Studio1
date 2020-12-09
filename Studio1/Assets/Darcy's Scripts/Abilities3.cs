using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class Abilities3 : MonoBehaviour
{
    [SerializeField]
    private Transform player, boosterTransform, earthenTransform, shieldTransform, pulseTransform, voidTransform;
    [SerializeField]
    private Transform[] bladeTransforms;
    [SerializeField]
    private GameObject strike, earthen, sun, wall, beam, booster, lance, shield, pulse, voidAbility, abilityUsed, earthenTemp;
    public GameObject qAbilityActive, eAbilityActive;
    [SerializeField]
    private GameObject[] bladeObjects;

    private Vector3 playerPos, playerDirection, spawnPos; //these values can be changed for each ability meaning that each ability can have unique properties.

    private string abilitySound;

    private float spawnDis, boostCount, earthenCount, wallCount, beamCount, shieldCount, voidCount, pulseCount, elapsedQ, elapsedE, elapsedCollision;

    private float moveSpeed = 0.1f;
    private float[] distancesToPlayer, distancesToEarthen;

    private bool beamActive = false;
    private bool shieldActive = false;
    private bool voidActive = false;
    private bool pulseActive = false;
    private bool earthenActive = false;
    private bool wallActive = false;
    public bool boostActive = false;
    private bool ableToCollide = true;

    void Update()
    {
        //timer to count to only allow damage to occur to blade every set amount of seconds
        if (elapsedCollision >= 0.5f)
        {
            elapsedCollision = 0;
            ableToCollide = true;
        }

        //functionality for earthen to destroy itself and suck enemies in
        if(earthenActive && earthenCount < 2.8f)
        {
            earthenCount += Time.deltaTime;
            elapsedE += Time.deltaTime;

            earthenTemp = GameObject.Find("Earthen(Clone)");
            bladeObjects = GameObject.FindGameObjectsWithTag("Blade"); //finds all the enemies tagged with blade
            bladeTransforms = new Transform[bladeObjects.Length]; //makes the transfrom array the same size as the gameobjects one
            distancesToEarthen = new float[bladeObjects.Length]; //makes the float array the same size as the gameobjects one
            if (elapsedE >= 0.01f) //this if statement happens every 0.01 seconds
            {
                elapsedE %= 0.01f; //this makes the elapsed time counter reset itself every 0.01 seconds
                for (int i = 0; i < bladeObjects.Length; i++)
                {
                    bladeTransforms[i] = bladeObjects[i].transform; //turns all the gameobjects into transforms and puts them in the array
                    distancesToEarthen[i] = Vector3.Distance(earthenTemp.transform.position, bladeTransforms[i].position);
                }
                for (int i = 0; i < bladeTransforms.Length; i++)
                {
                    if (distancesToEarthen[i] <= 30) //checks to see if the enemies are within a certain distance to the player
                    {
                        bladeTransforms[i].position = Vector3.MoveTowards(bladeTransforms[i].position, earthenTemp.transform.position, moveSpeed); //moves all the transforms toward the player
                    }
                }
            }
        }
        if(earthenCount >= 2.8f)
        {
            Destroy(eAbilityActive);
            earthenActive = false;
            earthenCount = 0;
        }

        //functionality for beam following and rotating with player, and space to add for it to stop the player from moving when used.
        if (beamActive == true && beamCount < 3) //making the beam follow the character, and counts the amount of time for it to be active.
        {
            beamCount += Time.deltaTime;
            playerPos = new Vector3(player.position.x, player.position.y + 2, player.position.z);
            qAbilityActive.transform.position = playerPos;
            qAbilityActive.transform.rotation = player.rotation;
        }
        if(beamCount >= 3) //deactivating the ability
        {
            Destroy(qAbilityActive);
            beamCount = 0;
            beamActive = false;
        }
        
        //functionaility for shield following player, and space for it to reduce damage to player once we add health.
        if(shieldActive == true && shieldCount < 2.65) 
        {
            shieldCount += Time.deltaTime;
            playerPos = new Vector3(player.position.x, player.position.y + 2, player.position.z);
            eAbilityActive.transform.position = playerPos;
        }
        if(shieldCount >= 2.65)
        {
            Destroy(eAbilityActive);
            shieldCount = 0;
            shieldActive = false;
        }

        //functionality for void 'pulling toward player' ability
        if(voidActive == true && voidCount < 3f)
        {
            voidCount += Time.deltaTime;
            elapsedE += Time.deltaTime;
            playerPos = new Vector3(player.position.x, player.position.y + 2, player.position.z);
            eAbilityActive.transform.position = playerPos;

            bladeObjects = GameObject.FindGameObjectsWithTag("Blade"); //finds all the enemies tagged with blade
            bladeTransforms = new Transform[bladeObjects.Length]; //makes the transfrom array the same size as the gameobjects one
            distancesToPlayer = new float[bladeObjects.Length]; //makes the float array the same size as the gameobjects one
            if (elapsedE >= 0.01f) //this if statement happens every 0.01 seconds
            {
                elapsedE %= 0.01f; //this makes the elapsed time counter reset itself every 0.01 seconds
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
        if(voidCount >= 3f)
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
                        bladeTransforms[i].position = Vector3.MoveTowards(bladeTransforms[i].position, player.position, -1 * moveSpeed); //moves all the transforms toward the player
                    }
                }
            }
        }
        if(pulseCount >= 1)
        {
            pulseCount = 0;
            pulseActive = false;
        }
        //functionality for boost speed and destroy(speed is changed in playermovement script
        if(boostActive && boostCount < 3)
        {
            boostCount += Time.deltaTime;
        }
        if(boostCount >= 3)
        {
            boostCount = 0;
            boostActive = false;
            Destroy(eAbilityActive);
        }

        //functionality to destroy wall
        if(wallActive && wallCount < 4)
        {
            wallCount += Time.deltaTime;
        }
        if(wallCount >= 4)
        {
            wallCount = 0;
            wallActive = false;
            Destroy(eAbilityActive);
        }
    }

    //animosity abilties
    public void StrikeCast()
    {
        playerPos = new Vector3(player.position.x, player.position.y + 2, player.position.z);
        qAbilityActive = Instantiate(strike, playerPos, player.rotation); //creating the ability
        abilitySound = "strike";
        GameObject.Find("SoundManager").GetComponent<SoundManager>().AbilitySounds(abilitySound);
    }

    public void EarthenCast()
    {
        earthenActive = true;
        playerPos = player.position;
        playerDirection = player.forward;
        spawnDis = 3;
        spawnPos = playerPos + playerDirection * spawnDis;
        eAbilityActive = Instantiate(earthen, spawnPos, earthenTransform.rotation); //these values make the earthen portal always spawn where the player is looking, 
                                                                                    //and slightly in front of the player.
        abilitySound = "earthen";
        GameObject.Find("SoundManager").GetComponent<SoundManager>().AbilitySounds(abilitySound);
    }

    //discontent abilities
    public void SunCast()
    {
        playerPos = new Vector3(player.position.x, player.position.y + 2, player.position.z);
        playerDirection = player.forward;
        spawnDis = 5;
        spawnPos = playerPos + playerDirection * spawnDis;
        qAbilityActive = Instantiate(sun, spawnPos, player.rotation);
        abilitySound = "sun";
        GameObject.Find("SoundManager").GetComponent<SoundManager>().AbilitySounds(abilitySound);
    }
    public void WallCast()
    {
        playerPos = new Vector3(player.position.x, player.position.y - 22, player.position.z);
        playerDirection = player.forward;
        spawnDis = 4;
        spawnPos = playerPos + playerDirection * spawnDis;
        wallActive = true;
        eAbilityActive = Instantiate(wall, spawnPos, player.rotation);
        abilitySound = "wall";
        GameObject.Find("SoundManager").GetComponent<SoundManager>().AbilitySounds(abilitySound);
    }

    //bliss abilties
    public void BeamCast()
    {
        playerPos = new Vector3(player.position.x, player.position.y + 2, player.position.z);
        qAbilityActive = Instantiate(beam, playerPos, player.rotation);
        beamActive = true;
        abilitySound = "beam";
        GameObject.Find("SoundManager").GetComponent<SoundManager>().AbilitySounds(abilitySound);
    }

    public void BoosterCast()
    {
        playerPos = player.position;
        playerDirection = player.forward;
        spawnPos = playerPos + playerDirection;
        boostActive = true;
        eAbilityActive = Instantiate(booster, spawnPos, boosterTransform.rotation);
        abilitySound = "boost";
        GameObject.Find("SoundManager").GetComponent<SoundManager>().AbilitySounds(abilitySound);
    }

    //hatred abilities
    public void LanceCast()
    {
        playerPos = new Vector3(player.position.x, player.position.y + 2, player.position.z);
        qAbilityActive = Instantiate(lance, playerPos, player.rotation);
        abilitySound = "lance";
        GameObject.Find("SoundManager").GetComponent<SoundManager>().AbilitySounds(abilitySound);
    }

    public void ShieldCast()
    {
        playerPos = new Vector3(player.position.x, player.position.y + 2, player.position.z);
        shieldActive = true;
        eAbilityActive = Instantiate(shield, playerPos, shieldTransform.rotation);
        abilitySound = "shield";
        GameObject.Find("SoundManager").GetComponent<SoundManager>().AbilitySounds(abilitySound);
    }

    //revelry abilities
    public void PulseCast()
    {
        playerPos = player.position;
        pulseActive = true;
        qAbilityActive = Instantiate(pulse, playerPos, pulseTransform.rotation);
        abilitySound = "pulse";
        GameObject.Find("SoundManager").GetComponent<SoundManager>().AbilitySounds(abilitySound);
    }

    public void VoidCast()
    {
        playerPos = player.position;     
        voidActive = true;
        eAbilityActive = Instantiate(voidAbility, playerPos, voidTransform.rotation);
        abilitySound = "void";
        GameObject.Find("SoundManager").GetComponent<SoundManager>().AbilitySounds(abilitySound);
    }

    void OnParticleCollision(GameObject other) //if the ability hits something with the enemy layer it does damage to it based on the ability used
    {
        abilityUsed = GameObject.Find("Abilities").GetComponent<Abilities3>().qAbilityActive;
        elapsedCollision += Time.deltaTime;
        if (ableToCollide)
        {
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
            ableToCollide = false;
        }
    }
}
