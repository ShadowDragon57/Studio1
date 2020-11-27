using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))] //adds a rigid body attribute to the scripted object, cannot be removed
public class Attributes : MonoBehaviour
{
    //Allows us to added various attributes to an object
    // Start is called before the first frame update
    [Range(0, 10)]
    public int x;

    [Range(0, 10)]
    public int y;

    [Header("Health Settings")]
    public int health = 0;
    public int maxHealth = 100;

    [Header("Test Settings")]
    [TextArea]
    public string MyTextArea;

    [Space(20)]

    [Tooltip("Stamina value between 0 and 100")]
    public int stamina;

    [Multiline(2)]
    public string s = "Hello World";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
