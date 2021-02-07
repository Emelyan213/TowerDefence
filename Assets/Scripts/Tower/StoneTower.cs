using Assets.Scripts.Enemy;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class StoneTower : Tower
    {
        [SerializeField] private TowerInfo towerInfo;

        private NearestEnemyFinder _enemyFinder;

        private Vector3 _position;

        private float _timer = 0;

        private void Start()
        {
            _enemyFinder = FindObjectOfType<NearestEnemyFinder>();

            _position = transform.position;

            SetParameters(towerInfo);
        }

        public new void SetParameters(TowerInfo parameters)
        {
            base.SetParameters(parameters);
            GetComponentInChildren<SpriteRenderer>().sprite = parameters.sprite;
        }

        private void LateUpdate()
        {
            _timer += Time.deltaTime;

            if (_timer < _fireRate)
                return;

            var enemy = _enemyFinder.GetNearestEnemy(_position, _fireRange);

            if (enemy == null)
                return;

            _timer = 0;
            enemy.GetDamage(_shootPower);
        }
    }
}
