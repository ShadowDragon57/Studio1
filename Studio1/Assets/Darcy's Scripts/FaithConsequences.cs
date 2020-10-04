using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaithConsequences : MonoBehaviour
{
    [SerializeField]
    private Text health;

    [SerializeField]
    private float healthCount, healthChange, healthAfterChange;

    void Start()
    {
        healthCount = 100.0f;
        health.text = healthCount.ToString();
    }

    void Update()
    {
        HealthScaledByFaith();
    }

    void HealthScaledByFaith()
    {
        healthChange = GetComponent<FaithCalculator>().faithCount; //grabbing the value of the faith from the calculator
        if(healthChange <= 90 && healthChange > 75) //these if statements control how much health the character has, scaled based                                                
        {                                           //on the current faith
            healthAfterChange = 1.50f * healthCount; //increased by half
        }
        if(healthChange <= 75 && healthChange > 50)
        {
            healthAfterChange = 1.25f * healthCount; //increased by a quarter
        }
        if(healthChange <= 50 && healthChange > 25)
        {
            healthAfterChange = healthCount; //default value
        }
        if(healthChange <= 25 && healthChange > 10)
        {
            healthAfterChange = 0.75f * healthCount; //decreased by a quarter
        }
        if(healthChange <= 10)
        {
            healthAfterChange = 0.50f * healthCount; //decrease by half
        }
        health.text = healthAfterChange.ToString("0");
    }
}
