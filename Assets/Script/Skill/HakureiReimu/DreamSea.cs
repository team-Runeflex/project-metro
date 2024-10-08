using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Skill/Dream Sea")]
public class DreamSea : ProjectileSkill
{

    public override void SkillAction(GameObject user, GameObject target = null)
    {
        Debug.Log("Dream Sea");
    }
}