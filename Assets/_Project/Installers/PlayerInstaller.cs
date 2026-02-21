using _Project.Configs;
using _Project.Scripts.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Installers
{
    [CreateAssetMenu(menuName = "Installers/PlayerInstaller")]
    public class PlayerInstaller : ScriptableObject, IInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerConfig);
            builder.RegisterComponentInNewPrefab(_playerConfig.PlayerCharacterPrefab, Lifetime.Scoped);
        }
    }
}