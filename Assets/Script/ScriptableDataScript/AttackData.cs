using UnityEngine;

namespace Script.ScriptableDataScript
{
    public enum CharacterEnum
    {
        HakureiReimu,
        KirisameMarisa,
    }

    [CreateAssetMenu(menuName = "ScriptableData/AttackData", fileName = "AttackData")]
    public class AttackData : ScriptableObject
    {

        [SerializeField]
        private int id;
        public int Id { get => id; set => id = value; }
        
        [SerializeField] 
        private CharacterEnum charEnum;
        public CharacterEnum CharEnum { get => charEnum; set => charEnum = value; }

        [SerializeField]
        private string attackName;
        public string AttackName { get => attackName; set => attackName = value; }
        
        [SerializeField] 
        private Sprite icon;
        public Sprite Icon { get => icon; set => icon = value; }

        [SerializeField]
        private bool isRanged;
        public bool IsRanged { get => isRanged; set => isRanged = value; }

        [SerializeField]
        private GameObject rangedPrefab;
        public GameObject RangedPrefab { get => rangedPrefab; set => rangedPrefab = value; }
    }
}