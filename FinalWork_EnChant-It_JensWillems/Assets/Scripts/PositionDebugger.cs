using UnityEngine;

public class PositionDebugger : MonoBehaviour
{
    private void Update()
    {
        Debug.Log("Position: " + transform.position);
    }
}
