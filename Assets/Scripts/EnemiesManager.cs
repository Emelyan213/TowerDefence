using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Navigation;
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

        public int KilledEnemiesCount => _killedEnemiesCount;

        private int _killedEnemiesCount;

        private List<IEnemy> _enemies = new List<IEnemy>();

        private GameController _gameController;
        private EnemiesStatesController _enemiesParametersController;
        private const int MaxRangeRandomEnemyCount = 3;
        private int _timeBetweenWaves;

        private int _waveIndex = 1;
        private bool _isGameContinues = true;

        private void Start()
        {

            _timeBetweenWaves = JsonWorker.Deserialize<ConfigFile>("config.txt").timeBetweenWaves;


            print(_timeBetweenWaves);
            _enemiesParametersController = new EnemiesStatesController();
            _enemiesParametersController.SetParameters(enemyTypes);

            _gameController = FindObjectOfType<GameController>();

            SpawnEnemies();
        }

        public void Stop()
        {
            _isGameContinues = false;
        }

        public void Restart()
        {
            _waveIndex = 1;
            _killedEnemiesCount = 0;
            OnChangedWave?.Invoke(_waveIndex);

            DestroyAllEnemy();
            _enemiesParametersController.SetParameters(enemyTypes);
            _isGameContinues = true;

            SpawnEnemies();
        }

        private void DestroyAllEnemy()
        {
            foreach (var enemy in _enemies.ToArray())
                enemy.Destroy();

            _enemies.Clear();
        }

        private void SpawnEnemies()
        {
            var random = new Random();

            StartCoroutine(Spawn());

            IEnumerator Spawn()
            {
                while (_isGameContinues)
                {
                    var count = random.Next(_waveIndex, _waveIndex + MaxRangeRandomEnemyCount);

                    yield return InstantiationEnemy(count);

                    yield return new WaitUntil(() => _enemies.Count == 0);

                    yield return new WaitForSeconds(_timeBetweenWaves);
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

                _killedEnemiesCount++;

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
