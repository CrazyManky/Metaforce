using System;
using _Project.Configs;
using _Project.Scripts.Interfaces;
using UniRx;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        public PlayerConfig Config { get; private set; }
        private IInputHandler _inputHandler;
        private ReactiveProperty<IEnemy> _target = new();

        private void Start()
        {
            _target
                .Select(target =>
                {
                    if (target == null)
                        return Observable.Empty<long>();

                    return Observable.Interval(TimeSpan.FromSeconds(Config.AttackSpeed));
                })
                .Switch()
                .Subscribe(_ => Attack())
                .AddTo(this);
        }

        [Inject]
        public void Construct(IInputHandler inputHandler, PlayerConfig playerConfig)
        {
            _inputHandler = inputHandler;
            Config = playerConfig;
        }

        public void SetTarget(IEnemy target)
        {
            _target.Value = target;
        }

        private void Update() => Move(_inputHandler.MoveDirection.Value);

        private void Move(Vector2 direction)
        {
            if (direction == Vector2.zero) return;
            _controller.Move(new Vector3(direction.x, 0, direction.y) * Config.Speed);
        }

        private void Attack()
        {
            if (_target.Value != null)
                _target.Value.TakeDamage(Config.Damage);
        }
    }
}