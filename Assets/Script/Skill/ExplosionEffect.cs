using UnityEngine;

[CreateAssetMenu(menuName = "SkillEffects/ExplosionEffect")]
public class ExplosionEffect : SkillEffectBase
{
    public float damage;
    public float explosionRadius;
    
    public override void Apply(GameObject user, GameObject target)
    {
        // 폭발 로직
        if (target.CompareTag("Enemy"))
        {
            EnemyState enemy = target.GetComponent<EnemyState>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // 폭발 반경 내 모든 적 감지
            Collider2D[] colliders = Physics2D.OverlapCircleAll(target.transform.position, explosionRadius);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    EnemyState nearbyEnemy = collider.GetComponent<EnemyState>();
                    if (nearbyEnemy != null)
                    {
                        nearbyEnemy.TakeDamage(damage);
                    }
                }
            }
        }
    }
}