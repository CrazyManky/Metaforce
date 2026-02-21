using _Project.Configs;
using _Project.Scripts.Level;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Installers
{
    [CreateAssetMenu(fileName = "Installers/EnemyInstantaller", menuName = "EnemyInstaller")]
    public class EnemyInstaller : ScriptableObject, IInstaller
    {
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private Location _pointContainer;

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_pointContainer).As<IPointContainer>();
            builder.RegisterInstance(_enemyConfig).AsSelf();
            builder.RegisterInstance(_enemyConfig.EnemyCharacterPrefab).AsSelf();
        }
    }
}