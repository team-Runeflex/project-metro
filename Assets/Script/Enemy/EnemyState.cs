using System;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public EnemyScriptData ESD;
    public float health;
    
    
    private void Awake()
    {
        health = ESD.Health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
