using UnityEngine;

namespace _Project.SOConfigs
{
    public class LevelConfig : MonoBehaviour
    {
        [field: SerializeField] public int MaxEnemies { get; private set; }
    }
}