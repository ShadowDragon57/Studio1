using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialMenu : MonoBehaviour
{
    public GameObject menuBase, variables, tutorialCardsObj;

    public GameObject page1, page2;
    public GameObject movementObj, cameraCardObj, interactables1Obj, rockTutorial1Obj, abilities1Obj, pauseObj, reset1Obj, sprintObj;
    public Texture movement, cameraCard, interactables1, rockTutorial1, abilities1, pause, reset1, sprint;

    // Start is called before the first frame update
    void Start()
    {
        menuBase.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (variables.GetComponent<tutorialPageVariables>().movementBool == true)
        {
            movementObj.GetComponent<RawImage>().texture = movement;
        }
        if (variables.GetComponent<tutorialPageVariables>().cameraCardBool == true)
        {
            cameraCardObj.GetComponent<RawImage>().texture = cameraCard;
        }
        if (variables.GetComponent<tutorialPageVariables>().interactables1Bool == true)
        {
            interactables1Obj.GetComponent<RawImage>().texture = interactables1;
        }
        if (variables.GetComponent<tutorialPageVariables>().rockTutorial1Bool == true)
        {
            rockTutorial1Obj.GetComponent<RawImage>().texture = rockTutorial1;
        }
        if (variables.GetComponent<tutorialPageVariables>().abilities1Bool == true)
        {
            abilities1Obj.GetComponent<RawImage>().texture = abilities1;
        }
        if (variables.GetComponent<tutorialPageVariables>().pauseBool == true)
        {
            pauseObj.GetComponent<RawImage>().texture = pause;
        }
        if (variables.GetComponent<tutorialPageVariables>().reset1Bool == true)
        {
            reset1Obj.GetComponent<RawImage>().texture = reset1;
        }
        if (variables.GetComponent<tutorialPageVariables>().sprintBool == true)
        {
            sprintObj.GetComponent<RawImage>().texture = sprint;
        }
    }

    public void nextPageButton()
    {
        page1.SetActive(false);
        page2.SetActive(true);
    }

    public void previousPageButton()
    {
        page1.SetActive(true);
        page2.SetActive(false);
    }

    public void MovementButton()
    {
        tutorialCardsObj.GetComponent<tutorialMenuDisplay>().begin(0);
        menuBase.SetActive(false);

    }
    public void cameraButton()
    {
        tutorialCardsObj.GetComponent<tutorialMenuDisplay>().begin(1);
        menuBase.SetActive(false);
    }
    public void interactableButton()
    {
        tutorialCardsObj.GetComponent<tutorialMenuDisplay>().begin(2);
        menuBase.SetActive(false);
    }
    public void rocksButton()
    {
        tutorialCardsObj.GetComponent<tutorialMenuDisplay>().begin(3);
        menuBase.SetActive(false);
    }
    public void abilititesButton()
    {
        tutorialCardsObj.GetComponent<tutorialMenuDisplay>().begin(4);
        menuBase.SetActive(false);
    }
    public void pauseButton()
    {
        tutorialCardsObj.GetComponent<tutorialMenuDisplay>().begin(5);
        menuBase.SetActive(false);
    }
    public void resetButton()
    {
        tutorialCardsObj.GetComponent<tutorialMenuDisplay>().begin(6);
        menuBase.SetActive(false);
    }
    public void sprintButton()
    {
        tutorialCardsObj.GetComponent<tutorialMenuDisplay>().begin(7);
        menuBase.SetActive(false);
    }
}
