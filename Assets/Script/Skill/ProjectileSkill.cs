using UnityEngine;

[CreateAssetMenu(menuName = "Skills/ProjectileSkill")]
public class ProjectileSkill : Skill
{
    public GameObject ProjectilePrefab; // 발사할 프리팹

    [SerializeField]
    private float deleteTime;
    public float DeleteTime { get => deleteTime; set => deleteTime = value; }

    public override void SkillAction(GameObject user, GameObject target)
    {
        if (ProjectilePrefab != null)
        {
            // 발사 위치 설정 (사용자 앞쪽)
            Vector3 spawnPosition = user.transform.position + user.transform.right;

            // 타겟 방향 설정
            Vector3 direction = target != null 
                ? (target.transform.position - user.transform.position).normalized 
                : user.transform.right;

            // 프로젝트타일 생성
            GameObject projectile = Instantiate(ProjectilePrefab, spawnPosition, Quaternion.identity);
            SkillProjectile skillProjectile = projectile.GetComponent<SkillProjectile>();
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = new Vector2(direction.x, direction.y) * Speed; // 설정한 방향으로 발사체 발사
            }

            // SkillProjectile 컴포넌트 초기화
            if (skillProjectile != null)
            {
                skillProjectile.skill = this; // 현재 스킬 할당
                // 필요한 초기화가 있으면 추가
            }
        }
        else
        {
            Debug.LogWarning("ProjectilePrefab이 설정되지 않았습니다.");
        }
    }
}
