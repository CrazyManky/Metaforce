using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptable Objects/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("PlayerStats")] 
        [field:SerializeField] public float Speed{get; set;}
        [field:SerializeField] public int Damage{get; set;} 
        [field:SerializeField] public float AttackSpeed{get; set;}
        [field:SerializeField] public float AttackRange{get; set;}

        [Header("PlayerData")] 
        [SerializeField] private PlayerCharacter _playerCharacterPrefab;

        public PlayerCharacter PlayerCharacterPrefab => _playerCharacterPrefab;
    }
}