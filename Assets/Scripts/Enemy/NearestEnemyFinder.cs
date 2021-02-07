using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class NearestEnemyFinder : MonoBehaviour
    {
        private EnemiesManager _enemiesManager;

        private void Start()
        {
            _enemiesManager = GetComponent<EnemiesManager>();
        }

        public IEnemy GetNearestEnemy(Vector3 position, float findDistance)
        {
            if (_enemiesManager.Enemies.Count == 0)
                return null;

            var nearestEnemy = _enemiesManager.Enemies[0];

            foreach (var enemy in _enemiesManager.Enemies)
            {
                var distanceToCurrentEnemy = Vector3.Distance(enemy.Position, position);
                var distanceToNearestEnemy = Vector3.Distance(nearestEnemy.Position, position);

                if (distanceToCurrentEnemy < distanceToNearestEnemy)
                    nearestEnemy = enemy;
            }

            if (Vector3.Distance(nearestEnemy.Position, position) > findDistance)
                nearestEnemy = null;

            return nearestEnemy;
        }
    }

}