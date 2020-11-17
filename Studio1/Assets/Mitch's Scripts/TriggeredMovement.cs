using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggeredMovement : MonoBehaviour
{
    bool triggered;
    public GameObject flame, text;

    // Update is called once per frame
    void Start()
    {
        triggered = false;
    }

    void Update()
    {
        if (triggered == true && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Tutorial redux part 2");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Entered");
        if (collision.gameObject.name == "Player")
        {
            triggered = true;
            flame.SetActive(true);
            text.SetActive(true);

        }
    }
}
