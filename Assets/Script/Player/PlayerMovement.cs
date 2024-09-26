using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;  // 이동 속도 설정
    public float xAxis;
    private Rigidbody2D rb;
    public float jumpForce;
    float timeCheck = 0;
    public float JumpCooldown = 0;
    private bool isGrounded;
    [HideInInspector]
    public String lastVector = "Right";
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void Update()
    {
        GetInputs();
        Move();
        timeCheck += Time.deltaTime;
        if (timeCheck > JumpCooldown || isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Jump();
                
            }
        } 
    }

    private void FixedUpdate()
    {
        UpdateLastVector();
    }
    

    void GetInputs()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            xAxis = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            xAxis = 1;
        }
        else
        {
            xAxis = 0;
        }
    }

    void Move()
    {
            rb.linearVelocity = new Vector2(speed * xAxis, rb.linearVelocity.y);
    }

    // 플레이어의 현재 방향을 확인하여 lastVector 값을 업데이트하는 메서드
    public void UpdateLastVector()
    {
        if (xAxis >= 0.1f)
        {
            lastVector = "right";
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (xAxis <= -0.1f)
        {
            lastVector = "left";
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    
    void Jump()
    {
        Debug.Log(123);
        timeCheck = 0;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
    }
    
}