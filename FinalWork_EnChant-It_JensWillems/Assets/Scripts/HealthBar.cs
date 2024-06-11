using System.Collections;
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
}
