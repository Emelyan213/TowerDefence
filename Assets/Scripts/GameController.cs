using System;
using Assets.Scripts.Enemy;
using Assets.Scripts.Towers;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public event Action<int> OnEndGame;

        public EnemiesManager Enemies => enemies;
        public Player Player => _player;

        [SerializeField] private int startPlayerHealth;
        [SerializeField] private EnemiesManager enemies;
        [SerializeField] private TowersManager towersManager;

        private Player _player;

        private void Awake()
        {
            enemies.TimeBetweenWaves = JsonWorker.Deserialize<ConfigFile>("config.txt").timeBetweenWaves;

            _player = new Player();
            _player.SetStartHealth(startPlayerHealth);

            _player.OnPlayerDeath += () =>
            {
                enemies.Stop();
                OnEndGame?.Invoke(enemies.KilledEnemiesCount);

                Time.timeScale = 0;
            };
        }

        private void Start()
        {
            _player.ResetValues();
        }

        public void Restart()
        {
            _player.ResetValues();
            enemies.Restart();
            towersManager.Restart();

            Time.timeScale = 1;
        }
    }
}
