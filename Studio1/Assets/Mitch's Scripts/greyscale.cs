using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greyscale : MonoBehaviour
{
    float saturaton;
    Renderer GreyRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        GreyRenderer = GetComponent<Renderer>();
        saturaton = -100;

        GreyRenderer.material.color = Color.HSVToRGB(0, saturaton, 0);
    }


}
