using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exitMenu : MonoBehaviour
{
    public GameObject menuBase;
    public GameObject pauseMenuObj;
    
    // Start is called before the first frame update
    void Start()
    {
        menuBase.SetActive(false);
    }

    void mainButton()
    {
        SceneManager.LoadScene("MainMenu");
        menuBase.SetActive(false);
    }

    void desktopButton()
    {
        Application.Quit();
    }

    void backButton()
    {
        menuBase.SetActive(false);
        pauseMenuObj.SetActive(true);
    }


}
