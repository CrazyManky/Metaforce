using _Project.Configs;
using _Project.Installers;
using _Project.Scripts.Enemy;
using _Project.Scripts.Factory.PoolObjects;
using _Project.Scripts.Level;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Factory
{
    public class EnemyFactory : IInitializable
    {
        private LifetimeScope _lifetimeScope;
        private EnemyInstaller _enemyInstaller;
        private PoolObjects<EnemyCharacter> _poolObjects;
        private LifetimeScope _enemyScope;
        private LocationConfig _pointContainer;
        public EnemyConfig Config { get; private set; }

        public EnemyFactory(LifetimeScope scope, EnemyInstaller enemyInstaller, LocationConfig locationConfig)
        {
            _enemyInstaller = enemyInstaller;
            _lifetimeScope = scope;
            _pointContainer = locationConfig;
            _poolObjects = new PoolObjects<EnemyCharacter>();
        }

        public void Initialize()
        {
            _enemyScope = _lifetimeScope.CreateChild(builder => { _enemyInstaller.Install(builder); });
            ;
            _poolObjects.Initialize(_enemyScope.Container.Resolve<EnemyCharacter>());
            Config = _enemyScope.Container.Resolve<EnemyConfig>();
        }

        public EnemyCharacter GetEnemy()
        {
            var enemy = _poolObjects.GetObject();
            enemy.SetPoints(_pointContainer);
            enemy.SetData(Config);
            return enemy;
        }
    }
}