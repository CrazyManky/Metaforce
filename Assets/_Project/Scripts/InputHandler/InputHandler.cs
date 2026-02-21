using System;
using _Project.Scripts.Interfaces;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace _Project.Scripts.InputHandler
{
    public class InputHandler :IInputHandler, IInitializable, IDisposable, ITickable
    {
        private const string _nameActionMap = "PlayerMovement";
        private const string _nameAction = "Movement";
        private InputActionAsset _inputActionAsset;
        private InputActionMap _playerActionMap;
        private InputAction _moveAction;
        private IReactiveProperty<Vector2> _moveDirection = new ReactiveProperty<Vector2>(Vector2.zero);

        public IReadOnlyReactiveProperty<Vector2> MoveDirection => _moveDirection;

        public InputHandler(InputActionAsset inputActionAsset)
        {
            _inputActionAsset = inputActionAsset;
        }

        public void Initialize()
        {
            _playerActionMap = _inputActionAsset.FindActionMap(_nameActionMap, true);
            _moveAction = _playerActionMap.FindAction(_nameAction, true);
            _moveAction.Enable();
        }

        public void Dispose()
        {
            _playerActionMap?.Dispose();
            _moveAction?.Dispose();
        }

        public void Tick()
        {
            _moveDirection.Value = _moveAction.ReadValue<Vector2>();
        }
    }
}