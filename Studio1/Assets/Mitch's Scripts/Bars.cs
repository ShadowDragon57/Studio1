using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bars : MonoBehaviour
{
    public GameObject health;
    Transform bar;
    public float currentHealth, maxHealth, healthPercent;

    // Start is called before the first frame update
    void Start()
    {
        bar = health.transform;
    }

    // Update is called once per frame
    void Update()
    {
        bar.localScale = new Vector3(SetHealth(currentHealth, maxHealth), 1f);
    }

    float SetHealth(float currentHealth, float maxHealth)
    {
        healthPercent = currentHealth / maxHealth;
        return healthPercent;
    }

    public void IdeologyHealthChange(float max)
    {
        maxHealth = max;
        currentHealth = max * healthPercent;
    }
}
