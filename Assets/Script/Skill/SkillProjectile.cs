using System.Collections;
using UnityEngine;

public class SkillProjectile : MonoBehaviour
{
    public Skill skill; // ScriptableObject로 정의한 Skill
    private Rigidbody2D rb;
    private GameObject me;

    private void Awake()
    {
        me = gameObject;
    }

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
            rb.velocity = new Vector2(skill.Speed * direction, rb.velocity.y);
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
                // ExplosionEffect가 있는지 확인
                bool hasExplosionEffect = false;
                foreach (var effect in skill.SkillEffects)
                {
                    if (effect is ExplosionEffect)
                    {
                        hasExplosionEffect = true;
                        break;
                    }
                }

                if (hasExplosionEffect)
                {
                    // ExplosionEffect가 있을 경우 코루틴 시작
                    StartCoroutine(HandleExplosion(other.gameObject));
                }
                else
                {
                    // ExplosionEffect가 없을 경우 기존 이펙트 즉시 적용
                    ApplyOtherEffects(other.gameObject);
                    targetHealth.TakeDamage(skill.Damage);
                    Destroy(gameObject); // 풀링 시스템을 사용하는 경우 이 부분을 풀링 코드로 변경
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator HandleExplosion(GameObject target)
    {
        // 확장 애니메이션 지속 시간
        float scalingDuration = 0.5f;
        Vector3 initialScale = Vector3.zero;
        float explosionRadius = 0f;

        // Skill의 SkillEffects 중 ExplosionEffect 찾기
        ExplosionEffect explosionEffect = null;
        foreach (var effect in skill.SkillEffects)
        {
            if (effect is ExplosionEffect)
            {
                explosionEffect = effect as ExplosionEffect;
                explosionRadius = explosionEffect.ExplosionRadius; // ExplosionRadius 프로퍼티 필요
                break;
            }
        }

        if (explosionEffect == null)
        {
            Debug.LogWarning("SkillEffects에 ExplosionEffect가 없습니다.");
            Destroy(gameObject);
            yield break;
        }

        // 초기 스케일 설정
        transform.localScale = initialScale;

        float elapsed = 0f;
        while (elapsed < scalingDuration)
        {
            // 스케일을 0에서 explosionRadius로 선형 보간
            float scale = Mathf.Lerp(0f, explosionRadius, elapsed / scalingDuration);
            transform.localScale = Vector3.one * scale;
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 최종 스케일 설정
        transform.localScale = Vector3.one * explosionRadius;

        // 폭발 효과 적용
        explosionEffect.Apply(me, target);

        // 추가적인 시각적 또는 음향 효과를 여기서 처리할 수 있습니다.

        // 폭발 후 파괴
        Destroy(gameObject); // 풀링 시스템을 사용하는 경우 이 부분을 풀링 코드로 변경
    }

    private void ApplyOtherEffects(GameObject target)
    {
        foreach (var effect in skill.SkillEffects)
        {
            if (!(effect is ExplosionEffect))
            {
                effect.Apply(me, target);
                Debug.Log("Effect 발동!");
            }
        }
    }
}
