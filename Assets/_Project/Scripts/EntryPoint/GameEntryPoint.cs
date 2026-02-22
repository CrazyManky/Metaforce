using _Project.Configs;
using _Project.Installers;
using _Project.Scripts.Factory;
using _Project.Scripts.Level;
using _Project.Scripts.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.EntryPoint
{
    public class GameEntryPoint : LifetimeScope
    {
        [SerializeField] private InputInstaller _inputInstaller;
        [SerializeField] private PlayerInstaller _playerInstaller;
        [SerializeField] private EnemyInstaller _enemyInstaller;
        [SerializeField] private LocationConfig _levelConfig;
        [SerializeField] private Location _locationPrefab;
        [SerializeField] private UICounter _counter;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_inputInstaller).AsImplementedInterfaces().AsSelf();
            builder.RegisterInstance(_playerInstaller).AsImplementedInterfaces().AsSelf();
            builder.RegisterInstance(_enemyInstaller).AsImplementedInterfaces().AsSelf();
            builder.RegisterInstance(_levelConfig).AsImplementedInterfaces().AsSelf();
            builder.RegisterComponentInNewPrefab(_counter, Lifetime.Singleton).AsSelf();
            builder.Register<PlayerFactory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<EnemyFactory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<LevelHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}