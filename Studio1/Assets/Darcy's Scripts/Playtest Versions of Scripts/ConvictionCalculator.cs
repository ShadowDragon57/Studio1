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
            naturalTickAmount = 0.10f * convictionCount; //naturaltickamount is 10 percent of whatever the conviction is 
            tickTimeCount += Time.deltaTime;

            if (tickTimeCount >= 10) //allows for the tick to happen every set amount of seconds
            { 
                convictionCount -= naturalTickAmount; //conviction loses 10 percent of its value every tick
                tickTimeCount = 0; //resetting time each tick
            }
        }
        if(convictionCount <= 5.0f)
        {
            naturalTickAmount = 0.5f; //naturaltickamount now goes down by 0.5
            tickTimeCount += Time.deltaTime;

            if (tickTimeCount >= 10) //allows for the tick to happen every set amount of seconds
            { 
                convictionCount -= naturalTickAmount; //conviction goes down by 0.5 every tick
                tickTimeCount = 0; //resetting time each tick
            }
        }
        if (convictionCount >= 100.0f) //if conviction reaches 100, it will reset back to 50 after 1.5 seconds
        {
            tierResetTimeCount += Time.deltaTime;
            if (tierResetTimeCount >= 1.5) // it occurs after 1.5 to allow for the consequences script to execute its stuff before the value gets reset
            {
                convictionCount = 50.0f;
                tierResetTimeCount = 0;
            }           
        }
        if (convictionCount < 0.0f) //if conviction reaches 0, it will reset back to 90 after 1.5 seconds
        {
            tierResetTimeCount += Time.deltaTime;
            if (tierResetTimeCount >= 1.5)
            {
                convictionCount = 90.0f;
                tierResetTimeCount = 0;
            }
        }
        conviction.text = convictionCount.ToString("0.0"); //writing to the canvas
    }

    public void EnemyKilledByCoat() //adding or decreasing the conviction count if coat kills something or if the guardian kills something, respectively.
    {
        convictionCount -= 10f;
    }

    public void EnemyKilledByGuardian()
    {
        convictionCount += 10f;
    }

    public void CollisionInTutorial()
    {
        convictionCount += 100f;
    }
}
