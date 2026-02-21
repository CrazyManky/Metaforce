using System;
using System.Collections.Generic;
using _Project.Configs;
using _Project.Scripts.Factory;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Player;
using _Project.Scripts.UI;
using _Project.SOConfigs;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace _Project.Scripts.Level
{
    public class LevelHandler : IInitializable, IFixedTickable
    {
        private EnemyFactory _enemyFactory;
        private LevelConfig _levelConfig;
        private Location _location;
        private PlayerFactory _playerFactory;
        private List<IEnemy> _activeEnemies = new();
        private UICounter _counter;
        private PlayerCharacter _activePlayer;

        private ReactiveProperty<int> _deadCount = new(0);

        public LevelHandler(LevelConfig levelConfig, EnemyFactory enemyFactory, PlayerFactory playerFactory,
            Location location, UICounter counter)
        {
            _levelConfig = levelConfig;
            _enemyFactory = enemyFactory;
            _playerFactory = playerFactory;
            _location = location;
            _counter = counter;
        }

        public void Initialize()
        {
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
            enemy.transform.position = _location.GetRandomPoint();

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
            enemy.Transform.position = _location.GetRandomPoint();
            enemy.Transform.gameObject.SetActive(true);
        }

        public void FixedTick()
        {
            _activePlayer.SetTarget(GetTargetForPlayer());
        }
    }
}