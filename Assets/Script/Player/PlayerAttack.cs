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
    public Transform PrefabsParent;
    public int bulletNumber = 50;
    public Transform bulletSpawnPoint;

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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}