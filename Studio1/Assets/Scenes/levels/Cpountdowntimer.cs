using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cpountdowntimer : MonoBehaviour
{
    float Currenttime = 0f;
    public float startingtime = 60f;

    [SerializeField] Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        Currenttime = startingtime;
    }

    // Update is called once per frame
    void Update()
    {
        Currenttime -= 1 * Time.deltaTime;
        countdownText.text = Currenttime.ToString ("0");

        if(Currenttime <= 0)
        {
            Currenttime = startingtime;
            SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
        }
      
    }
}
