using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class InformationViewer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI goldCountText;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI waveIndexText;
        private void Start()
        {
            var gameController = FindObjectOfType<GameController>();

            gameController.Player.OnHealthChanged += health =>  healthText.text = health.ToString();
            gameController.Player.OnGoldChanged += gold => goldCountText.text = gold.ToString();
            gameController.Enemies.OnChangedWave += wave => waveIndexText.text = $"волна {wave}";
        }
    }
}

