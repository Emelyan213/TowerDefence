using UnityEngine;

namespace Source.Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        protected Transform _transform;

        protected float _health;
        protected int _goldCoinCount;
        protected int _damagePower;

        private void Awake()
        {
            _transform = transform;
        }
        protected void SetParameters(EnemyState state)
        {
            _health = state.health;
            _goldCoinCount = state.goldCoinCount;
            _damagePower = state.damagePower;
        }

    }
}

