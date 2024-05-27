using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceCanvasController : MonoBehaviour
{
    public Transform target; 
    public bool moveWithCamera = true; 
    public float distanceFromTarget = 0.4f; 

    void Update()
    {
        if (moveWithCamera)
        {
            distanceFromTarget = 0.8f;

            transform.position = target.position + target.forward * distanceFromTarget;
            transform.rotation = Quaternion.LookRotation(target.forward, target.up);
        }
    }
}
