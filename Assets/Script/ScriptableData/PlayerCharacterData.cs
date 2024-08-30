using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableData/PlayerData", order = int.MaxValue)]
public class PlayerCharacterData : ScriptableObject
{
    //플레이어 캐릭터 데이터
    [SerializeField]
    private float health;
    public float Health { get => health; set => health = value;  }
    [SerializeField] 
    private float might; //힘, 공격은 기본 공격력 * 힘 * 추가 피해량, 화면에는 %로 표기
    public float Might {get => might; set => health = value;}
    
    

}
