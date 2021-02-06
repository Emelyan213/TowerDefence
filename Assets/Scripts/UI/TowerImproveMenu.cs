using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Assets.Scripts.Towers;

namespace Assets.Scripts.UI
{
    public class TowerImproveMenu : MonoBehaviour
    {
        [SerializeField] private GameObject content;
        [SerializeField] private Button closeButton;
        [SerializeField] private GameObject buttonPrefab;
        [SerializeField] private Transform placeForButtons;
        [SerializeField] private TextMeshProUGUI characteristicInfo;
        [SerializeField] private TextMeshProUGUI improvePrice;

        private GameController _gameController;
        private Tower _currentTower;

        private void Awake()
        {
            _gameController = FindObjectOfType<GameController>();
            closeButton.onClick.AddListener(Hide);
        }
        public bool IsActiveForTower(Tower tower)
        {
            return tower == _currentTower;
        }

        public void Show(Tower tower, Vector3 position)
        {
            content.SetActive(true);
            transform.position = position;
            _currentTower = tower;

            characteristicInfo.text =
                $"Сила выстрела - {tower.ShootPower:F1} \n " +
                $"Скорострельность - {tower.FireRate:F1} \n " +
                $"Дальность - {tower.FireRange:F1} \n";

            improvePrice.text =$"Стоимость улучшения {tower.ImprovePrice}";

            ClearOldContent();

            CreateButtons(tower);
        }

        private void ClearOldContent()
        {
            var buttons = placeForButtons.GetComponentsInChildren<Button>().Select(e => e.gameObject);

            foreach (var button in buttons.ToList())
                Destroy(button);
        }

        private void CreateButtons(Tower tower)
        {
            CreateButton(GetImproveActionWithCheck(tower.ImproveShootPower, tower.ImprovePrice), "улучшить силу выстрела");

            CreateButton(GetImproveActionWithCheck(tower.ImproveFireRate, tower.ImprovePrice), "улучшить скорострельность");

            CreateButton(GetImproveActionWithCheck(tower.ImproveFireRange, tower.ImprovePrice), "улучшить дальность");
        }

        private UnityAction GetImproveActionWithCheck(Action action, int price)
        {
            return () =>
            {
                if (!_gameController.Player.TryToDecreaseGoldCoins(price)) return;

                action?.Invoke();
                Hide();
            };
        }

        private void CreateButton(UnityAction action, string text)
        {
            var menuButton = Instantiate(buttonPrefab, placeForButtons).GetComponent<MenuButton>();

            menuButton.Setup(action, text);
        }

        private void Hide()
        {
            content.SetActive(false);
            _currentTower = null;
        }
    }
}
