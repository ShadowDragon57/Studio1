using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianInventory : MonoBehaviour
{
    public int rocksCollected, rockLimit;

    // Update is called once per frame
    void Start()
    {
        rockLimit = 16;
    }
}
