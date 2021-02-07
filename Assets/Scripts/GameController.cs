using System;
using Assets.Scripts.Enemy;
using Assets.Scripts.Towers;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public event Action<int> OnEndGame;

        public EnemiesManager EnemiesManager => enemiesManager;
        public Player Player { get; private set; }

        [SerializeField] private int startPlayerHealth;
        [SerializeField] private EnemiesManager enemiesManager;
        [SerializeField] private TowersManager towersManager;

        private const int DefaultTimeBetweenWaves = 3;

        private void Awake()
        {
            enemiesManager.TimeBetweenWaves = GetTimeBetweenWaves();

            Player = new Player();
            Player.SetStartHealth(startPlayerHealth);

            Player.OnPlayerDeath += () =>
            {
                enemiesManager.Stop();
                OnEndGame?.Invoke(enemiesManager.KilledEnemiesCount);

                Time.timeScale = 0;
            };
        }

        private void Start()
        {
            Player.ResetValues();
        }

        public void Restart()
        {
            Player.ResetValues();
            enemiesManager.Restart();
            towersManager.Restart();

            Time.timeScale = 1;
        }

        private int GetTimeBetweenWaves()
        {
            var jsonWorker = new JsonWorker();
            var fileName = "config.txt";

            return jsonWorker.IsFileExist(fileName)
                ? jsonWorker.Deserialize<ConfigFile>(fileName).timeBetweenWaves
                : DefaultTimeBetweenWaves;
        }
    }
}
