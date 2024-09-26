using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance {get ; private set;}
    
    private Dictionary<string, Skill> skillDictionary = new Dictionary<string, Skill>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAllSkills();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadAllSkills()
    {
        Skill[] skills = Resources.LoadAll<Skill>("Skills");
        foreach (var skill in skills)
        {
            if (!skillDictionary.ContainsKey(skill.SkillName))
            {
                skillDictionary.Add(skill.SkillName, skill);
            }
            else
            {
                Debug.LogWarning($"중복된 이름 발견: {skill.SkillName}");
            }
        }
    }

    public Skill GetSkill(string skillName)
    {
        if (skillDictionary.TryGetValue(skillName, out Skill skill))
        {
            return skill;
        }
        Debug.Log($"스킬을 찾을 수 없습니다: {skillName}");
        return null;
    }
}
