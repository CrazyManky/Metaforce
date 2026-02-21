using System;
using _Project.Configs;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Level;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace _Project.Scripts.Enemy
{
    public class EnemyCharacter : MonoBehaviour, IEnemy
    {
        [SerializeField] private NavMeshAgent _agent;

        private IPointContainer _pointContainer;
        private Vector3 _targetPoint;
        public ReactiveCommand IsDead = new();

        public IDisposable _disposable = new CompositeDisposable();

        public Transform Transform => transform;

        public float DelayRespawn { get; private set; }
        public int Health { get; private set; }
        public Vector3 TargetPosition { get; private set; }

        [Inject]
        public void Construct(IPointContainer pointContainer)
        {
            _pointContainer = pointContainer;
        }

        private void Start() => MoveToRandomPoint();
        public void SetPoints(IPointContainer pointContainer) => _pointContainer = pointContainer;

        public void SetData(EnemyConfig config)
        {
            DelayRespawn = config.DelayRespawn;
            Health = config.MaxHealth;
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
            _targetPoint = _pointContainer.GetRandomPoint();
            _agent.SetDestination(_targetPoint);
        }

        public void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}