using System;
using UnityEngine;

public class SkillProjectile : MonoBehaviour
{
    public Skill skill; // ScriptableObject로 정의한 Skill
    private Rigidbody2D rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 플레이어의 방향을 감지하여 발사 방향 설정: 오른쪽이면 1, 왼쪽이면 -1
        float direction = transform.localScale.x > 0 ? 1 : -1;


        if (skill.SkillType == SkillType.Offensive)
        {
            // 발사체의 속도를 방향에 맞게 설정
            rb.linearVelocity = new Vector2(skill.Speed * direction, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 대상이 적일 때 효과 적용
        if (other.CompareTag("Enemy"))
        {
            
            EnemyState targetHealth = other.GetComponent<EnemyState>();
            if (targetHealth != null)
            {
                if (skill.SkillEffects != null)
                {
                    foreach (var s in skill.SkillEffects)
                    {
                        s.Apply(gameObject, other.gameObject);
                        Debug.Log("Effect 발동!");
                    }
                }
                targetHealth.TakeDamage(skill.Damage);
            }
            
            // 프로젝트타일을 파괴 또는 풀링 시스템으로 반환
            Destroy(gameObject); // 풀링을 사용 중이면 이 부분을 풀링 코드로 변경
        }
    }
}