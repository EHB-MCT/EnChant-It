using UnityEngine;

public class Waterfall : MonoBehaviour
{
    private float _speedWaterfall = -50;

    void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * _speedWaterfall);
    }
}
