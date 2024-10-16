using UnityEngine;

[CreateAssetMenu(menuName = "SkillEffects/ExplosionEffect")]
public class ExplosionEffect : SkillEffectBase
{
    public float Damage;
    public float ExplosionRadius;

    public override void Apply(GameObject user, GameObject target = null)
    {
        if (target == null) return;

        // 타겟이 적인지 확인
        if (target.CompareTag("Enemy"))
        {
            EnemyState enemy = target.GetComponent<EnemyState>();
            if (enemy != null)
            {
                enemy.TakeDamage(Damage);
            }

            // 폭발 반경 내 모든 적 감지 (2D 예제)
            Collider2D[] colliders = Physics2D.OverlapCircleAll(target.transform.position, ExplosionRadius);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    EnemyState nearbyEnemy = collider.GetComponent<EnemyState>();
                    if (nearbyEnemy != null)
                    {
                        nearbyEnemy.TakeDamage(Damage);
                    }
                }
            }
        }
    }
}