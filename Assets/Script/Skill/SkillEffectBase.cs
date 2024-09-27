using UnityEngine;

public abstract class SkillEffectBase : ScriptableObject, ISkillEffect
{
    public abstract void Apply(GameObject user, GameObject target = null);
}