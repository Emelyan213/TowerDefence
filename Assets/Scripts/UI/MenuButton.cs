using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _label;

        public void Setup(UnityAction action, string text)
        {
            _button.onClick.AddListener(action);
            _label.text = text;
        }
    }
}
