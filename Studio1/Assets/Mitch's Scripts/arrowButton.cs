using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowButton : MonoBehaviour
{
    public GameObject obj;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0) && this.name == "NextArrow")
        {
            obj.GetComponent<TutorialCards>().nextCard();
        }
        else if (Input.GetMouseButtonUp(0) && this.name == "PreviousArrow")
        {
            obj.GetComponent<TutorialCards>().previousCard();
        }
    }
}
