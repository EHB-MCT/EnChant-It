using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    void Update()
    {
        Debug.Log("Position: " + transform.position);
    }
}
