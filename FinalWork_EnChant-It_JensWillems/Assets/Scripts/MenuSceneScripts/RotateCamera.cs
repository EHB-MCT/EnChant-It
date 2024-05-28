using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public Transform target; 
    public float rotationSpeed = 10f;
    public float distance = 10f;

    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(0, 0, -distance);
    }

    void Update()
    {
        transform.position = target.position + offset;
        transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);

        offset = transform.position - target.position;

        transform.LookAt(target);
    }
}
