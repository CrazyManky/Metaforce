using _Project.Installers;
using _Project.Scripts.Player;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Factory
{
    public class PlayerFactory
    {
        private LifetimeScope _lifetimeScope;
        private InputInstaller _inputInstaller;
        private PlayerInstaller _playerInstaller;

        public PlayerFactory(LifetimeScope lifetimeScope, InputInstaller inputHandler, PlayerInstaller playerInstaller)
        {
            _lifetimeScope = lifetimeScope;
            _inputInstaller = inputHandler;
            _playerInstaller = playerInstaller;
        }


        public PlayerCharacter CreatePlayer()
        {
            var playerScope = _lifetimeScope.CreateChild(builder =>
            {
                _inputInstaller.Install(builder);
                _playerInstaller.Install(builder);
            });
            var player = playerScope.Container.Resolve<PlayerCharacter>();

            return player;
        }
    }
}