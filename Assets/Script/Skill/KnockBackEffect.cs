using UnityEngine;

[CreateAssetMenu(menuName = "SkillEffects/KnockbackEffect")]
public class KnockbackEffect : SkillEffectBase
{
    public float force;

    public override void Apply(GameObject user, GameObject target)
    {
        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector3 direction = (target.transform.position - user.transform.position).normalized;
            rb.AddForce(direction * force, ForceMode2D.Impulse);
        }
    }
}
