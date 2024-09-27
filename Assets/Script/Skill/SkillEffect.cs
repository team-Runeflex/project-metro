using UnityEngine;

public class SkillEffect
{
    public delegate void OnSkillUsed(GameObject user, GameObject target);
    public static event OnSkillUsed SkillUsed;

    public static void TriggerSkillUsed(GameObject user, GameObject target)
    {
        SkillUsed?.Invoke(user, target);
    }
}