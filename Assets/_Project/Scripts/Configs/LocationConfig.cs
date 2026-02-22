using _Project.Scripts.Level;
using UnityEngine;

namespace _Project.Configs
{
    [CreateAssetMenu(fileName = "LocationConfig", menuName = "Configs/LocationConfig", order = 0)]
    public class LocationConfig : ScriptableObject
    {
        [field: SerializeField] public Location LocationPrefab { get; private set; }
        [field: SerializeField] public int MaxEnemies { get; private set; }
        [SerializeField] private Vector3[] _position;

        public Vector3 GetRandomPoint()
        {
            return _position[Random.Range(0, _position.Length)];
        }
    }
}