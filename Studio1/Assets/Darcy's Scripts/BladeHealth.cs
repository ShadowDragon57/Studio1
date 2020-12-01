using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeHealth : MonoBehaviour
{
    public float bladeHealth = 25f;
    [SerializeField]
    private GameObject blade;

    void Start()
    {
        blade = gameObject;
    }
    public void PulseDamage()
    {
        bladeHealth -= 10f;
        if (bladeHealth <= 0)
        {
            Destroy(blade);
            GameObject.Find("Canvas").GetComponent<ConvictionCalculator1>().EnemyKilledByCoat();
        }
    }
    public void BeamDamage()
    {
        bladeHealth -= 3f;
        if (bladeHealth <= 0)
        {
            Destroy(blade);
            GameObject.Find("Canvas").GetComponent<ConvictionCalculator1>().EnemyKilledByCoat();
        }
    }  
    public void StrikeDamage()
    {
        bladeHealth -= 15f;
        if (bladeHealth <= 0)
        {
            Destroy(blade);
            GameObject.Find("Canvas").GetComponent<ConvictionCalculator1>().EnemyKilledByCoat();
        }
    }
    public void SunDamage()
    {
        bladeHealth -= 10f;
        if (bladeHealth <= 0)
        {
            Destroy(blade);
            GameObject.Find("Canvas").GetComponent<ConvictionCalculator1>().EnemyKilledByCoat();
        }
    }
    public void LanceDamage()
    {
        bladeHealth -= 25f;
        if (bladeHealth <= 0)
        {
            Destroy(blade);
            GameObject.Find("Canvas").GetComponent<ConvictionCalculator1>().EnemyKilledByCoat();
        }
    }
}
