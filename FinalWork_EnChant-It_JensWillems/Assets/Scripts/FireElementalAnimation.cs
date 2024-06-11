using UnityEngine;

public class FireElementalAnimation : MonoBehaviour
{
    [Header("Settings")]
    public float MinBobSpeed = 0.5f;
    public float MmaxBobSpeed = 1.5f;
    public float MinBobHeight = 0.1f;
    public float MaxBobHeight = 0.5f;
    public float JumpHeight = 3f;
    public float JjumpDuration = 1.5f;

    [Header("References")]
    public Transform Player;

    private float _originalY;
    private float _speed;
    private float _heigth;
    private bool _isJumping = false;
    private float _jumpTimer = 0f;

    private void Start()
    {
        _originalY = transform.position.y;
        _speed = Random.Range(MinBobSpeed, MmaxBobSpeed);
        _heigth = Random.Range(MinBobHeight, MaxBobHeight);
    }

    private void Update()
    {
        if(Player != null)
        {
            transform.LookAt(Player);
        }
        if (!_isJumping)
        {
            float yOffset = Mathf.Sin(Time.time * _speed) * _heigth;
            transform.position = new Vector3(transform.position.x, _originalY + yOffset, transform.position.z);
        }
        else
        {
            _jumpTimer += Time.deltaTime;
            float normalizedTime = _jumpTimer / JjumpDuration;
            float jumpCurveValue = Mathf.Sin(normalizedTime * Mathf.PI);
            float yOffset = jumpCurveValue * JumpHeight;
            transform.position = new Vector3(transform.position.x, _originalY + yOffset, transform.position.z);
            if (normalizedTime >= 1f)
            {
                _isJumping = false;
                _jumpTimer = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Jump();
        }
    }

    private void Jump()
    {
        _isJumping = true;
    }
}
