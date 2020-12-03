using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCards : MonoBehaviour
{
    public Texture movement, cameraCard, interactables1, interactables2, interactables3, rockTutorial1, rockTutorial2, abilities1, abilities2, abilities3, pause, reset1, reset2, sprint;

    GameObject cardPickup;

    public GameObject variableList;

    public GameObject fadingPanel, cardDisplay;

    public GameObject nextObj, previousObj, quitObj;

    bool nextCardBool, previousCardBool, check;

    int currentDisplay;

    List<Texture> tutorialCardsInteractables = new List<Texture>();
    List<Texture> tutorialCardsRockTutorial = new List<Texture>();
    List<Texture> tutorialCardsAbilities = new List<Texture>();
    List<Texture> tutorialCardsReset = new List<Texture>();

    Animator fade, card;

    // Start is called before the first frame update
    void Start()
    {
        tutorialCardsInteractables.Add(interactables1);
        tutorialCardsInteractables.Add(interactables2);
        tutorialCardsInteractables.Add(interactables3);

        tutorialCardsRockTutorial.Add(rockTutorial1);
        tutorialCardsRockTutorial.Add(rockTutorial2);

        tutorialCardsAbilities.Add(abilities1);
        tutorialCardsAbilities.Add(abilities2);
        tutorialCardsAbilities.Add(abilities3);

        tutorialCardsReset.Add(reset1);
        tutorialCardsReset.Add(reset2);

        nextObj.SetActive(false);
        previousObj.SetActive(false);
        quitObj.SetActive(false);
        nextCardBool = false;
        previousCardBool = false;

        fade = fadingPanel.GetComponent<Animator>();
        card = cardDisplay.GetComponent<Animator>();

        check = false;

        currentDisplay = 0;
    }

    void Update()
    {
        if (fade.GetCurrentAnimatorStateInfo(0).IsName("black"))
        {
            card.SetBool("openCard", true);
            if (card.GetCurrentAnimatorStateInfo(0).IsName("showCard"))
            {
                card.SetBool("openCard", false);
                if (check == false)
                {
                    displayArrows();
                    check = true;
                }
                check = false;
            }
        }
        if (fade.GetCurrentAnimatorStateInfo(0).IsName("New State"))
        {
            card.SetBool("closeCard", false);
            fade.SetBool("closePanel", false);
        }
    }

    void SetCardDisplay(int cardID)
    {
        switch (cardID)
        {
            case 0: //Movement
                cardDisplay.GetComponent<RawImage>().texture = movement;
                variableList.GetComponent<tutorialPageVariables>().movementBool = true;
                break;
            case 1: //Camera
                cardDisplay.GetComponent<RawImage>().texture = cameraCard;
                variableList.GetComponent<tutorialPageVariables>().cameraCardBool = true;
                break;
            case 2: //Interactables
                cardDisplay.GetComponent<RawImage>().texture = tutorialCardsInteractables[currentDisplay];
                variableList.GetComponent<tutorialPageVariables>().interactables1Bool = true;
                break;
            case 3: //Rocks
                cardDisplay.GetComponent<RawImage>().texture = tutorialCardsRockTutorial[currentDisplay];
                variableList.GetComponent<tutorialPageVariables>().rockTutorial1Bool = true;
                break;
            case 4: //Abilities
                cardDisplay.GetComponent<RawImage>().texture = tutorialCardsAbilities[currentDisplay];
                variableList.GetComponent<tutorialPageVariables>().abilities1Bool = true;
                break;
            case 5: //Pause
                cardDisplay.GetComponent<RawImage>().texture = pause;
                variableList.GetComponent<tutorialPageVariables>().pauseBool = true;
                break;
            case 6: //Reset
                cardDisplay.GetComponent<RawImage>().texture = tutorialCardsReset[currentDisplay];
                variableList.GetComponent<tutorialPageVariables>().reset1Bool = true;
                break;
            case 7: //Sprint
                cardDisplay.GetComponent<RawImage>().texture = sprint;
                variableList.GetComponent<tutorialPageVariables>().sprintBool = true;
                break;
        }
    }

    public void displayArrows()
    {
        Debug.Log("Arrows");
        if (cardPickup.GetComponent<cardVariables>().seriesNum - 1 > currentDisplay)
        {
            nextObj.SetActive(true);
            Debug.Log("ArrowsN");
        }
        else
        {
            nextObj.SetActive(false);
        }

        if (currentDisplay > 0)
        {
            previousObj.SetActive(true);
            Debug.Log("ArrowsP");
        }
        else
        {
            previousObj.SetActive(false);
        }
        quitObj.SetActive(true);
    }

    public void nextCard()
    {
        currentDisplay++;
        nextCardBool = false;
        SetCardDisplay(cardPickup.GetComponent<cardVariables>().cardID);
        displayArrows();
    }

    public void previousCard()
    {
        currentDisplay--;
        previousCardBool = false;
        SetCardDisplay(cardPickup.GetComponent<cardVariables>().cardID);
        displayArrows();
    }


    public void quit()
    {
        fade.SetBool("openPanel", false);
        card.SetBool("closeCard", true);
        fade.SetBool("closePanel", true);
        nextObj.SetActive(false);
        previousObj.SetActive(false);
        quitObj.SetActive(false);
        cardPickup = null;
        currentDisplay = 0;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("TutorialCard") == true)
        {
            cardPickup = collision.gameObject;
            SetCardDisplay(cardPickup.GetComponent<cardVariables>().cardID);
            Debug.Log(collision.name);
            cardPickup.SetActive(false);
            fade.SetBool("openPanel", true);
        }
    }
}
