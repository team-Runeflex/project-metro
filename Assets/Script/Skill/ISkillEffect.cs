using UnityEngine;

public interface ISkillEffect
{
    void Apply(GameObject user, GameObject target = null);
}
