using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make the object float up and down while spinning gently
public class Floater : MonoBehaviour
{
    // Start is called before the first frame update
    // User Input
    public float degreesPerSecond = 15.0f; //Spinning speed
    public float amplitude = 0.5f; //Height
    public float frequency = 1f; //Movement speed

    //Position Storage Variables
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3();

    // Use this for initialization
    void Start()
    {
        //Store the starting position and rotation of the object
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Spin Object on y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        //Float up/down with Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}
