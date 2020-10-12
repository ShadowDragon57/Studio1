using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvictionConsequences : MonoBehaviour
{
    [SerializeField]
    private Text health;

    [SerializeField]
    private float healthCount, convictionCount, healthAfterChange, timeCount;
    [SerializeField]
    private GameObject[] tiers = new GameObject[5];
  
    private GameObject activeTier, oneAbove, oneBelow;

    private int current;

    private bool changeOnce;

    void Start()
    {
        foreach(var s in tiers) //turning off all of the spheres
        {
            s.SetActive(false); 
        }
        current = 2;
        changeOnce = true;
        tiers[current].SetActive(true); //turning back on the third one
        healthCount = 100.0f;
        health.text = healthCount.ToString();
    }

    void Update()
    {
        HealthScaledByFaith();
        WhichTier();
    }

    private void HealthScaledByFaith()
    {
        convictionCount = GetComponent<ConvictionCalculator>().convictionCount; //grabbing the value of the faith from the calculator
        if(convictionCount <= 90 && convictionCount > 75) //these if statements control how much health the character has, scaled based                                                
        {                                           //on the current faith
            healthAfterChange = 1.50f * healthCount; //increased by half
        }
        if(convictionCount <= 75 && convictionCount > 50)
        {
            healthAfterChange = 1.25f * healthCount; //increased by a quarter
        }
        if(convictionCount <= 50 && convictionCount > 25)
        {
            healthAfterChange = healthCount; //default value
        }
        if(convictionCount <= 25 && convictionCount > 10)
        {
            healthAfterChange = 0.75f * healthCount; //decreased by a quarter
        }
        if(convictionCount <= 10)
        {
            healthAfterChange = 0.50f * healthCount; //decrease by half
        }
        health.text = healthAfterChange.ToString("0");
    }

    private void WhichTier()
    {
        convictionCount = GetComponent<ConvictionCalculator>().convictionCount; //getting the value of conviction from the calculator

        if(current == 4) //this checks to see if the array is at its max
        {
            activeTier = tiers[current];
            oneBelow = tiers[current - 1];
        }
        else if (current == 0) //this checks to see if the array is at its min
        {
            activeTier = tiers[current];
            oneAbove = tiers[current + 1];
        }
        else //this is only allowed to happen if the array is in between its min and max
        {
            activeTier = tiers[current];
            oneAbove = tiers[current + 1];
            oneBelow = tiers[current - 1];
        }

        if(changeOnce == true) // the bool makes it so that the if statement will only happen once
        {
            if (convictionCount >= 100.0f) //checking to see if the conviction is at its max
            {
                if(current < 4) //only allowed to happen if the array isnt at its max
                {
                    activeTier.SetActive(false);
                    oneAbove.SetActive(true);
                    current++;
                }
                changeOnce = false;
            }
            if (convictionCount <= 0.0f) //checking to see if the conviction is at its min
            {
                if(current > 0) //only allowed to happen if the array isnt at its min
                {
                    activeTier.SetActive(false);
                    oneBelow.SetActive(true);
                    current--;
                }
                changeOnce = false;
            }
        }

        if(changeOnce == false) //if the bool is false, it will count to 1.5 and set it back to true, this allows it to only happen once every 1.5 seconds
        {
            timeCount += Time.deltaTime;
        }
        if(timeCount >= 1.5)
        {
            changeOnce = true; 
            timeCount = 0;
        }
    }
}
