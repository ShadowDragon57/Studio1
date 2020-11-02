using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostIdeology : MonoBehaviour
{
    bool triggered, once;
    public GameObject triggeredZone;
    public ConvictionCalculator conviction;

    // Update is called once per frame
    void Start()
    {
        triggered = false;
        once = false;
    }

    void Update()
    {
        if (triggered == true && once == false)
        {
            conviction.convictionCount += 100;
            once = true;
            Debug.Log("Once");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Entered");
        if (collision.gameObject.name == "Player")
        {
            triggered = true;
        }
    }
}
