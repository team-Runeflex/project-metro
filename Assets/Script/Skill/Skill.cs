using UnityEngine;

public enum SkillType
{
    Offensive,
    Defensive,
    Heal,
    Passive,
    Movement
}


public abstract class Skill : ScriptableObject
{
    [SerializeField]
    private string skillName;
    public string SkillName { get => skillName; set => skillName = value; }
    [SerializeField]
    private Sprite icon;
    public Sprite Icon { get => icon; set => icon = value; }
    [SerializeField]
    private float cooldown;
    public float Cooldown { get => cooldown; set => cooldown = value; }

    private float cooldownTimer;
    private bool cooldownActive;
    
    [SerializeField]
    private SkillType skillType;
    public SkillType SkillType { get => skillType; set => skillType = value; }
    [SerializeField]
    private float speed;
    public float Speed { get => speed; set => speed = value; }

    [SerializeField]
    private float damage;
    public float Damage { get => damage; set => damage = value; }
    
    [SerializeField]
    private SkillEffectBase[] skillEffects;
    public SkillEffectBase[] SkillEffects { get => skillEffects; set => skillEffects = value; }

    [SerializeField]
    private float enemyDetection;
    public float EnemyDetection { get => enemyDetection; set => enemyDetection = value; }

    
    
    
    public abstract void SkillAction(GameObject user, GameObject target = null);
    private void HandleCooldown()
    {
        // 쿨다운 처리 로직
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0)
        {
            cooldownTimer = 0;
            cooldownActive = true;
        }
    }

    /*protected GameObject NearestEnemy()
    {
        
    }*/

}
