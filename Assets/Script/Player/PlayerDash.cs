using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 20f; // 대쉬 속도
    public float dashDuration = 0.2f; // 대쉬 지속 시간
    public float dashCooldown = 1f; // 대쉬 쿨타임

    private bool isDashing = false;
    private float dashTimeLeft;
    private float lastDashTime;

    private Rigidbody2D rb;
    private Vector2 dashDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isDashing && Time.time >= lastDashTime + dashCooldown)
        {
            StartDash();
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            Dash();
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        lastDashTime = Time.time;

        // 대쉬 방향 설정 (현재 캐릭터가 바라보고 있는 방향으로 대쉬)
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput < 0)
        {
            dashDirection = Vector2.left;
        }
        else if (horizontalInput > 0)
        {
            dashDirection = Vector2.right;
        }
        else
        {
            // 만약 방향키 입력이 없을 경우 캐릭터의 현재 바라보는 방향으로 설정
            dashDirection = transform.localScale.x < 0 ? Vector2.left : Vector2.right;
        }
    }

    void Dash()
    {
        if (dashTimeLeft > 0)
        {
            rb.linearVelocity = dashDirection * dashSpeed;
            dashTimeLeft -= Time.fixedDeltaTime;
        }
        else
        {
            isDashing = false;
            rb.linearVelocity = Vector2.zero; // 대쉬 후 멈춤
        }
    }
}