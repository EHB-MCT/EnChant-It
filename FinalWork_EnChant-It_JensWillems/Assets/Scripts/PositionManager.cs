using UnityEngine;
using System.Collections.Generic;

public class PositionManager : MonoBehaviour
{
    public List<Transform> positions = new List<Transform>();
    public GameObject PlayerGameObject;

    private void Start()
    {
        TeleportToPosition(0);
    }

    public void TeleportToPosition(int index)
    {
        if (index >= 0 && index < positions.Count)
        {
            Transform newPosition = positions[index];
            if (PlayerGameObject != null)
            {
                PlayerGameObject.transform.position = newPosition.position;
                PlayerGameObject.transform.rotation = newPosition.rotation;
            }
            else
            {
                Debug.LogError("Player GameObject is not assigned.");
            }
        }
        else
        {
            Debug.LogError("Index out of range.");
        }
    }
}
