using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvictionConsequences : MonoBehaviour
{
    [SerializeField]
    private Text health;

    [SerializeField]
    private float healthCount, convictionCount, healthAfterChange;
    [SerializeField]
    private GameObject[] tiers = new GameObject[5];

    private GameObject activeTier, oneAbove, oneBelow;

    void Start()
    {
        foreach(GameObject s in tiers) //turning off all of the spheres
        {
            s.SetActive(false); 
        }
        tiers[3].SetActive(true); //turning back on the third one
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
        convictionCount = GetComponent<ConvictionCalculator>().convictionCount;
        foreach(GameObject s in tiers)
        {
            if(s.activeInHierarchy == true)
            {
                activeTier = s;
            }
        }
        if(convictionCount <= 0.0f)
        {
            activeTier.SetActive(false);
        }
    }
}
