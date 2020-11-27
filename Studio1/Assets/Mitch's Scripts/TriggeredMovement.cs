using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggeredMovement : MonoBehaviour
{
    bool triggered;
    public GameObject flame;

    // Update is called once per frame
    void Start()
    {
        triggered = false;
    }

    void Update()
    {
        if (triggered == true && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("tutorial part 2A");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Graphics")
        {
            
            triggered = true;
            flame.SetActive(true);

        }
    }
}
