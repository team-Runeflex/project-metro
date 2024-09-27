using UnityEngine;

[CreateAssetMenu(menuName = "SkillEffects/ProjectileSpawnEffect")]
public class ProjectileSpawnEffect : ScriptableObject, ISkillEffect
{
    public GameObject projectilePrefab;
    public float projectileSpeed;

    public void Apply(GameObject user, GameObject target)
    {
        if (projectilePrefab != null)
        {
            Vector3 spawnPosition = user.transform.position + user.transform.forward;
            Vector3 direction = target != null 
                ? (target.transform.position - user.transform.position).normalized 
                : user.transform.forward;

            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.LookRotation(direction));
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                rb.linearVelocity = direction * projectileSpeed;
            }

            SkillProjectile skillProjectile = projectile.GetComponent<SkillProjectile>();
            if (skillProjectile != null)
            {
                skillProjectile.skill = user.GetComponent<PlayerAttack>().skills[0]; // 예시로 첫 번째 스킬 할당
            }
        }
    }
}