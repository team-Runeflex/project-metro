using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Skill/Ying Yang Orb")]
public class YinYangOrb : Skill
{
    public float damage;


    public override void SkillAction(GameObject user, GameObject target = null)
    {
        Debug.Log("YinYangOrb");
    }
}