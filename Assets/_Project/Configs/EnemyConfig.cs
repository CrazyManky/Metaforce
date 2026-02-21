using _Project.Scripts.Enemy;
using UnityEngine;

namespace _Project.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Enemy")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public int MaxHealth;
        [field: SerializeField] public float  DelayRespawn;
        [SerializeField] private EnemyCharacter _enemyCharacterPrefab;

        public EnemyCharacter EnemyCharacterPrefab => _enemyCharacterPrefab;
    }
}