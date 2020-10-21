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

    private GameObject activeTier, oneAbove, oneBelow, currentIdeology;

    private int current;

    private bool changeOnce, changeHealth;

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
        changeHealth = false;
    }

    void Update()
    {
        if(changeHealth == true)
        {
            HealthScaledByIdeology();
            changeHealth = false;
        }
        WhichIdeology();
    }

    private void HealthScaledByIdeology()
    {
        currentIdeology = tiers[current];
        if (currentIdeology.CompareTag("revelry")) //reverly causes the health to be doubled
        {
            healthAfterChange = healthCount * 2;
            healthCount = healthAfterChange;
            health.text = healthCount.ToString();
        }
        if (currentIdeology.CompareTag("bliss")) //bliss causes health to be increased by 50 percent.
        {
            healthAfterChange = healthCount * 1.5f;
            healthCount = healthAfterChange;
            health.text = healthCount.ToString();
        }
        if (currentIdeology.CompareTag("animosity")) //animosity is the default- there is no health change. however i have kept it here just in case we want to add something
        {
        }
        if (currentIdeology.CompareTag("discontent")) //discontent reduces health by 50 percent.
        {
            healthAfterChange = healthCount * 0.50f;
            healthCount = healthAfterChange;
            health.text = healthCount.ToString();
        }
        if (currentIdeology.CompareTag("hatred")) //hatred halves health
        {
            healthAfterChange = healthCount / 2;
            healthCount = healthAfterChange;
            health.text = healthCount.ToString();
        }
    }

    private void WhichIdeology()
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
                    changeHealth = true;
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
                    changeHealth = true;
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
