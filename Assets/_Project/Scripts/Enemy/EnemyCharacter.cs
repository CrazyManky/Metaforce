using System;
using _Project.Configs;
using _Project.Scripts.Interfaces;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Enemy
{
    public class EnemyCharacter : MonoBehaviour, IEnemy
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Renderer _renderEnemy;

        private static readonly int Size = Shader.PropertyToID("_size");
        private float _outlineValue = 1.05f;
        private LocationConfig _pointContainer;

        public ReactiveCommand IsDead = new();
        public Transform Transform => transform;
        public float DelayRespawn { get; private set; }
        public int Health { get; private set; }
        public Vector3 TargetPosition { get; private set; }

        private Material _outlineMaterial;

        private void Start()
        {
            _outlineMaterial = _renderEnemy.materials[1];
            MoveToRandomPoint();
        }

        public void SetPoints(LocationConfig pointContainer) => _pointContainer = pointContainer;

        public void SetData(EnemyConfig config)
        {
            DelayRespawn = config.DelayRespawn;
            Health = config.MaxHealth;
        }

        public void ActiveOutline()
        {
            _outlineMaterial.SetFloat(Size, _outlineValue);
        }

        public void TakeDamage(int damage)
        {
            Health = Math.Max(0, Health - damage);
            if (Health == 0)
            {
                IsDead.Execute();
                gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (_agent.pathPending) return;

            if (_agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending)
                MoveToRandomPoint();
        }

        private void MoveToRandomPoint()
        {
            TargetPosition = _pointContainer.GetRandomPoint();
            _agent.SetDestination(TargetPosition);
        }

        public void DisableOutline()
        {
            Debug.Log("Отключение обводки");
            _outlineMaterial.SetFloat(Size, 0);
        }
    }
}