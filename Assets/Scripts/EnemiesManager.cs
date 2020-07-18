using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Navigation;
using Source.Enemy;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class EnemiesManager : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public EnemyInfo[] enemyTypes;
        public float delay = 1;
        public event Action<int> OnChangedWave;

        public WayPoint[] wps;

        private List<IEnemy> _enemies = new List<IEnemy>();

        private GameController _gameController;
        private EnemiesParametersController _enemiesParametersController;
        private const int MaxRangeRandomEnemyCount = 3;
        private int timeBetweenWaves = 5;

        private int _waveIndex = 1;

        private void Awake()
        {
            _enemiesParametersController = new EnemiesParametersController();

            _enemiesParametersController.SetParameters(enemyTypes);
        }

        void Start()
        {
            _gameController = FindObjectOfType<GameController>();

            var random = new Random();

            for (var i = 0; i < 10; i++)
                print(random.Next(0, 2));

            var count = 10;

            StartCoroutine(Spawn());

            IEnumerator Spawn()
            {
                while (true)
                {
                    count = random.Next(_waveIndex, _waveIndex + MaxRangeRandomEnemyCount);

                    yield return InstantiationEnemy(count);

                    yield return new WaitForSeconds(timeBetweenWaves);
                }
            }
        }

        private void EndWave()
        {
            _waveIndex++;
            OnChangedWave?.Invoke(_waveIndex);

            _enemiesParametersController.ImproveParameters();
        }

        private IEnumerator InstantiationEnemy(int countForInstantiate)
        {
            for (var i = 0; i < countForInstantiate; i++)
            {
                var enemyObject = Instantiate(enemyPrefab, transform);

                var mover = enemyObject.GetComponent<Mover>();

                mover.SetWayPoints(wps);
                mover.StartMoveOnPoints();

                EnemySetting(enemyObject);

                yield return new WaitForSeconds(delay);
            }
        }

        private void EnemySetting(GameObject enemyObject)
        {
            var enemy = enemyObject.GetComponent<IEnemy>();
            enemy.SetParameters(_enemiesParametersController.GetRandomParameters());

            enemy.OnDeath += (ee, coin) =>
            {
                _enemies.Remove(ee);
                _gameController.Player.IncreaseGold(coin);

                if (_enemies.Count == 0)
                    EndWave();
            };

            enemy.OnSetMainTowerDamage += _gameController.Player.DecreaseHealth;
            _enemies.Add(enemy);
        }

        public IEnemy GetNearestEnemy(Vector3 position, float findDistance)
        {
            if (_enemies.Count == 0)
                return null;

            var nearestEnemy = _enemies[0];

            foreach (var enemy in _enemies)
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
