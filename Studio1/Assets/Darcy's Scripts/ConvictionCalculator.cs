using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvictionCalculator : MonoBehaviour
{
    [SerializeField]
    private Text conviction;

    public float convictionCount; 
    private float naturalTickAmount, tickTimeCount, tierResetTimeCount; 

    void Start()
    {
        convictionCount = 50.0f; 
    }
    void Update()
    {
        if(convictionCount > 5.0f)
        {
            naturalTickAmount = 0.10f * convictionCount; //naturaltickamount is 10 percent of whatever the faith is 
            tickTimeCount += Time.deltaTime;
            if (tickTimeCount >= 3) //allows for the tick to happen every set amount of seconds
            {
                convictionCount -= naturalTickAmount; //faith loses 10 percent of its value every tick
                tickTimeCount = 0; //resetting time each tick
            }
        }
        if(convictionCount <= 5.0f)
        {
            naturalTickAmount = 0.5f; //naturaltickamount now goes down by 0.5
            tickTimeCount += Time.deltaTime;
            if (tickTimeCount >= 3) //allows for the tick to happen every set amount of seconds
            {
                convictionCount -= naturalTickAmount; //faith goes down by 0.5 every tick
                tickTimeCount = 0; //resetting time each tick
            }
        }
        if (convictionCount >= 100.0f)
        {
            tierResetTimeCount += Time.deltaTime;
            if (tierResetTimeCount >= 1.5)
            {
                convictionCount = 50.0f;
                tierResetTimeCount = 0;
            }           
        }
        if (convictionCount < 0.0f)
        {
            tierResetTimeCount += Time.deltaTime;
            if (tierResetTimeCount >= 1.5)
            {
                convictionCount = 90.0f;
                tierResetTimeCount = 0;
            }
        }
        conviction.text = convictionCount.ToString("0.0");
    }
}
