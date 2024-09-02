using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableData/EnemyData", fileName = "Enemy")]
public class EnemyScriptData : ScriptableObject
{
    [SerializeField] 
    private float health;
    public float Health{ get => health; set => health = value; }
    
    
}
