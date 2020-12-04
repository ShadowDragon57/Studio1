using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip test;
    public void PlaySound()
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(test);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            PlaySound();
        }
    }
}
