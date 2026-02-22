using UnityEngine;
using Random = System.Random;

namespace _Project.Scripts.Level
{
    public class Location : MonoBehaviour
    {
        
    }

    public interface IPointContainer
    {
        public Vector3 GetRandomPoint();
    }
}