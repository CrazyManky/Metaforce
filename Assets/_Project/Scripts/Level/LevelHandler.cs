using System;
using System.Collections.Generic;
using _Project.Configs;
using _Project.Scripts.Factory;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using _Project.Scripts.UI;
using UniRx;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;


namespace _Project.Scripts.Level
{
    public class LevelHandler : IInitializable, IFixedTickable
    {
        private EnemyFactory _enemyFactory;
        private LocationConfig _levelConfig;
        private PlayerFactory _playerFactory;
        private List<IEnemy> _activeEnemies = new();
        private UICounter _counter;
        private PlayerCharacter _activePlayer;

        private ReactiveProperty<int> _deadCount = new(0);

        public LevelHandler(LocationConfig levelConfig, EnemyFactory enemyFactory, PlayerFactory playerFactory,
            UICounter counter)
        {
            _levelConfig = levelConfig;
            _enemyFactory = enemyFactory;
            _playerFactory = playerFactory;
            _counter = counter;
        }

        public void Initialize()
        {
            Object.Instantiate(_levelConfig.LocationPrefab);
            _deadCount.Subscribe(_counter.SetValue).AddTo(_counter);
            _activePlayer = _playerFactory.CreatePlayer();
            CreateEnemies();
        }

        private IEnemy GetTargetForPlayer()
        {
            if (_activePlayer == null || _activeEnemies.Count == 0)
                return null;

            IEnemy closest = null;
            float minDistSqr = _activePlayer.Config.AttackRange * _activePlayer.Config.AttackRange;
            Vector3 playerPos = _activePlayer.transform.position;

            foreach (var enemy in _activeEnemies)
            {
                if (enemy == null) continue;
                if (!enemy.Transform.gameObject.activeSelf) continue;

                float distSqr = (enemy.Transform.position - playerPos).sqrMagnitude;
                enemy.DisableOutline();
                if (distSqr <= minDistSqr)
                {
                    minDistSqr = distSqr;
                    closest = enemy;
                }
            }

            return closest;
        }

        private void CreateEnemies()
        {
            for (int i = 0; i < _levelConfig.MaxEnemies; i++)
            {
                CreateEnemy();
            }
        }


        private void CreateEnemy()
        {
            var enemy = _enemyFactory.GetEnemy();
            enemy.transform.position = _levelConfig.GetRandomPoint();

            if (!_activeEnemies.Contains(enemy))
                _activeEnemies.Add(enemy);

            enemy.IsDead
                .Subscribe(_ =>
                {
                    Observable.Timer(TimeSpan.FromSeconds(enemy.DelayRespawn))
                        .Subscribe(__ => { RespawnEnemy(enemy); });
                    _deadCount.Value++;
                })
                .AddTo(enemy);
        }

        private void RespawnEnemy(IEnemy enemy)
        {
            enemy.SetData(_enemyFactory.Config);
            enemy.Transform.position = _levelConfig.GetRandomPoint();
            enemy.Transform.gameObject.SetActive(true);
        }

        public void FixedTick()
        {
            var target = GetTargetForPlayer();
            if (target != null)
                target.ActiveOutline();
            _activePlayer.SetTarget(target);
        }
    }
}