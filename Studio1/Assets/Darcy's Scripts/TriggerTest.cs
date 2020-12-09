using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PaperRustle();
    }
}
