using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceCanvasController : MonoBehaviour
{
    public Transform Target; 
    public bool MoveWithCamera = true; 
    public float DistanceFromTarget = 0.4f; 

    void Update()
    {
        if (MoveWithCamera)
        {
            DistanceFromTarget = 0.8f;

            transform.position = Target.position + Target.forward * DistanceFromTarget;
            transform.rotation = Quaternion.LookRotation(Target.forward, Target.up);
        }
    }
}
