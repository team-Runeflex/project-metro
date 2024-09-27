using UnityEngine;

[CreateAssetMenu(menuName = "SkillEffects/DamageEffect")]
public class DamageEffect : SkillEffectBase
{
    public float damage;

    public override void Apply(GameObject user, GameObject target)
    { 
        EnemyState targetHealth = target.GetComponent<EnemyState>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }
    }
}