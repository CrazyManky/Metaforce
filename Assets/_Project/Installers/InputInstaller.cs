using _Project.Scripts.InputHandler;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace _Project.Installers
{
    [CreateAssetMenu(fileName = "PlayerInputContainer", menuName = "PlayerInputContainer")]
    public class InputInstaller : ScriptableObject, IInstaller
    {
        [SerializeField] private InputActionAsset _inputActionAsset;

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterComponent(_inputActionAsset);
            builder.Register<InputHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}