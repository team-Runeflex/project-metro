using System;
using UnityEngine;


public class SearchPlayer : MonoBehaviour
{
    public float playerSearchSize;
    private CircleCollider2D collider2D;
    private EnemyState enemyState;
    
    public float playerSearchTimer = 1.5f;
    private float playerTimer;
    
    private void Awake()
    {
        // Collider2D 컴포넌트 가져오기
        collider2D = GetComponent<CircleCollider2D>();
        if (collider2D == null)
        {
            Debug.LogError("Collider2D 컴포넌트가 없습니다. 추가해주세요.");
            return;
        }

        // Collider2D가 Trigger로 설정되어 있는지 확인
        if (!collider2D.isTrigger)
        {
            Debug.LogWarning("Collider2D가 Trigger로 설정되어 있지 않습니다. Trigger로 설정합니다.");
            collider2D.isTrigger = true;
        }

        // 오프셋 설정
        collider2D.radius = playerSearchSize;

        // EnemyState 컴포넌트 가져오기
        enemyState = GetComponentInParent<EnemyState>();
        if (enemyState == null)
        {
            Debug.LogError("EnemyState 컴포넌트를 찾을 수 없습니다.");
        }

        // 타이머 초기화
        playerTimer = playerSearchTimer;
    }

    private void Update()
    {

        // 타이머가 0보다 작아지지 않도록 클램핑
        playerTimer = Mathf.Max(playerTimer, 0f);
        if(enemyState.meetPlayer)
            playerTimer -= Time.deltaTime;
        if (playerTimer <= 0)
        {
            enemyState.meetPlayer = false;
            Debug.Log("플레이어 사라짐!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemyState.meetPlayer = true;
            Debug.Log("플레이어 포착!");
        } 
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerTimer = playerSearchTimer;
    }
    
    private void OnDrawGizmos()
    {
        if (collider2D != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube((Vector2)transform.position + collider2D.offset, collider2D.bounds.size);
        }
    }
}
