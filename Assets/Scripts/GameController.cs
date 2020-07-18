using UnityEngine;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public Player Player => _player;
        public EnemiesManager Enemies => enemies;
        private Player _player;

        [SerializeField] private int startPlayerHealth;
        [SerializeField] private EnemiesManager enemies;

        private void Awake()
        {
            _player = new Player();
            _player.SetStartHealth(startPlayerHealth);
        }

        private void Start()
        {
            _player.ResetValues();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
