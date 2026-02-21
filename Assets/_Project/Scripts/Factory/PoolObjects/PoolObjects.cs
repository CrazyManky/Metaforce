using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Factory.PoolObjects
{
    public class PoolObjects<T> where T : MonoBehaviour
    {
        private List<T> _objects;
        private T _pullObject;

        public void Initialize(T PullObject)
        {
            _objects = new(10);
            _pullObject = PullObject;
            _pullObject.gameObject.SetActive(false);

            for (int i = 0; i < _objects.Count; i++)
            {
                var instasnce = Object.Instantiate(_pullObject);
                instasnce.gameObject.SetActive(false);
                _objects.Add(instasnce);
            }
        }

        public T GetObject()
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                if (!_objects[i].gameObject.activeSelf)
                {
                    _objects[i].gameObject.SetActive(true);
                    return _objects[i];
                }
            }

            var instasnce = Object.Instantiate(_pullObject);
            instasnce.gameObject.SetActive(true);
            _objects.Add(instasnce);
            return instasnce;
        }

        public void Dispose()
        {
            for (int i = 0; i < _objects.Count; i++)
                Object.Destroy(_objects[i]);

            _objects.Clear();
            _objects = null;
        }
    }
}