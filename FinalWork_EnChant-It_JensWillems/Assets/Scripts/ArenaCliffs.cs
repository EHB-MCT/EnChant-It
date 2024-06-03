using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ArenaCliffs : MonoBehaviour
{
    public GameObject cliffPrefab; // Reference to the cliff prefab
    public int numberOfCliffs = 10; // Number of cliffs to create
    public float circleRadius = 10f; // Radius of the circle

#if UNITY_EDITOR
    void OnValidate()
    {
        // Ensure numberOfCliffs is at least 1
        numberOfCliffs = Mathf.Max(1, numberOfCliffs);

        // Check if we're in edit mode
        if (!Application.isPlaying)
        {
            // Clear existing cliffs only in edit mode
            ClearExistingCliffs();
        }

        // Recreate the cliff circle when any of the parameters change in the editor
        CreateCliffCircle();
    }
#endif

    void Start()
    {
        // Create the cliff circle when the game starts
        CreateCliffCircle();
    }

    void ClearExistingCliffs()
    {
        // Clear any existing cliffs only in edit mode
        foreach (Transform child in transform)
        {
#if UNITY_EDITOR
            DestroyImmediate(child.gameObject);
#endif
        }
    }

    void CreateCliffCircle()
    {
        // Create new cliffs in a circle formation
        for (int i = 0; i < numberOfCliffs; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfCliffs; // Calculate angle for each cliff
            Vector3 pos = new Vector3(Mathf.Cos(angle) * circleRadius, 0f, Mathf.Sin(angle) * circleRadius); // Calculate position on circle

            // Instantiate the cliff prefab at the calculated position
            GameObject cliff = Instantiate(cliffPrefab, pos, Quaternion.identity);

            // Calculate the direction to the center of the circle
            Vector3 directionToCenter = Vector3.zero - pos;

            // Get the rotation that looks down the direction vector with the local up vector as the Y-axis
            Quaternion lookRotation = Quaternion.LookRotation(directionToCenter, Vector3.up);

            // Apply the rotation to align the cliff prefab with the direction to the center
            cliff.transform.rotation = lookRotation;

            cliff.transform.SetParent(transform); // Set the parent of the instantiated cliff to this GameObject
        }
    }
}
