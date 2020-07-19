using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public event Action<int> OnEndGame;

        public Player Player => _player;
        public EnemiesManager Enemies => enemies;
        private Player _player;

        [SerializeField] private int startPlayerHealth;
        [SerializeField] private EnemiesManager enemies;
        [SerializeField] private TowersManager towersManager;

        private void Awake()
        {
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
