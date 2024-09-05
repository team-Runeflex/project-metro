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
    
    private void Update()
    {
        if (curTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0); //히트박스 생성
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.CompareTag("Enemy"))
                    {
                        collider.GetComponent<EnemyState>().TakeDamage(defaultDamage);
                    }
                }
                curTime = coolTime;
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                
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
