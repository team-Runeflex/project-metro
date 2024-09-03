using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private float curTime;
    public float coolTime = 0.1f;
    public Transform pos;
    public Vector2 boxSize;
    private void Update()
    {
        if (curTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0); //히트박스 생성
                foreach (Collider2D collider in collider2Ds)
                {
                    Debug.Log(collider.tag);
                }
                curTime = coolTime;
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

    public void Attack()
    {
        
    }
}
