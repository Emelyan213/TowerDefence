using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Navigation;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Enemy
{
    public class EnemiesManager : MonoBehaviour
    {
        public event Action<int> OnChangedWave;
        public int KilledEnemiesCount { get; private set; }
        public List<IEnemy> Enemies { get; private set; }
        public int TimeBetweenWaves { get; set; }

        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private EnemyInfo[] enemyTypes;
        [SerializeField] private float delayingEnemyInstantiation = 1;

        [SerializeField] private WayPoint[] wayPoints;

        private GameController _gameController;
        private EnemiesStatesController _enemiesParametersController;
        private const int MaxRangeRandomEnemyCount = 3;

        private int _waveIndex = 1;
        private bool _isGameContinues = true;
        private int _notInstantiatedEnemiesCount = 0;

        private void Start()
        {
            Enemies = new List<IEnemy>();

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
            KilledEnemiesCount = 0;
            OnChangedWave?.Invoke(_waveIndex);

            DestroyAllEnemy();
            _enemiesParametersController.SetParameters(enemyTypes);
            _isGameContinues = true;

            SpawnEnemies();
        }

        private void DestroyAllEnemy()
        {
            foreach (var enemy in Enemies.ToArray())
                enemy.Destroy();

            Enemies.Clear();
        }

        private void SpawnEnemies()
        {
            var random = new Random();

            StartCoroutine(Spawn());

            IEnumerator Spawn()
            {
                while (_isGameContinues)
                {
                    _notInstantiatedEnemiesCount = random.Next(_waveIndex, _waveIndex + MaxRangeRandomEnemyCount);

                    yield return InstantiationEnemy(_notInstantiatedEnemiesCount);

                    yield return new WaitUntil(() => Enemies.Count == 0);

                    yield return new WaitForSeconds(TimeBetweenWaves);
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

                mover.SetWayPoints(wayPoints);

                InitializeEnemy(enemyObject);

                _notInstantiatedEnemiesCount--;

                yield return new WaitForSeconds(delayingEnemyInstantiation);
            }
        }

        private void InitializeEnemy(GameObject enemyObject)
        {
            var enemy = enemyObject.GetComponent<IEnemy>();
            enemy.SetParameters(_enemiesParametersController.GetRandomEnemyState());

            enemy.OnDeath += (coin) =>
            {
                Enemies.Remove(enemy);
                _gameController.Player.IncreaseGold(coin);

                KilledEnemiesCount++;

                if (Enemies.Count == 0 && _notInstantiatedEnemiesCount == 0)
                    EndWave();
            };

            enemy.OnSetMainTowerDamage += _gameController.Player.DecreaseHealth;
            Enemies.Add(enemy);
        }
    }
}
