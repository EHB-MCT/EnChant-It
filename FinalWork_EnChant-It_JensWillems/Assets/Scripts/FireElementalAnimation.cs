using UnityEngine;

public class FireElementalAnimation : MonoBehaviour
{
    public float minBobSpeed = 0.5f;
    public float maxBobSpeed = 1.5f;
    public float minBobHeight = 0.1f;
    public float maxBobHeight = 0.5f;
    public float jumpHeight = 3f;
    public float jumpDuration = 1.5f;

    public Transform Player;

    private float originalY;
    private float bobSpeed;
    private float bobHeight;
    private bool isJumping = false;
    private float jumpTimer = 0f;

    private void Start()
    {
        originalY = transform.position.y;
        bobSpeed = Random.Range(minBobSpeed, maxBobSpeed);
        bobHeight = Random.Range(minBobHeight, maxBobHeight);
    }

    private void Update()
    {
        if(Player != null)
        {
            transform.LookAt(Player);
        }
        if (!isJumping)
        {
            float yOffset = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
            transform.position = new Vector3(transform.position.x, originalY + yOffset, transform.position.z);
        }
        else
        {
            jumpTimer += Time.deltaTime;
            float normalizedTime = jumpTimer / jumpDuration;
            float jumpCurveValue = Mathf.Sin(normalizedTime * Mathf.PI);
            float yOffset = jumpCurveValue * jumpHeight;
            transform.position = new Vector3(transform.position.x, originalY + yOffset, transform.position.z);
            if (normalizedTime >= 1f)
            {
                isJumping = false;
                jumpTimer = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Jump();
        }
    }

    private void Jump()
    {
        isJumping = true;
    }
}
