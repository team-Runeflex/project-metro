using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Skills/InstantSkill")]
public class InstantSkill : Skill
{
    public List<ISkillEffect> effects;

    public override void SkillAction(GameObject user, GameObject target)
    {
        foreach (var effect in effects)
        {
            effect.Apply(user, target);
        }
    }
}