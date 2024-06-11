using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float HealthLength = 100;
    public GameObject Bar;
    public bool RecoverHealth;

    void Update()
    {
        Bar.GetComponent<RectTransform>().sizeDelta = new Vector2(HealthLength, 110);

        if(RecoverHealth == true )
        {
            if(HealthLength >= 100)
            {
                RecoverHealth = false;
            } 
        }
    }

    IEnumerator SimulateHealth()
    {
        yield return new WaitForSeconds(5);
        HealthLength -= 30;
        yield return new WaitForSeconds(2);
        HealthLength -= 20; 
        yield return new WaitForSeconds(3);
        HealthLength -= 40; 
        yield return new WaitForSeconds(3);
        RecoverHealth = true;
    }
}
