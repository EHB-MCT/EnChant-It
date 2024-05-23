using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceCanvasController : MonoBehaviour
{
    public Transform target; // Reference to the target (usually the camera)
    public bool moveWithCamera = true; // Boolean variable to control movement
    public float distanceFromTarget = 0.4f; // Distance from the target

    void Update()
    {
        if (moveWithCamera)
        {
            distanceFromTarget = 0.8f;
            // If moveWithCamera is true, make the canvas follow the target (camera)
            transform.position = target.position + target.forward * distanceFromTarget;
            transform.rotation = Quaternion.LookRotation(target.forward, target.up);
        }
        // If moveWithCamera is false, the canvas remains static
    }
}
