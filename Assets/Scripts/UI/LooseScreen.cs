using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class LooseScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI killedEnemiesCount;
        [SerializeField] private Button resetButton;
        [SerializeField] private GameObject content;

        private void Awake()
        {
            var gameManager = FindObjectOfType<GameController>();

            resetButton.onClick.AddListener(()=>
            {
                gameManager.Restart();
                content.SetActive(false);
            });

            gameManager.OnEndGame += Show;
        }

        private void Show(int killedEnemies)
        {
            content.SetActive(true);

            killedEnemiesCount.text = $"Врагов убито {killedEnemies}";
        }

    }
}
