using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public RawImage coat;
    public Texture coatOn, coatOff;
    Texture coatTexture;
    float ticker = 3.0f;
    bool completed;

    // Start is called before the first frame update
    void Start()
    {
        completed = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartButton()
    {
        CoatDisplay();
        if (completed == true)
        {
            SceneManager.LoadScene("Tutorial redux part 1");
        }
    }

    public void OptionsButton()
    {
        Debug.Log("Options");
    }

    public void ExitButton()
    {
        Application.Quit();
    }


    void CoatDisplay()
    {
        Debug.Log("Activated");
        
        if (ticker > 0)
        {
            ticker -= Time.deltaTime;
            
            if (ticker%2 == 0)
            {
                coat.texture = coatOn;
            }
            else
            {
                coat.texture = coatOff;
            }
        }
        completed = true;

    }
}
