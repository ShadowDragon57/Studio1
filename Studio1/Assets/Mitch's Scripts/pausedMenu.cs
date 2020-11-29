using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausedMenu : MonoBehaviour
{
	public GameObject menuBase;

	public GameObject Hatred, Discontent, Animosity, Bliss, Revelry;
	public GameObject HatredMenu, DiscontentMenu, AnimosityMenu, BlissMenu, RevelryMenu;

	public GameObject optionObj, tutorialObj, exitObj;

	// Start is called before the first frame update

	void Start()
    {
		Time.timeScale = 1;

		menuBase.SetActive(false);
		HatredMenu.SetActive(false);
		DiscontentMenu.SetActive(false);
		AnimosityMenu.SetActive(false);
		BlissMenu.SetActive(false);
		RevelryMenu.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (Time.timeScale == 1)
			{
				Time.timeScale = 0;
				menuBase.SetActive(true);
				showMenu().SetActive(true);
			}
			else if (Time.timeScale == 0)
			{
				Time.timeScale = 1;
				menuBase.SetActive(false);
				showMenu().SetActive(false);
			}
		}
	}

	GameObject showMenu()
    {
		if(Hatred.activeInHierarchy == true)
        {
			return HatredMenu;
        }
		else if (Discontent.activeInHierarchy == true)
		{
			return DiscontentMenu;
		}
		else if(Animosity.activeInHierarchy == true)
		{
			return AnimosityMenu;
		}
		else if (Bliss.activeInHierarchy == true)
		{
			return BlissMenu;
		}
		else if (Revelry.activeInHierarchy == true)
		{
			return RevelryMenu;
		}
		else
		{
			return null;
        }
    }

	public void continueButton()
    {
		Time.timeScale = 1;
		menuBase.SetActive(false);
		showMenu().SetActive(false);
	}

	public void optionsMenu()
    {

    }

	public void tutorialButton()
    {

    }

	public void exitButton()
    {
		menuBase.SetActive(false);
		showMenu().SetActive(false);
		exitObj.SetActive(true);
	}

}
