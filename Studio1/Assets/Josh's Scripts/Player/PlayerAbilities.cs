using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour
{
    //References
    Text qCoolDownTxt, eCoolDownTxt;
    GameObject currentIdeology;
    PlayerMovement player;

    //Private Vars
    bool qAbiUsed, eAbiUsed, boostActive, activated;
    float qTimer, eTimer, boostTimer;


    // Start is called before the first frame update
    void Start()
    {
        //Defining References
        qCoolDownTxt = GameObject.Find("QCoolDown").GetComponent<Text>();
        eCoolDownTxt = GameObject.Find("ECoolDown").GetComponent<Text>();

        qAbiUsed = false;
        eAbiUsed = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentIdeology = GameObject.Find("Canvas").GetComponent<ConvictionConsequences>().currentIdeology;

        #region Ability Timers

        if (qAbiUsed)
        {
            qTimer -= Time.deltaTime;

            if (qTimer <= 0)
            {
                qAbiUsed = false;
            }
        }

        if (eAbiUsed)
        {
            eTimer -= Time.deltaTime;

            if (eTimer <= 0)
            {
                eAbiUsed = false;
            }
        }
        #endregion

        #region Boost Ideology

        //Activates the changes that occur for boost
        if (boostActive)
        {
            player.leftSpeed = 35f;
            player.rightSpeed = 35f;
            player.forwardSpeed = 55f;
            player.backSpeed = 35f;
            boostTimer = 3;
            boostTimer -= Time.deltaTime;

            if (boostTimer <= 0)
            {
                boostActive = false;
            }
        }
        #endregion
    }

    void FixedUpdate()
    {
        #region Q Abilities;
        if (Input.GetKey(KeyCode.Q) && qAbiUsed == false)
        {
            //Changes Q Ability based on current ideology
            if (currentIdeology.CompareTag("revelry"))
            {
                qAbiUsed = true;
            }

            if (currentIdeology.CompareTag("bliss"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities2>().BeamCast();
                qAbiUsed = true;
            }

            if (currentIdeology.CompareTag("animosity"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities2>().StrikeCast();
                qAbiUsed = true;
            }

            if (currentIdeology.CompareTag("discontent"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities2>().SunCast();
                qAbiUsed = true;
            }

            if (currentIdeology.CompareTag("hatred"))
            {
                qAbiUsed = true;
            }
        }

        #endregion

        #region E Abilities
        if (Input.GetKey(KeyCode.E) && eAbiUsed == false)
        {
            //Changes Q Ability based on current ideology
            if (currentIdeology.CompareTag("revelry"))
            {
                eAbiUsed = true;
            }

            if (currentIdeology.CompareTag("bliss"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities2>().BoosterCast();
                boostActive = true;
                eAbiUsed = true;
            }

            if (currentIdeology.CompareTag("animosity"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities2>().EarthenCast();
                eAbiUsed = true;
            }

            if (currentIdeology.CompareTag("discontent"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities2>().WallCast();
                eAbiUsed = true;
            }

            if (currentIdeology.CompareTag("hatred"))
            {
                eAbiUsed = true;
            }
        }
        #endregion
    }
}
