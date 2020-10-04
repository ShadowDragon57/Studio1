using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaithCalculator : MonoBehaviour
{
    [SerializeField]
    private Text faith;

    public float faithCount; 
    private float naturalTickAmount, timeCount; 

    void Start()
    {
        faithCount = 50.0f; 
    }
    void Update()
    {
        naturalTickAmount = 0.10f * faithCount; //naturaltickamount is 10 percent of whatever the faith is 
        timeCount += Time.deltaTime;
        if(timeCount >= 3) //allows for the tick to happen every set amount of seconds
        {
            faithCount -= naturalTickAmount; //faith loses 10 percent of its value every tick
            faith.text = faithCount.ToString("0.0"); 
            timeCount = 0; //resetting time each tick
        }
    }
}
