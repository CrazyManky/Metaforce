using _Project.Configs;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.Interfaces
{
    public interface IEnemy
    {
        public Transform Transform { get; }
        public float DelayRespawn { get; }
        public int Health { get; }
        public Vector3 TargetPosition { get; }
        public void ActiveOutline();

        public void SetData(EnemyConfig config);
        public void TakeDamage(int damage);
        public void DisableOutline();
    }
}