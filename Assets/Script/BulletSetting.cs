using System;
using UnityEngine;

public class BulletSetting : MonoBehaviour
{
    public PlayerMovement playerMovement; // PlayerMovement 스크립트에서 플레이어의 정보를 가져옴
    private Rigidbody2D rb;
    private string lastVector;
    public float destroyTime = 3f; // 발사체가 사라질 시간 (초 단위)

    private void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

        // 플레이어의 방향을 미리 설정해둠
        playerMovement.UpdateLastVector();
    }



    private void FixedUpdate()
    {
        // 방향 업데이트
        playerMovement.UpdateLastVector();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log(other.tag);
            other.GetComponent<EnemyState>().TakeDamage(10);
        }
    }

    // 발사체가 활성화될 때 호출되는 메서드
    private void OnEnable()
    {
        playerMovement.UpdateLastVector();
        // 발사체 활성화 시에도 방향 업데이트

        if (playerMovement.lastVector == "right")
        {
            // 오른쪽 방향
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            rb.linearVelocity = new Vector2(10, 0); // 오른쪽으로 발사
        }
        else if (playerMovement.lastVector == "left")
        {
            // 왼쪽 방향
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            rb.linearVelocity = new Vector2(-10, 0); // 왼쪽으로 발사
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            rb.linearVelocity = new Vector2(10, 0); // 오른쪽으로 발사
            
        }

        // destroyTime 후에 발사체 삭제
        Invoke("DestroyBullet", destroyTime);
    }

    // 발사체가 비활성화될 때 속도를 0으로 설정
    private void OnDisable()
    {
        rb.linearVelocity = Vector2.zero;
    }

    // 발사체 삭제 메서드
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}