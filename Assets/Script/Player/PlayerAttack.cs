using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private float curTime;
    public float coolTime = 0.1f;
    public Transform pos;
    public Vector2 boxSize;
    public float defaultDamage;
    public PlayerCharacterData playerData;
    public int bulletNumber = 50;

    [Header("Skill Settings")]
    public Skill[] skills;

    private void Update()
    {
        if (curTime <= 0)
        {
            // 근접 공격
            if (Input.GetKeyDown(KeyCode.X))
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.CompareTag("Enemy"))
                    {
                        collider.GetComponent<EnemyState>().TakeDamage(defaultDamage);
                        curTime = coolTime;
                        break; // 하나의 적에게만 데미지를 주고 쿨타임 설정
                    }
                }
            }

            // 원거리 공격
            if (Input.GetKeyDown(KeyCode.V))
            {
                if (bulletNumber is > 0 or -1)
                {
                    var bulletGo = BulletPoolManager.instance.Pool.Get();
                    bulletGo.transform.position = transform.position;
                    bulletGo.SetActive(true); // 풀에서 가져온 후 활성화
                    if (bulletNumber > 0)
                        bulletNumber--;

                    curTime = coolTime;
                }
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            UseSkill(0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            UseSkill(1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

    /// <summary>
    /// 스킬 사용할 때 씁미다.
    /// </summary>
    /// <param name="index">스킬 인덱스</param>
    /// <param name="target">상대를 정하는 것 (가까이 있는적)은 사용</param>
    public void UseSkill(int index, GameObject target = null)
    {
        if (index < skills.Length && skills[index])
        {
            Skill skill = skills[index];
            skill.SkillAction(this.gameObject, target);
        }
        else
        {
            Debug.LogWarning("스킬 인덱스가 유효하지 않거나 스킬이 할당되지 않았습니다.");
        }
    }

}