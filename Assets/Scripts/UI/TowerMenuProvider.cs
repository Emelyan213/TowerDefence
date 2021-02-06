using Assets.Scripts.Towers;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class TowerMenuProvider : MonoBehaviour
    {
        private TowerImproveMenu _menu;
        private Tower _tower;

        private void Start()
        {
            _menu = FindObjectOfType<TowerImproveMenu>();
            _tower = GetComponent<Tower>();
        }

        private void OnMouseDown()
        {
            if (_menu.IsActiveForTower(_tower))
                return;

            var position = Input.mousePosition;

            _menu.Show(_tower, position);
        }
    }
}