using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    Text playerHealth;

    public float playerHP = 10000;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("playerHealthTxt").GetComponent<Text>();
    }
}
