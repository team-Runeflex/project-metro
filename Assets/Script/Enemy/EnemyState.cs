using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyState : MonoBehaviour
{
    [Header("Enemy Data")]
    public EnemyData ESD; // 적 데이터 (체력, 이동 속도 등)
    public float health;
    public GameObject player;

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    [Header("Movement")]
    public int nextMove = 1; // 초기 이동 방향 (-1: 왼쪽, 1: 오른쪽)

    [Header("Raycast Settings")]
    [SerializeField]
    private float raycastDistance = 1.5f; // Raycast 거리
    [SerializeField]
    private Vector2 raycastOffset = new Vector2(0.5f, -0.1f); // Raycast 시작 위치 오프셋
    [SerializeField]
    private LayerMask platformLayerMask; // Raycast가 감지할 레이어

    private Coroutine thinkCoroutine;
    private bool isTurning = false;
    
    
    
    public bool meetPlayer = false;

    private void Awake()
    {
        // 초기화
        health = ESD.Health;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // 초기 이동 방향 설정
        SetNextMove(RandomDirection());

        // 5초마다 Think 함수 호출
        thinkCoroutine = StartCoroutine(ThinkRoutine(5f));
    }

    private void FixedUpdate()
    {
        if (!meetPlayer)
        {
            // 이동 처리
            rigid.linearVelocity = new Vector2(nextMove * ESD.MoveSpeed, rigid.linearVelocity.y);

            // Raycast 시작 위치 계산 (오프셋 적용)
            Vector2 frontVec = new Vector2(rigid.position.x + nextMove * raycastOffset.x,
                rigid.position.y + raycastOffset.y);

            // Raycast 시각화 (디버깅 용도)
            Debug.DrawRay(frontVec, Vector2.down * raycastDistance, Color.red);

            // Raycast 실행
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector2.down, raycastDistance, platformLayerMask);

            // 디버깅: Raycast가 감지했는지 로그 출력
            if (rayHit.collider != null)
            {
                //Debug.Log("Raycast Hit: " + rayHit.collider.name);
            }
            else
            {
                Debug.Log("Raycast Missed");
            }

            // 플랫폼이 없으면 즉시 방향 전환
            if (rayHit.collider == null && !isTurning)
            {
                ChangeDirectionImmediate();
            }
        }
        else
        {
            Vector3 distance = player.transform.position - transform.position;
            Vector3 dir = distance.normalized;
            float fDist = distance.magnitude;
            if (fDist > ESD.MoveSpeed * Time.deltaTime)
            {
                transform.position += dir * (ESD.MoveSpeed * Time.deltaTime);
            }
        }
    }

    private void Update()
    {
        if (!meetPlayer)
        {
            if (nextMove == 1)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (nextMove == -1)
                transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            if (player.transform.position.x > transform.position.x)
                transform.localScale = new Vector3(1, 1, 1);
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    // 체력 감소 함수
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // 사망 처리 함수
    private void Die()
    {
        //anim.SetTrigger("Die");
        // 사망 후 일정 시간 후 오브젝트 제거
        Destroy(gameObject, 1f);
    }

    // Think 코루틴: 주기적으로 방향을 변경
    private IEnumerator ThinkRoutine(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Think();
        }
    }

    // 행동 결정 함수: 새로운 방향 설정
    private void Think()
    {
        Debug.Log("Think called");
        SetNextMove(RandomDirection());
    }

    // 랜덤 방향 선택 (-1 또는 1 또는 0)
    private int RandomDirection()
    {
        return Random.Range(-1, 2);
    }

    // 다음 이동 방향 설정 및 스프라이트 방향 변경
    private void SetNextMove(int direction)
    {
        nextMove = direction;
        spriteRenderer.flipX = nextMove == 1;
        //anim.SetInteger("WalkSpeed", nextMove);
        Debug.Log("Next Move set to: " + nextMove);
    }

    // 즉시 방향 전환 함수
    private void ChangeDirectionImmediate()
    {
        isTurning = true;
        Debug.Log("No ground detected. Changing direction immediately.");
        SetNextMove(-nextMove);

        // 방향 전환 후 잠시 대기
        CancelInvoke(); // 현재 Think 코루틴의 Invoke 취소
        Invoke("ResetTurning", 1f); // 1초 후에 Turning 상태 해제
    }

    // 방향 전환 상태 리셋 함수
    private void ResetTurning()
    {
        isTurning = false;
        // Think 함수는 이미 주기적으로 호출되므로 추가 호출 필요 없음
    }

    // 사망 시 코루틴 정지
    private void OnDestroy()
    {
        if (thinkCoroutine != null)
        {
            StopCoroutine(thinkCoroutine);
        }
    }
}
