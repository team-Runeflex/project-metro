using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Skills/CompositeSkill")]
public class CompositeSkill : Skill
{
    [SerializeReference]
    public List<ISkillEffect> effects;
    
    public override void SkillAction(GameObject user, GameObject target = null)
    {
        foreach (var effect in effects)
        {
            effect.Apply(user, target);
        }
    }
}