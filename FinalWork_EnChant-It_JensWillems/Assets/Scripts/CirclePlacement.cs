using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CirclePlacement : MonoBehaviour
{
    [Header("References")]
    public GameObject[] Pillars;
    public GameObject[] Braziers;
    public GameObject Target;

    [Header("Settings")]
    public float PillarRadius = 5.0f;
    public float BrazierRadius = 3.5f;

    void OnValidate()
    {
        ArrangePillars(Pillars, PillarRadius);
        ArrangeBraziers(Braziers, BrazierRadius);
    }

    void ArrangePillars(GameObject[] objects, float radius)
    {
        int numberOfObjects = objects.Length;

        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            Vector3 newPosition = new Vector3(
                Mathf.Cos(angle) * radius,
                0.73f,  
                Mathf.Sin(angle) * radius
            );
            objects[i].transform.position = Target.transform.position + newPosition;
        }
    }

    void ArrangeBraziers(GameObject[] objects, float radius)
    {
        int numberOfObjects = objects.Length;

        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects + Mathf.PI / numberOfObjects;
            Vector3 newPosition = new Vector3(
                Mathf.Cos(angle) * radius,
                -3.5f,  
                Mathf.Sin(angle) * radius
            );
            objects[i].transform.position = Target.transform.position + newPosition;
        }
    }
}
