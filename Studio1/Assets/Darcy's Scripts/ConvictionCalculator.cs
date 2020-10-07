using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvictionCalculator : MonoBehaviour
{
    [SerializeField]
    private Text conviction;

    public float convictionCount; 
    private float naturalTickAmount, timeCount; 

    void Start()
    {
        convictionCount = 50.0f; 
    }
    void Update()
    {
        naturalTickAmount = 0.10f * convictionCount; //naturaltickamount is 10 percent of whatever the faith is 
        timeCount += Time.deltaTime;
        if(timeCount >= 1) //allows for the tick to happen every set amount of seconds
        {
            convictionCount -= naturalTickAmount; //faith loses 10 percent of its value every tick
            conviction.text = convictionCount.ToString("0.0"); 
            timeCount = 0; //resetting time each tick
        }
    }
}
