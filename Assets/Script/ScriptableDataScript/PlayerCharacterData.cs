using System.Collections.Generic;
using NUnit.Framework;
using Script.ScriptableDataScript;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableData/PlayerData", order = -100)]
public class PlayerCharacterData : ScriptableObject
{

    //플레이어 캐릭터에 따른 스킬 같은걸 선택하기 위한 id
    [SerializeField] 
    private int id;
    public int Id{ get => id; set => id  = value;}
    
    //플레이어 캐릭터 데이터
    [SerializeField]
    private float health;
    public float Health { get => health; set => health = value;  }
    [SerializeField] 
    private float might; //힘, 공격은 기본 공격력 * 힘 * 추가 피해량, 화면에는 %로 표기
    public float Might {get => might; set => health = value;}

    [SerializeField] //기본 근접 공격
    private AttackData defaultAttackData;
    public AttackData DefaultAttackData { get => defaultAttackData; set
        {
            if (defaultAttackData.IsRanged == false)
                defaultAttackData = value;
        } 
    }

    [SerializeField] //기본 원거리 공격
    private AttackData defaultRangedData;
    public AttackData DefaultRangedData { get => defaultRangedData; set
        {
            if (defaultAttackData.IsRanged == true)
                defaultAttackData = value;
        } 
    }

    [SerializeField]
    private List<AttackData> skills;
    public List<AttackData> Skills { get => skills; set => skills = value; }    


    // Unity Editor에서 값을 변경할 때 실행됨
    private void OnValidate()
    {
        // defaultRangedData가 null이 아니고, IsRanged가 false일 경우 무효화
        if (defaultRangedData != null && !defaultRangedData.IsRanged)
        {
            Debug.LogWarning("DefaultRangedData는 원거리 공격 데이터만 설정할 수 있습니다.");
            defaultRangedData = null;
        }
        // defaultRangedData가 null이 아니고, IsRanged가 true일 경우 무효화
        if (defaultAttackData != null && defaultAttackData.IsRanged)
        {
            Debug.LogWarning("DefaultAttackData는 근접 공격 데이터만 설정할 수 있습니다.");
            defaultAttackData = null;
        }
    }

}
