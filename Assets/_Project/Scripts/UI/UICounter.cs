using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class UICounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;

        public void SetValue(int value)
        {
            _counter.text = $"Убито:{value}";
        }
    }
}