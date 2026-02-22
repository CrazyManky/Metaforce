using _Project.Configs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Installers
{
    [CreateAssetMenu(fileName = "Installers/EnemyInstantaller", menuName = "EnemyInstaller")]
    public class EnemyInstaller : ScriptableObject, IInstaller
    {
        [SerializeField] private EnemyConfig _enemyConfig;

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_enemyConfig).AsSelf();
            builder.RegisterInstance(_enemyConfig.EnemyCharacterPrefab).AsSelf();
        }
    }
}