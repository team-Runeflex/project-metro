using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/ProjectileSkill")]
public class ProjectileSkill : Skill
{
    public GameObject projectilePrefab; // 발사할 프리팹
    
    public override void SkillAction(GameObject user, GameObject target)
    {
        // 프리팹이 할당되어 있는지 확인
        if (projectilePrefab != null)
        {
            // 발사 지점을 플레이어 앞쪽으로 설정
            Vector3 spawnPosition = user.transform.position + user.transform.right;

            // 타겟이 없으면 플레이어의 정면 방향으로 발사
            Vector3 direction = target != null 
                ? (target.transform.position - user.transform.position).normalized 
                : user.transform.right;

            // 프로젝트타일 생성
            GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.LookRotation(direction));
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                rb.linearVelocity = direction * Speed; // 설정한 방향으로 발사체를 발사
            }

            // SkillProjectile 컴포넌트에 스킬 할당
            SkillProjectile skillProjectile = projectile.GetComponent<SkillProjectile>();
            if (skillProjectile != null)
            {
                skillProjectile.skill = this; // 현재 스킬을 할당하여 발사체에 효과 연결
            }
        }
        else
        {
            Debug.LogWarning("ProjectilePrefab이 설정되지 않았습니다.");
        }
    }
}