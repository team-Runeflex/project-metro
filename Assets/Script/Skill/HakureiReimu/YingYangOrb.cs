using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Skill/Ying Yang Orb")]
public class YinYangOrb : ProjectileSkill
{
    public Sprite image;



    public override void SkillAction(GameObject caster, GameObject target)
    {
        // 발사체 생성
        GameObject orb = Instantiate(projectilePrefab, caster.transform.position, Quaternion.identity);
        Rigidbody2D rb = orb.GetComponent<Rigidbody2D>();


    }
}