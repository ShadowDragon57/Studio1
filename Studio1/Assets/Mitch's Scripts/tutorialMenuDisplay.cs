using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialMenuDisplay : MonoBehaviour
{
    public Texture movement, cameraCard, interactables1, interactables2, interactables3, rockTutorial1, rockTutorial2, abilities1, abilities2, abilities3, pause, reset1, reset2, sprint;

    public GameObject fadingPanel, cardDisplay;

    public GameObject nextObj, previousObj, quitObj;

    bool nextCardBool, previousCardBool, check;

    public int max, current, ID;

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

        current = 0;
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
    }

    public void SetCardDisplay(int cardIDNum)
    {
        ID = cardIDNum;
        
        switch (cardIDNum)
        {
            case 0: //Movement
                cardDisplay.GetComponent<RawImage>().texture = movement;
                max = 1;
                break;
            case 1: //Camera
                cardDisplay.GetComponent<RawImage>().texture = cameraCard;
                max = 1;
                break;
            case 2: //Interactables
                cardDisplay.GetComponent<RawImage>().texture = tutorialCardsInteractables[current];
                max = 3;
                break;
            case 3: //Rocks
                cardDisplay.GetComponent<RawImage>().texture = tutorialCardsRockTutorial[current];
                max = 2;
                break;
            case 4: //Abilities
                cardDisplay.GetComponent<RawImage>().texture = tutorialCardsAbilities[current];
                max = 3;
                break;
            case 5: //Pause
                cardDisplay.GetComponent<RawImage>().texture = pause;
                break;
            case 6: //Reset
                cardDisplay.GetComponent<RawImage>().texture = tutorialCardsReset[current];
                max = 2;
                break;
            case 7: //Sprint
                cardDisplay.GetComponent<RawImage>().texture = sprint;
                break;
        }
    }

    public void displayArrows()
    {
        if (max > current)
        {
            nextObj.SetActive(true);
        }
        else
        {
            nextObj.SetActive(false);
        }

        if (current > 0)
        {
            previousObj.SetActive(true);
        }
        else
        {
            previousObj.SetActive(false);
        }
    }

    public void nextCard()
    {
        current++;
        nextCardBool = false;
        SetCardDisplay(ID);
        displayArrows();
    }

    public void previousCard()
    {
        current--;
        previousCardBool = false;
        SetCardDisplay(ID);
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
        current = 0;
    }

    public void begin(int cardID)
    {
        SetCardDisplay(cardID);
        fade.SetBool("openPanel", true);
    }
}
