using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableData/EnemyData", fileName = "Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] 
    private float health;
    public float Health{ get => health; set => health = value; }
    
    [SerializeField]
    private float searchPlayerRadius;

    public float SearchPlayerRadius{ get => searchPlayerRadius; set => searchPlayerRadius = value; }
    
    [SerializeField]
    private float damage;
    public float Damage { get => damage; set => damage = value; }

    [SerializeField]
    private bool isRanged;
    public bool IsRanged { get => isRanged; set => isRanged = value; }
    
    [SerializeField]
    private GameObject rangedEffect;
    public GameObject RangedEffect { get => rangedEffect; set => rangedEffect = value; }

    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }


}
