using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip musicClip, paperRustle, voidClip, pulse, boost, beam, earthen, strike, wall, sun, shield, lance;
    private GameObject qAbilityGameObject, eAbilityGameObject, paperObject;
    private bool canPlayMusic = true;
    private bool isTalking = false;

    void Start()
    {
        PlayMusic(canPlayMusic);
        qAbilityGameObject = new GameObject("qAbilitySoundObject");
        eAbilityGameObject = new GameObject("eAbilitySoundObject");
        paperObject = new GameObject("PaperRustle"); 

        AudioSource qAbilityTemp = GameObject.Find("qAbilitySoundObject").AddComponent<AudioSource>();
        AudioSource eAbilityTemp = GameObject.Find("eAbilitySoundObject").AddComponent<AudioSource>();
        AudioSource paperTemp = GameObject.Find("PaperRustle").AddComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            canPlayMusic ^= true; //this swaps the bool over, if its false, makes it true, and if its true, makes it false.
            PlayMusic(canPlayMusic);
        }
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            isTalking ^= true;
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            PaperRustle();
        }
        VolumeControl(isTalking);
    }

    public void PlayMusic(bool canPlayMusic) //this method plays the music, is called at start, and turns off when needed to, based on the boolean's value
    {
        if (GameObject.FindGameObjectsWithTag("Music").Length == 0) //ensuring that only one object gets created when called from an update method
        {
            GameObject musicObject = new GameObject("MusicPlaying");
            musicObject.tag = "Music";
            AudioSource temp = GameObject.Find("MusicPlaying").AddComponent<AudioSource>();
        }
        AudioSource musicSource = GameObject.Find("MusicPlaying").GetComponent<AudioSource>();
        if (canPlayMusic)
        {
            musicSource.clip = musicClip;
            musicSource.loop = true;
            musicSource.Play();
        }
        if(!canPlayMusic)
        {
            musicSource.Stop();
        }
    }

    public void PaperRustle() //paper rustling method that is called when the player picks up the tutorial or secret cards
    {
        AudioSource paperRustleSource = GameObject.Find("PaperRustle").GetComponent<AudioSource>();
        paperRustleSource.clip = paperRustle;
        paperRustleSource.Play();
    }

    public void AbilitySounds(string abilityUsed) //this method gets called in the abilities script whenever an ability is used, and plays the corresponding sound
    {
        AudioSource qAbilitySource = GameObject.Find("qAbilitySoundObject").GetComponent<AudioSource>();
        AudioSource eAbilitySource = GameObject.Find("eAbilitySoundObject").GetComponent<AudioSource>();
        switch (abilityUsed)
        {
            case "void":
                eAbilitySource.clip = voidClip;
                eAbilitySource.Play();
                break;
            case "pulse":
                qAbilitySource.clip = pulse;
                qAbilitySource.Play();
                break;
            case "boost":
                eAbilitySource.clip = boost;
                eAbilitySource.Play();
                break;
            case "beam":
                qAbilitySource.clip = beam;
                qAbilitySource.Play();
                break;
            case "earthen":
                eAbilitySource.clip = earthen;
                eAbilitySource.Play();
                break;
            case "strike":
                qAbilitySource.clip = strike;
                qAbilitySource.Play();
                break;
            case "wall":
                eAbilitySource.clip = wall;
                eAbilitySource.Play();
                break;
            case "sun":
                qAbilitySource.clip = sun;
                qAbilitySource.Play();
                break;
            case "shield":
                eAbilitySource.clip = shield;
                eAbilitySource.Play();
                break;
            case "lance":
                qAbilitySource.clip = lance;
                qAbilitySource.Play();
                break;
        }
    }

    public void VolumeControl(bool isTalking) //this method lowers the volume of the music playing while coat is talking to an npc, scaled based on the current user volume.
    {
        AudioSource qAbilitySource = GameObject.Find("qAbilitySoundObject").GetComponent<AudioSource>();
        AudioSource eAbilitySource = GameObject.Find("eAbilitySoundObject").GetComponent<AudioSource>();
        AudioSource paperSource = GameObject.Find("PaperRustle").GetComponent<AudioSource>();
        AudioSource musicSource = GameObject.Find("MusicPlaying").GetComponent<AudioSource>();

        //music volume control
        if (isTalking)
        {                                                                                            //if the player is talking to an npc, lowers the volume by
            musicSource.volume = GameObject.Find("MusicSlider").GetComponent<Slider>().value * 0.6f; //60 percent    
        }
        if (!isTalking)
        {
            musicSource.volume = GameObject.Find("MusicSlider").GetComponent<Slider>().value;
        }

        //sound effect volume control
        paperSource.volume = GameObject.Find("EffectSlider").GetComponent<Slider>().value;
        qAbilitySource.volume = GameObject.Find("EffectSlider").GetComponent<Slider>().value;
        eAbilitySource.volume = GameObject.Find("EffectSlider").GetComponent<Slider>().value;
    }
}
