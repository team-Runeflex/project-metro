

using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterSkillData
{
    public string name;
    public string[] skills;
}

[System.Serializable]
public class CharacterSkillDatabase
{
    public CharacterSkillData[] characters;
}

public class CharacterInitializer : MonoBehaviour
{
    public TextAsset characterSkillJSON;
    public List<PlayerCharacterData> players;

    private void Start()
    {
        LoadCharacterSkills();
    }

    private void LoadCharacterSkills()
    {
        CharacterSkillDatabase database = JsonUtility.FromJson<CharacterSkillDatabase>(characterSkillJSON.text);
        foreach (var characterData in database.characters)
        {
            PlayerCharacterData playerCharacter = players.Find(c => c.name == characterData.name);
            if (playerCharacter != null)
            {
                List<Skill> assignedSkills = new List<Skill>();
                foreach (var skillName in characterData.skills)
                {
                    Skill skill = SkillManager.Instance.GetSkill(skillName);
                    if (skill != null)
                    {
                        assignedSkills.Add(skill);
                    }
                }
                playerCharacter.Skills = assignedSkills.ToArray();
            }
            else
            {
                Debug.LogWarning("캐릭터를 찾을 수 없음");
            }
        }
    }
}