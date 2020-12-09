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
    public bool qAbiUsed, eAbiUsed, activated;
    public float qTimer, eTimer, boostTimer;


    // Start is called before the first frame update
    void Start()
    {
        //Defining References
        qCoolDownTxt = GameObject.Find("QCoolDown").GetComponent<Text>();
        eCoolDownTxt = GameObject.Find("ECoolDown").GetComponent<Text>();

        qAbiUsed = false;
        eAbiUsed = false;
        qTimer = 3f;
        eTimer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        currentIdeology = GameObject.Find("Canvas").GetComponent<ConvictionConsequences1>().currentIdeology;

        #region Ability Timers

        if (qAbiUsed)
        {
            qTimer -= Time.deltaTime;

            if (qTimer <= 0.0f)
            {
                qTimer = 3f;
                qAbiUsed = false;
            }
        }

        if (eAbiUsed)
        {
            eTimer -= Time.deltaTime;
            
            if (eTimer <= 0.0f)
            {
                eTimer = 5f;
                eAbiUsed = false;
            }
        }
        #endregion
    }

    void FixedUpdate()
    {
        #region Q Abilities;
        if (Input.GetKey(KeyCode.Q) && !qAbiUsed)
        {
            //Changes Q Ability based on current ideology
            if (currentIdeology.CompareTag("revelry"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities3>().PulseCast();
                qAbiUsed = true;
            }

            if (currentIdeology.CompareTag("bliss"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities3>().BeamCast();
                qAbiUsed = true;
            }

            if (currentIdeology.CompareTag("animosity"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities3>().StrikeCast();
                qAbiUsed = true;
            }

            if (currentIdeology.CompareTag("discontent"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities3>().SunCast();
                qAbiUsed = true;
            }

            if (currentIdeology.CompareTag("hatred"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities3>().LanceCast();
                qAbiUsed = true;
            }
        }

        #endregion

        #region E Abilities
        if (Input.GetKey(KeyCode.E) && !eAbiUsed)
        {
            //Changes Q Ability based on current ideology
            if (currentIdeology.CompareTag("revelry"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities3>().VoidCast();
                eAbiUsed = true;
            }

            if (currentIdeology.CompareTag("bliss"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities3>().BoosterCast();
                eAbiUsed = true;
            }

            if (currentIdeology.CompareTag("animosity"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities3>().EarthenCast();
                eAbiUsed = true;
            }

            if (currentIdeology.CompareTag("discontent"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities3>().WallCast();
                eAbiUsed = true;
            }

            if (currentIdeology.CompareTag("hatred"))
            {
                GameObject.Find("Abilities").GetComponent<Abilities3>().ShieldCast();
                eAbiUsed = true;
            }
        }
        #endregion
    }
}
