using UnityEngine;

public class EnemyChapter3 : MonoBehaviour
{
    private Transform PlayerTransform;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerTransform = player.transform;
        }
    }
    void Update()
    {

        transform.LookAt(PlayerTransform);
    }
}
