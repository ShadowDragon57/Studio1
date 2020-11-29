using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialPageVariables : MonoBehaviour
{
    public bool movementBool, cameraCardBool, interactables1Bool, rockTutorial1Bool, abilities1Bool, pauseBool, reset1Bool, sprintBool;

    void Start()
    {
        movementBool = false;
        cameraCardBool = false;
        interactables1Bool = false;
        rockTutorial1Bool = false;
        abilities1Bool = false;
        pauseBool = false;
        reset1Bool = false;
        sprintBool = false;
    }
}
