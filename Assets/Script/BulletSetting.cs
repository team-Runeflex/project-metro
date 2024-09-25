using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSetting : MonoBehaviour
{
    public PlayerMovement playerMovement; // 플레이어의 방향 정보를 가져옴
    private Rigidbody2D rb;
    private Coroutine destroyCoroutine;
    public float speed = 10f; // 총알 속도
    public float destroyTime = 3f; // 총알이 사라질 시간
    public IObjectPool<GameObject> Pool { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        // 플레이어의 방향을 업데이트
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerMovement.UpdateLastVector();

        // 방향에 따라 속도와 회전 설정
        if (playerMovement.lastVector == "right")
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            rb.linearVelocity = Vector2.right * speed;
        }
        else if (playerMovement.lastVector == "left")
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            rb.linearVelocity = Vector2.left * speed;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            rb.linearVelocity = Vector2.right * speed;
        }

        if (destroyCoroutine is not null) StopCoroutine(destroyCoroutine);
        destroyCoroutine = StartCoroutine(ReturnToPoolFlow(destroyTime));
    }

    private void OnDisable()
    {
        rb.linearVelocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyState>().TakeDamage(10);
            if (destroyCoroutine is not null) StopCoroutine(destroyCoroutine);
            destroyCoroutine = StartCoroutine(ReturnToPoolFlow(0));
        }
    }

    private IEnumerator ReturnToPoolFlow(float time)
    {
        yield return new WaitForSeconds(time);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        Pool.Release(gameObject);
    }
}