using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CirclePlacement : MonoBehaviour
{
    [Header("References")]
    public GameObject[] pillars;
    public GameObject[] braziers;
    public GameObject target;

    [Header("Settings")]
    public float pillarRadius = 5.0f;
    public float brazierRadius = 3.5f;

    void OnValidate()
    {
        ArrangePillars(pillars, pillarRadius);
        ArrangeBraziers(braziers, brazierRadius);
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
            objects[i].transform.position = target.transform.position + newPosition;
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
            objects[i].transform.position = target.transform.position + newPosition;
        }
    }
}
