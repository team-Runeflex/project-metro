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
    [SerializeField]
    private SkillType skillType;
    public SkillType SkillType { get => skillType; set => skillType = value; }
    
    public abstract void SkillAction(GameObject user, GameObject target = null);
}
