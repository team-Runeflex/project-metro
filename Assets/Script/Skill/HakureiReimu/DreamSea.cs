using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Skill/Dream Sea")]
public class DreamSea : Skill
{

    public override void SkillAction(GameObject user, GameObject target = null)
    {
        Debug.Log("Dream Sea");
    }
}