using UniRx;
using UnityEngine;

namespace _Project.Scripts.Interfaces
{
    public interface IInputHandler
    {
        public IReadOnlyReactiveProperty<Vector2> MoveDirection { get; }
    }
}