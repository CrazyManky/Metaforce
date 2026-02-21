using UnityEngine;
using Random = System.Random;

namespace _Project.Scripts.Level
{
    public class Location : MonoBehaviour, IPointContainer
    {
        [SerializeField] private Transform[] _routingPoints;

        private float _OffestY = 1f;

        public Vector3 GetRandomPoint()
        {
            var random = new Random().Next(0, _routingPoints.Length);
            var resolvePosition = _routingPoints[random].position + new Vector3(0, _OffestY, 0);
            return resolvePosition;
        }
    }

    public interface IPointContainer
    {
        public Vector3 GetRandomPoint();
    }
}