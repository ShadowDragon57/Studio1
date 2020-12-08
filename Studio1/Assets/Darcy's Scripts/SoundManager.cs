using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip test, ambient; 
    public AudioClip mainMenuClip;
    [SerializeField]
    private bool mainMenu = true;

    void Start()
    {
        PlayMainMenu(mainMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayAmbient();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            mainMenu ^= true; //this swaps the bool over, if its false, makes it true, and if its true, makes it false.
            PlayMainMenu(mainMenu);
        }
        AudioSource mainMenuSource = GameObject.Find("MainMenu").GetComponent<AudioSource>();
        mainMenuSource.volume = GameObject.Find("Slider").GetComponent<Slider>().value;
    }
    public void PlaySound() //test from prototype, can be deleted when stuff works
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(test);
    }

    public void PlayAmbient() //ambient background music for during game
    {
        GameObject ambientGameObject = new GameObject("Ambient"); //to make sounds work in unity, audio sources have to be attached to gameobjects,
                                                                  //and then the clips have to attached to those audio sources
        AudioSource ambientSource = ambientGameObject.AddComponent<AudioSource>();
        ambientSource.clip = ambient; //the audio sourse can then be changed within the code to allow for things such as volume, looping, spacial sound, etc.
        ambientSource.loop = true;
        ambientSource.Play();
    }

    public void PlayMainMenu(bool mainMenu)
    {
        if (GameObject.FindGameObjectsWithTag("Music").Length == 0)
        {
            GameObject mainMenuGameObject = new GameObject("MainMenu");
            mainMenuGameObject.tag = "Music";
            AudioSource temp = GameObject.Find("MainMenu").AddComponent<AudioSource>();
        }
        AudioSource mainMenuSource = GameObject.Find("MainMenu").GetComponent<AudioSource>();
        if (mainMenu)
        {
            mainMenuSource.clip = mainMenuClip;
            mainMenuSource.loop = true;
            mainMenuSource.Play();
        }
        if(!mainMenu)
        {
            mainMenuSource.Stop();
        }
    }
}
